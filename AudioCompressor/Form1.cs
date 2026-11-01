using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace AudioCompressor
{
    public partial class Form1 : Form
    {
        // ⭐ متغيرات عامة للصوت ⭐
        private string currentFilePath = "";
        private AudioFileReader audioFile;
        private WaveOutEvent waveOut;

        // ⭐ متغيرات الإلغاء والسرعة ⭐
        private CancellationTokenSource cts;
        private Stopwatch compressionStopwatch;

        public Form1()
        {
            InitializeComponent();
            panelDropZone.AllowDrop = true;
        }

        // ====================================
        // دوال تحميل الملف (الطلب 1)
        // ====================================
        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "WAV Files|*.wav";
                ofd.Title = "اختر ملف صوتي لمعالجته";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadAudioFile(ofd.FileName);
                }
            }
        }

        private void panelDropZone_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && files[0].EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                {
                    e.Effect = DragDropEffects.Copy;
                    lblDropText.Text = "أفلت الملف هنا!";
                    lblDropText.ForeColor = Color.Green;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        private void panelDropZone_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                LoadAudioFile(files[0]);
            }
            lblDropText.Text = "اسحب ملف WAV وأفلته هنا";
            lblDropText.ForeColor = Color.Gray;
        }

        // ⭐ متغير عام لحفظ العينات الصوتية الخام ⭐
        public short[] RawAudioSamples;

        // ⭐⭐⭐ المتغيرات الجديدة لحفظ إعدادات الضغط ⭐⭐⭐
        private byte[] compressedResultData = null;
        private string usedAlgorithmName = "";
        private int usedStepSize = 0;
        private int usedDownsampleFactor = 1;
        private int usedQuantBits = 8;
        private WaveFormat originalWaveFormat = null;

        private void LoadAudioFile(string filePath)
        {
            try
            {
                StopAndCleanupAudio();
                currentFilePath = filePath;
                lblFileName.Text = "الملف المختار: " + System.IO.Path.GetFileName(filePath);
                panelDropZone.BackColor = Color.LightGreen;

                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
                double fileSizeKB = fileInfo.Length / 1024.0;

                using (var reader = new WaveFileReader(filePath))
                {
                    var format = reader.WaveFormat;
                    originalWaveFormat = format;

                    string sizeStr = $"حجم الملف: {fileSizeKB:F2} KB";
                    string durationStr = $"المدة الزمنية: {reader.TotalTime.ToString(@"mm\:ss\.fff")}";
                    string sampleRateStr = $"معدل أخذ العينات: {format.SampleRate} Hz";
                    string channelsStr = $"عدد القنوات: {format.Channels} ({(format.Channels == 1 ? "Mono" : "Stereo")})";
                    string bitsStr = $"معدل البت: {format.BitsPerSample} bit";
                    string encodingStr = $"نوع الترميز: {format.Encoding}";

                    lblProperties.Text = $"{sizeStr}\n{durationStr}\n{sampleRateStr}\n{channelsStr}\n{bitsStr}\n{encodingStr}";
                }

                using (var audioFile = new AudioFileReader(filePath))
                {
                    float[] floatSamples = new float[audioFile.Length / 4];
                    audioFile.Read(floatSamples, 0, floatSamples.Length);

                    RawAudioSamples = new short[floatSamples.Length];
                    for (int i = 0; i < floatSamples.Length; i++)
                    {
                        float sample = Math.Max(-1f, Math.Min(1f, floatSamples[i]));
                        RawAudioSamples[i] = (short)(sample * 32767f);
                    }
                }

                MessageBox.Show("تم تحميل الملف وتجهيز العينات بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل الملف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====================================
        // دوال تشغيل وإيقاف الصوت
        // ====================================
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                MessageBox.Show("الرجاء تحميل ملف صوتي أولاً!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Paused)
            {
                waveOut.Play();
                return;
            }

            try
            {
                StopAndCleanupAudio();
                audioFile = new AudioFileReader(currentFilePath);
                waveOut = new WaveOutEvent();
                waveOut.Init(audioFile);
                waveOut.PlaybackStopped += WaveOut_PlaybackStopped;
                waveOut.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تشغيل الملف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Pause();
            }
        }

        private void StopAndCleanupAudio()
        {
            if (waveOut != null)
            {
                if (waveOut.PlaybackState != PlaybackState.Stopped)
                {
                    waveOut.Stop();
                }
                waveOut.PlaybackStopped -= WaveOut_PlaybackStopped;
                waveOut.Dispose();
                waveOut = null;
            }

            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
        }

        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            StopAndCleanupAudio();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ⭐ إعداد الرسم البياني ⭐
            // ⭐ إعداد الرسم البياني ⭐
            chartPerformance.Series.Clear(); // مسح أي سلاسل افتراضية

            // 1. سلسلة السرعة
            Series speedSeries = new Series("السرعة"); // اسم بسيط
            speedSeries.ChartType = SeriesChartType.Line;
            speedSeries.Color = Color.YellowGreen;
            speedSeries.BorderWidth = 2;
            chartPerformance.Series.Add(speedSeries);

            // 2. سلسلة نسبة الإنجاز
            Series progressSeries = new Series("الإنجاز"); // اسم بسيط
            progressSeries.ChartType = SeriesChartType.Line;
            progressSeries.Color = Color.Green;
            progressSeries.BorderWidth = 2;
            chartPerformance.Series.Add(progressSeries);

            // إعداد المحاور
            chartPerformance.ChartAreas[0].AxisX.Title = "(الزمن (ثواني";
            chartPerformance.ChartAreas[0].AxisY.Title = "القيمة";
        }

        private void comboAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelStepSize.Visible = false;
            panelQuantBits.Visible = false;

            if (comboAlgorithm.SelectedItem == null) return;
            string selectedAlgo = comboAlgorithm.SelectedItem.ToString();

            if (selectedAlgo == "Delta Modulation (DM)" || selectedAlgo == "Adaptive Delta Modulation (ADM)")
            {
                panelStepSize.Visible = true;
                if (selectedAlgo == "Delta Modulation (DM)")
                {
                    trackStepSize.Value = 500;
                }
                else
                {
                    trackStepSize.Value = 100;
                }
                lblStepVal.Text = trackStepSize.Value.ToString();
            }
            else if (selectedAlgo == "DPCM")
            {
                panelQuantBits.Visible = true;
                comboQuantBits.SelectedItem = "4-bit";
            }
        }

        private void trackStepSize_Scroll(object sender, EventArgs e)
        {
            lblStepVal.Text = trackStepSize.Value.ToString();
        }

        private short[] ApplyDownsampling(short[] originalSamples, int factor)
        {
            if (factor <= 1) return originalSamples;

            short[] downsampled = new short[originalSamples.Length / factor];
            for (int i = 0; i < downsampled.Length; i++)
            {
                downsampled[i] = originalSamples[i * factor];
            }
            return downsampled;
        }

        // ⭐⭐⭐ زر الضغط (محدث لدعم Progress Bar و Cancel) ⭐⭐⭐
        private async void btnCompress_Click(object sender, EventArgs e)
        {
            if (RawAudioSamples == null || RawAudioSamples.Length == 0)
            {
                MessageBox.Show("الرجاء تحميل ملف صوتي أولاً!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboAlgorithm.SelectedIndex == -1)
            {
                MessageBox.Show("الرجاء اختيار خوارزمية ضغط!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int downsampleFactor = 1;
            if (comboDownsampling.SelectedIndex == 1) downsampleFactor = 2;
            else if (comboDownsampling.SelectedIndex == 2) downsampleFactor = 4;

            short[] samplesToCompress = ApplyDownsampling(RawAudioSamples, downsampleFactor);
            string algoName = comboAlgorithm.SelectedItem.ToString();
            int quantBits = (comboQuantBits.SelectedItem != null && comboQuantBits.SelectedItem.ToString() == "4-bit") ? 4 : 8;

            // تجهيز أدوات المراقبة والإلغاء
            cts = new CancellationTokenSource();
            btnCancel.Enabled = true;
            btnCompress.Enabled = false;
            progressBar1.Value = 0;
            // ⭐ تصفير الرسم البياني ⭐
         
            chartPerformance.Series["السرعة"].Points.Clear();
            chartPerformance.Series["الإنجاز"].Points.Clear();
            compressionStopwatch = Stopwatch.StartNew();

            Progress<int> progress = new Progress<int>(percent =>
            {
                progressBar1.Value = Math.Max(0, Math.Min(100, percent));
                double elapsedSeconds = compressionStopwatch.Elapsed.TotalSeconds;
                if (elapsedSeconds > 0)
                {
                    double currentSpeed = (percent / 100.0) * samplesToCompress.Length / elapsedSeconds;


                    // ⭐ إضافة النقاط للرسم البياني ⭐
                 
                    chartPerformance.Series["السرعة"].Points.AddXY(elapsedSeconds, currentSpeed);
                    chartPerformance.Series["الإنجاز"].Points.AddXY(elapsedSeconds, percent);
                }
            });

            try
            {
                byte[] compressedResult = null;

                if (algoName == "Delta Modulation (DM)")
                {
                    int step = trackStepSize.Value;
                    compressedResult = await Task.Run(() => AudioCompressorCore.CompressDM(samplesToCompress, step, cts.Token, progress));
                }
                else if (algoName == "Adaptive Delta Modulation (ADM)")
                {
                    int step = trackStepSize.Value;
                    compressedResult = await Task.Run(() => AudioCompressorCore.CompressADM(samplesToCompress, step, cts.Token, progress));
                }
                else if (algoName == "DPCM")
                {
                    compressedResult = await Task.Run(() => AudioCompressorCore.CompressDPCM(samplesToCompress, quantBits, cts.Token, progress));
                }

                compressionStopwatch.Stop();

                if (compressedResult != null)
                {
                    double originalSizeKB = (samplesToCompress.Length * 2) / 1024.0;
                    double compressedSizeKB = compressedResult.Length / 1024.0;
                    double savings = 100.0 * (1.0 - (compressedSizeKB / originalSizeKB));
                    double elapsedMilliseconds = compressionStopwatch.Elapsed.TotalMilliseconds;

                    string settingsStr = "";
                    if (algoName == "Delta Modulation (DM)" || algoName == "Adaptive Delta Modulation (ADM)")
                    {
                        settingsStr = $"Step Size: {trackStepSize.Value}\n";
                    }
                    else if (algoName == "DPCM")
                    {
                        settingsStr = $"Quantization: {quantBits}-bit\n";
                    }
                    settingsStr += $"Sampling Rate: {originalWaveFormat.SampleRate} Hz\n" +
                                   $"Sample Bit Rate: 16-bit\n" +
                                   $"Downsampling Factor: {downsampleFactor}x";

                    string report = $"تم الضغط بنجاح!\n\n" +
                                    $"الحجم قبل الضغط: {originalSizeKB:F2} KB\n" +
                                    $"الحجم بعد الضغط: {compressedSizeKB:F2} KB\n" +
                                    $"نسبة التوفير: {savings:F2}%\n" +
                                    $"الزمن المستغرق: {elapsedMilliseconds:F2} مللي ثانية\n" +
                                    $"الخوارزمية: {algoName}\n" +
                                    $"إعدادات الخوارزمية:\n{settingsStr}";

                    compressedResultData = compressedResult;
                    usedAlgorithmName = algoName;
                    usedStepSize = trackStepSize.Value;
                    usedDownsampleFactor = downsampleFactor;
                    usedQuantBits = quantBits;

                    MessageBox.Show(report, "تقرير الضغط", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (OperationCanceledException)
            {
                // ⭐ هذا الكود يعمل فقط إذا ضغط المستخدم زر الإلغاء ⭐
                progressBar1.Value = 0;

                // مسح الرسم البياني عند الإلغاء لبدء صفحة جديدة لاحقاً
                // مسح الرسم البياني عند الإلغاء
                chartPerformance.Series["السرعة"].Points.Clear();
                chartPerformance.Series["الإنجاز"].Points.Clear();

                compressedResultData = null; // مسح أي بيانات ناقصة

                // رسالة تبان للمستخدم إنو تم الإلغاء (اختياري)
                MessageBox.Show("تم إلغاء عملية الضغط بنجاح!", "إلغاء", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء الضغط: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCompress.Enabled = true;
                btnCancel.Enabled = false;
                if (cts != null) cts.Dispose();
            }
        }

        // ⭐⭐⭐ زر الإلغاء ⭐⭐⭐
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
            }
        }

        private void btnDecompress_Click(object sender, EventArgs e)
        {
            if (compressedResultData == null)
            {
                MessageBox.Show("الرجاء ضغط الملف أولاً قبل فك ضغطه!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (originalWaveFormat == null)
            {
                MessageBox.Show("خطأ: لم يتم تحديد صيغة الصوت الأصلية!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                short[] decompressedSamples = null;
                int originalDownsampledLength = RawAudioSamples.Length / usedDownsampleFactor;

                if (usedAlgorithmName == "Delta Modulation (DM)")
                {
                    decompressedSamples = AudioCompressorCore.DecompressDM(compressedResultData, usedStepSize, originalDownsampledLength);
                }
                else if (usedAlgorithmName == "Adaptive Delta Modulation (ADM)")
                {
                    decompressedSamples = AudioCompressorCore.DecompressADM(compressedResultData, usedStepSize, originalDownsampledLength);
                }
                else if (usedAlgorithmName == "DPCM")
                {
                    decompressedSamples = AudioCompressorCore.DecompressDPCM(compressedResultData, usedQuantBits, originalDownsampledLength);
                }

                if (usedDownsampleFactor > 1 && decompressedSamples != null)
                {
                    decompressedSamples = AudioCompressorCore.ApplyUpsampling(decompressedSamples, usedDownsampleFactor);
                }

                byte[] pcmBytes = new byte[decompressedSamples.Length * 2];
                Buffer.BlockCopy(decompressedSamples, 0, pcmBytes, 0, pcmBytes.Length);

                StopAndCleanupAudio();

                WaveFormat playbackFormat = new WaveFormat(originalWaveFormat.SampleRate, 16, originalWaveFormat.Channels);

                var waveProvider = new RawSourceWaveStream(
                    new System.IO.MemoryStream(pcmBytes),
                    playbackFormat);

                waveOut = new WaveOutEvent();
                waveOut.Init(waveProvider);
                waveOut.PlaybackStopped += WaveOut_PlaybackStopped;
                waveOut.Play();

                MessageBox.Show("تم فك الضغط بنجاح! جاري تشغيل الصوت الناتج.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء فك الضغط: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            StopAndCleanupAudio();
            // ⭐⭐⭐ تصفير شريط التقدم والرسم البياني ⭐⭐⭐
            progressBar1.Value = 0;
            chartPerformance.Series["السرعة"].Points.Clear();
            chartPerformance.Series["الإنجاز"].Points.Clear();
            // ⭐ مسح متغيرات الضغط وفك الضغط فقط (إلغاء العمليات) ⭐

            compressedResultData = null;
            usedAlgorithmName = "";
            usedStepSize = 0;
            usedDownsampleFactor = 1;

            if (!string.IsNullOrEmpty(currentFilePath) && originalWaveFormat != null)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(currentFilePath);
                double fileSizeKB = fileInfo.Length / 1024.0;

                using (var reader = new WaveFileReader(currentFilePath))
                {
                    var format = reader.WaveFormat;
                    lblProperties.Text = $"حجم الملف: {fileSizeKB:F2} KB\n" +
                                         $"المدة الزمنية: {reader.TotalTime.ToString(@"mm\:ss\.fff")}\n" +
                                         $"معدل أخذ العينات: {format.SampleRate} Hz\n" +
                                         $"عدد القنوات: {format.Channels} ({(format.Channels == 1 ? "Mono" : "Stereo")})\n" +
                                         $"معدل البت: {format.BitsPerSample} bit\n" +
                                         $"نوع الترميز: {format.Encoding}";
                }

                lblFileName.Text = "الملف المختار: " + System.IO.Path.GetFileName(currentFilePath);
                panelDropZone.BackColor = Color.LightGreen;
                MessageBox.Show("تم إعادة الضبط بنجاح! الملف الأصلي لا يزال محملاً.", "إعادة ضبط", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblFileName.Text = "لم يتم اختيار ملف";
                lblProperties.Text = "لم يتم تحميل ملف بعد";
                panelDropZone.BackColor = SystemColors.ActiveBorder;
                lblDropText.Text = "اسحب ملف WAV وأفلته هنا";
                lblDropText.ForeColor = Color.Gray;
            }

            comboAlgorithm.SelectedIndex = -1;
            comboDownsampling.SelectedIndex = 0;
            panelStepSize.Visible = false;
            panelQuantBits.Visible = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (compressedResultData == null)
            {
                MessageBox.Show("الرجاء ضغط الملف أولاً قبل الحفظ!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Compressed Audio Files|*.comp";
                sfd.Title = "حفظ الملف الصوتي بعد الضغط";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.IO.File.WriteAllBytes(sfd.FileName, compressedResultData);
                        MessageBox.Show("تم حفظ الملف المضغوط بنجاح!\nملاحظة: هذا الملف لا يمكن تشغيله بواسطة مشغلات الصوت العادية لأنه مضغوط بخوارزمية مخصصة.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("خطأ في حفظ الملف: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chartPerformance_Click(object sender, EventArgs e)
        {

        }
    }
}