using System;
using System.Threading;

namespace AudioCompressor
{
    public static class AudioCompressorCore
    {
        private static int ClampToShort(int value)
        {
            return Math.Max(short.MinValue, Math.Min(short.MaxValue, value));
        }

        // إعدادات DPCM حسب عدد البتات (4 أو 8). يجب أن تكون نفسها عند الضغط وفك الضغط.
        // نفس المدى (±1024 تقريباً) في الحالتين: كلما قلّ عدد البتات زادت خطوة التكميم.
        private static void GetDpcmParams(int bits, out int levels, out int center, out int scale)
        {
            if (bits != 4 && bits != 8)
                throw new ArgumentException("DPCM يدعم 4 أو 8 بت فقط", "bits");
            levels = 1 << bits;      // 16 أو 256
            center = levels / 2;     // 8 أو 128 (يمثل فرقاً = 0)
            scale = 2048 / levels;   // 128 أو 8
        }

        // ====================================
        // ⭐⭐⭐ دوال الضغط (Compress) ⭐⭐⭐
        // ====================================

        // 1. خوارزمية Delta Modulation (DM)
        // ⭐⭐⭐ 1. Delta Modulation (DM) ⭐⭐⭐
        public static byte[] CompressDM(short[] samples, int stepSize, CancellationToken ct, IProgress<int> progress)
        {
            byte[] compressedData = new byte[(samples.Length + 7) / 8];
            int predicted = 0;
            byte currentByte = 0;
            int bitIndex = 7;

            // حساب نسبة التحديث (كل 1% نحدث الشريط)
            int reportInterval = Math.Max(1, samples.Length / 100);

            for (int i = 0; i < samples.Length; i++)
            {
                // ⭐ هل ضغط المستخدم زر الإلغاء؟ ⭐
                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested(); // أوقف الدالة فوراً
                  
                }
                if (i % 50000 == 0) Thread.Sleep(1);
                if (samples[i] > predicted)
                {
                    currentByte |= (byte)(1 << bitIndex);
                    predicted += stepSize;
                }
                else
                {
                    predicted -= stepSize;
                }

                bitIndex--;
                if (bitIndex < 0)
                {
                    compressedData[i / 8] = currentByte;
                    currentByte = 0;
                    bitIndex = 7;
                }

                // ⭐ تحديث التقدم ⭐
                if (i % reportInterval == 0)
                {
                    progress.Report((int)((long)i * 100 / samples.Length));
                }
            }

            // إصلاح: كتابة آخر بايت إذا لم يكتمل (عدد العينات ليس من مضاعفات 8)
            if (bitIndex != 7 && samples.Length > 0)
                compressedData[(samples.Length - 1) / 8] = currentByte;

            progress.Report(100);
            return compressedData;
        }


        // ⭐⭐⭐ 2. Adaptive Delta Modulation (ADM) ⭐⭐⭐
        public static byte[] CompressADM(short[] samples, int initialStepSize, CancellationToken ct, IProgress<int> progress)
        {
            byte[] compressedData = new byte[(samples.Length + 7) / 8];
            int predicted = 0;
            int step = initialStepSize;
            int minStep = initialStepSize / 4;
            int maxStep = initialStepSize * 10;
            int lastBit = 0;
            byte currentByte = 0;
            int bitIndex = 7;

            int reportInterval = Math.Max(1, samples.Length / 100);

            for (int i = 0; i < samples.Length; i++)
            {
                if (ct.IsCancellationRequested) ct.ThrowIfCancellationRequested();
                if (i % 50000 == 0) Thread.Sleep(1);

                int currentBit = 0;
                if (samples[i] > predicted) { currentBit = 1; predicted += step; }
                else { currentBit = 0; predicted -= step; }

                if (currentBit == lastBit) step = Math.Min(step * 2, maxStep);
                else step = Math.Max(step / 2, minStep);

                lastBit = currentBit;
                if (currentBit == 1) currentByte |= (byte)(1 << bitIndex);

                bitIndex--;
                if (bitIndex < 0) { compressedData[i / 8] = currentByte; currentByte = 0; bitIndex = 7; }

                if (i % reportInterval == 0) progress.Report((int)((long)i * 100 / samples.Length));
            }

            // إصلاح: كتابة آخر بايت إذا لم يكتمل
            if (bitIndex != 7 && samples.Length > 0)
                compressedData[(samples.Length - 1) / 8] = currentByte;

            progress.Report(100);
            return compressedData;
        }


