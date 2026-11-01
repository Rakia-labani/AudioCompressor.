namespace AudioCompressor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.btnLoad = new System.Windows.Forms.Button();
            this.panelDropZone = new System.Windows.Forms.Panel();
            this.lblDropText = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblProperties = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboDownsampling = new System.Windows.Forms.ComboBox();
            this.comboAlgorithm = new System.Windows.Forms.ComboBox();
            this.panelQuantBits = new System.Windows.Forms.Panel();
            this.comboQuantBits = new System.Windows.Forms.ComboBox();
            this.panelStepSize = new System.Windows.Forms.Panel();
            this.lblStepVal = new System.Windows.Forms.Label();
            this.trackStepSize = new System.Windows.Forms.TrackBar();
            this.btnCompress = new System.Windows.Forms.Button();
            this.btnDecompress = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chartPerformance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelDropZone.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelQuantBits.SuspendLayout();
            this.panelStepSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackStepSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.Khaki;
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.ForeColor = System.Drawing.Color.Black;
            this.btnLoad.Location = new System.Drawing.Point(1249, 522);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(159, 40);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "فتح ملف صوتي";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // panelDropZone
            // 
            this.panelDropZone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panelDropZone.Controls.Add(this.lblDropText);
            this.panelDropZone.Controls.Add(this.lblFileName);
            this.panelDropZone.ForeColor = System.Drawing.Color.Gray;
            this.panelDropZone.Location = new System.Drawing.Point(424, 495);
            this.panelDropZone.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panelDropZone.Name = "panelDropZone";
            this.panelDropZone.Size = new System.Drawing.Size(808, 101);
            this.panelDropZone.TabIndex = 1;
            this.panelDropZone.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelDropZone_DragDrop);
            this.panelDropZone.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelDropZone_DragEnter);
            // 
            // lblDropText
            // 
            this.lblDropText.AutoSize = true;
            this.lblDropText.ForeColor = System.Drawing.Color.Gray;
            this.lblDropText.Location = new System.Drawing.Point(326, 50);
            this.lblDropText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDropText.Name = "lblDropText";
            this.lblDropText.Size = new System.Drawing.Size(114, 17);
            this.lblDropText.TabIndex = 2;
            this.lblDropText.Text = "اسحب ملف وأفلته";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(695, 84);
            this.lblFileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(109, 17);
            this.lblFileName.TabIndex = 3;
            this.lblFileName.Text = "لم يتم اختيار ملف";
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.ForeColor = System.Drawing.Color.Black;
            this.btnPlay.Location = new System.Drawing.Point(524, 624);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(162, 43);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "▶ تشغيل المعاينة";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.ForeColor = System.Drawing.Color.Black;
            this.btnStop.Location = new System.Drawing.Point(969, 624);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(162, 39);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "⏹ إيقاف";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.groupBox1.Controls.Add(this.lblProperties);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Location = new System.Drawing.Point(13, 29);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox1.Size = new System.Drawing.Size(261, 163);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "خصائص الملف الصوتي";
            // 
            // lblProperties
            // 
            this.lblProperties.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProperties.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblProperties.Location = new System.Drawing.Point(35, 40);
            this.lblProperties.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProperties.Name = "lblProperties";
            this.lblProperties.Size = new System.Drawing.Size(193, 100);
            this.lblProperties.TabIndex = 0;
            this.lblProperties.Text = "لم يتم تحميل ملف بعد";
            this.lblProperties.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboDownsampling);
            this.groupBox2.Controls.Add(this.comboAlgorithm);
            this.groupBox2.Controls.Add(this.panelQuantBits);
            this.groupBox2.Controls.Add(this.panelStepSize);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox2.Location = new System.Drawing.Point(13, 211);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupBox2.Size = new System.Drawing.Size(294, 344);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "إعدادات الضغط";
            // 
            // comboDownsampling
            // 
            this.comboDownsampling.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboDownsampling.FormattingEnabled = true;
            this.comboDownsampling.Items.AddRange(new object[] {
            "1x (أصلي) ",
            "2x 4",
            "x"});
            this.comboDownsampling.Location = new System.Drawing.Point(12, 289);
            this.comboDownsampling.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.comboDownsampling.Name = "comboDownsampling";
            this.comboDownsampling.Size = new System.Drawing.Size(121, 26);
            this.comboDownsampling.TabIndex = 10;
            // 
            // comboAlgorithm
            // 
            this.comboAlgorithm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboAlgorithm.FormattingEnabled = true;
            this.comboAlgorithm.Items.AddRange(new object[] {
            "Delta Modulation (DM)",
            "Adaptive Delta Modulation (ADM)",
            "DPCM"});
            this.comboAlgorithm.Location = new System.Drawing.Point(8, 69);
            this.comboAlgorithm.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.comboAlgorithm.Name = "comboAlgorithm";
            this.comboAlgorithm.Size = new System.Drawing.Size(253, 26);
            this.comboAlgorithm.TabIndex = 0;
            this.comboAlgorithm.SelectedIndexChanged += new System.EventHandler(this.comboAlgorithm_SelectedIndexChanged);
            // 
            // panelQuantBits
            // 
            this.panelQuantBits.Controls.Add(this.label3);
            this.panelQuantBits.Controls.Add(this.comboQuantBits);
            this.panelQuantBits.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelQuantBits.Location = new System.Drawing.Point(8, 214);
            this.panelQuantBits.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panelQuantBits.Name = "panelQuantBits";
            this.panelQuantBits.Size = new System.Drawing.Size(253, 51);
            this.panelQuantBits.TabIndex = 9;
            // 
            // comboQuantBits
            // 
            this.comboQuantBits.FormattingEnabled = true;
            this.comboQuantBits.Items.AddRange(new object[] {
            "4-bit",
            "8-bit"});
            this.comboQuantBits.Location = new System.Drawing.Point(4, 18);
            this.comboQuantBits.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.comboQuantBits.Name = "comboQuantBits";
            this.comboQuantBits.Size = new System.Drawing.Size(121, 26);
            this.comboQuantBits.TabIndex = 0;
            // 
            // panelStepSize
            // 
            this.panelStepSize.Controls.Add(this.label2);
            this.panelStepSize.Controls.Add(this.lblStepVal);
            this.panelStepSize.Controls.Add(this.trackStepSize);
            this.panelStepSize.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelStepSize.Location = new System.Drawing.Point(8, 108);
            this.panelStepSize.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.panelStepSize.Name = "panelStepSize";
            this.panelStepSize.Size = new System.Drawing.Size(253, 89);
            this.panelStepSize.TabIndex = 8;
            // 
            // lblStepVal
            // 
            this.lblStepVal.AutoSize = true;
            this.lblStepVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblStepVal.Location = new System.Drawing.Point(132, 34);
            this.lblStepVal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStepVal.Name = "lblStepVal";
            this.lblStepVal.Size = new System.Drawing.Size(23, 18);
            this.lblStepVal.TabIndex = 1;
            this.lblStepVal.Text = "%";
            // 
            // trackStepSize
            // 
            this.trackStepSize.Location = new System.Drawing.Point(10, 23);
            this.trackStepSize.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.trackStepSize.Maximum = 5000;
            this.trackStepSize.Minimum = 100;
            this.trackStepSize.Name = "trackStepSize";
            this.trackStepSize.Size = new System.Drawing.Size(124, 56);
            this.trackStepSize.TabIndex = 0;
            this.trackStepSize.Value = 500;
            this.trackStepSize.Scroll += new System.EventHandler(this.trackStepSize_Scroll);
            // 
            // btnCompress
            // 
            this.btnCompress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompress.ForeColor = System.Drawing.Color.Black;
            this.btnCompress.Location = new System.Drawing.Point(208, 583);
            this.btnCompress.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnCompress.Name = "btnCompress";
            this.btnCompress.Size = new System.Drawing.Size(112, 42);
            this.btnCompress.TabIndex = 8;
            this.btnCompress.Text = "بدء الضغط";
            this.btnCompress.UseVisualStyleBackColor = false;
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
            // 
            // btnDecompress
            // 
            this.btnDecompress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDecompress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecompress.ForeColor = System.Drawing.Color.Black;
            this.btnDecompress.Location = new System.Drawing.Point(13, 583);
            this.btnDecompress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDecompress.Name = "btnDecompress";
            this.btnDecompress.Size = new System.Drawing.Size(110, 42);
            this.btnDecompress.TabIndex = 9;
            this.btnDecompress.Text = "فك الضغط";
            this.btnDecompress.UseVisualStyleBackColor = false;
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(743, 700);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(162, 39);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "🔄 إعادة ضبط ";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(79, 660);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(162, 39);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "حفظ الملف المضغوط";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.progressBar1.Location = new System.Drawing.Point(424, 92);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(808, 39);
            this.progressBar1.TabIndex = 12;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(370, 687);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(162, 39);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "❌ إلغاء العملية";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // chartPerformance
            // 
            this.chartPerformance.BackColor = System.Drawing.Color.DimGray;
            this.chartPerformance.BorderlineColor = System.Drawing.Color.Red;
            chartArea3.AxisX2.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.Name = "ChartArea1";
            this.chartPerformance.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartPerformance.Legends.Add(legend3);
            this.chartPerformance.Location = new System.Drawing.Point(424, 154);
            this.chartPerformance.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartPerformance.Name = "chartPerformance";
            this.chartPerformance.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            this.chartPerformance.Size = new System.Drawing.Size(808, 296);
            this.chartPerformance.TabIndex = 14;
            this.chartPerformance.Text = "chart1";
            this.chartPerformance.Click += new System.EventHandler(this.chartPerformance_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnCompress);
            this.panel1.Controls.Add(this.btnDecompress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 753);
            this.panel1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(192, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 18);
            this.label1.TabIndex = 16;
            this.label1.Text = "الخوارزمية";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 18);
            this.label2.TabIndex = 16;
            this.label2.Text = "حجم الخطوة";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 18);
            this.label3.TabIndex = 17;
            this.label3.Text = "مستوى التكميم";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(158, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 18;
            this.label4.Text = "تخفيض العينات";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 22.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(382, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 43);
            this.label5.TabIndex = 16;
            this.label5.Text = "AudioLab";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1421, 753);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chartPerformance);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.panelDropZone);
            this.Controls.Add(this.btnLoad);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "Form1";
            this.Text = "   ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelDropZone.ResumeLayout(false);
            this.panelDropZone.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelQuantBits.ResumeLayout(false);
            this.panelQuantBits.PerformLayout();
            this.panelStepSize.ResumeLayout(false);
            this.panelStepSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackStepSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformance)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Panel panelDropZone;
        private System.Windows.Forms.Label lblDropText;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblProperties;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboAlgorithm;
        private System.Windows.Forms.Panel panelStepSize;
        private System.Windows.Forms.Label lblStepVal;
        private System.Windows.Forms.TrackBar trackStepSize;
        private System.Windows.Forms.ComboBox comboDownsampling;
        private System.Windows.Forms.Panel panelQuantBits;
        private System.Windows.Forms.ComboBox comboQuantBits;
        private System.Windows.Forms.Button btnCompress;
        private System.Windows.Forms.Button btnDecompress;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPerformance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