        // ⭐⭐⭐ 3. DPCM (4-bit أو 8-bit) ⭐⭐⭐
        // 8-bit: بايت واحد لكل عينة (ضغط 2:1)، 4-bit: عينتان في كل بايت (ضغط 4:1)
        public static byte[] CompressDPCM(short[] samples, int bits, CancellationToken ct, IProgress<int> progress)
        {
            int levels, center, scale;
            GetDpcmParams(bits, out levels, out center, out scale);

            byte[] compressedData = (bits == 8) ? new byte[samples.Length] : new byte[(samples.Length + 1) / 2];
            int predicted = 0;

            int reportInterval = Math.Max(1, samples.Length / 100);

            for (int i = 0; i < samples.Length; i++)
            {
                if (ct.IsCancellationRequested) ct.ThrowIfCancellationRequested();
                if (i % 50000 == 0) Thread.Sleep(1);

                int diff = samples[i] - predicted;
                // تقريب لأقرب مستوى ثم حصر القيمة داخل المدى لمنع الالتفاف (overflow)
                int q = (int)Math.Round((double)diff / scale, MidpointRounding.AwayFromZero) + center;
                q = Math.Max(0, Math.Min(levels - 1, q));

                if (bits == 8)
                {
                    compressedData[i] = (byte)q;
                }
                else if (i % 2 == 0)
                {
                    compressedData[i / 2] = (byte)(q << 4);   // النصف العلوي للعينة الزوجية
                }
                else
                {
                    compressedData[i / 2] |= (byte)q;         // النصف السفلي للعينة الفردية
                }

                predicted = ClampToShort(predicted + ((q - center) * scale));

                if (i % reportInterval == 0) progress.Report((int)((long)i * 100 / samples.Length));
            }
            progress.Report(100);
            return compressedData;
        }


        // ====================================
        // ⭐⭐⭐ دوال فك الضغط (Decompress) ⭐⭐⭐
        // ====================================

        // 1. فك ضغط Delta Modulation (DM)
        public static short[] DecompressDM(byte[] compressedData, int stepSize, int originalLength)
        {
            short[] decompressedData = new short[originalLength];
            int predicted = 0;

            for (int i = 0; i < originalLength; i++)
            {
                int byteIndex = i / 8;
                int bitIndex = 7 - (i % 8);
                int bit = (compressedData[byteIndex] >> bitIndex) & 1;

                if (bit == 1)
                {
                    predicted += stepSize;
                }
                else
                {
                    predicted -= stepSize;
                }

                decompressedData[i] = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, predicted));
            }
            return decompressedData;
        }

        // 2. فك ضغط Adaptive Delta Modulation (ADM)
        public static short[] DecompressADM(byte[] compressedData, int initialStepSize, int originalLength)
        {
            short[] decompressedData = new short[originalLength];
            int predicted = 0;
            int step = initialStepSize;
            int minStep = initialStepSize / 4;
            int maxStep = initialStepSize * 10;
            int lastBit = 0;

            for (int i = 0; i < originalLength; i++)
            {
                int byteIndex = i / 8;
                int bitIndex = 7 - (i % 8);
                int currentBit = (compressedData[byteIndex] >> bitIndex) & 1;

                if (currentBit == 1)
                    predicted += step;
                else
                    predicted -= step;

                decompressedData[i] = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, predicted));

                if (currentBit == lastBit)
                {
                    step = Math.Min(step * 2, maxStep);
                }
                else
                {
                    step = Math.Max(step / 2, minStep);
                }

                lastBit = currentBit;
            }
            return decompressedData;
        }

        // 3. فك ضغط DPCM (يحتاج عدد البتات وعدد العينات الأصلي)
        public static short[] DecompressDPCM(byte[] compressedData, int bits, int originalLength)
        {
            int levels, center, scale;
            GetDpcmParams(bits, out levels, out center, out scale);

            int available = (bits == 8) ? compressedData.Length : compressedData.Length * 2;
            int n = Math.Min(originalLength, available);

            short[] decompressedData = new short[n];
            int predicted = 0;

            for (int i = 0; i < n; i++)
            {
                int q;
                if (bits == 8) q = compressedData[i];
                else q = (i % 2 == 0) ? (compressedData[i / 2] >> 4) : (compressedData[i / 2] & 0x0F);

                predicted = ClampToShort(predicted + ((q - center) * scale));
                decompressedData[i] = (short)predicted;
            }
            return decompressedData;
        }

        // دالة مساعدة لرفع معدل العينات (Upsampling)
        public static short[] ApplyUpsampling(short[] downsampledSamples, int factor)
        {
            if (factor <= 1) return downsampledSamples;

            short[] upsampled = new short[downsampledSamples.Length * factor];
            for (int i = 0; i < downsampledSamples.Length; i++)
            {
                for (int j = 0; j < factor; j++)
                {
                    upsampled[i * factor + j] = downsampledSamples[i];
                }
            }
            return upsampled;
        }
    }
}