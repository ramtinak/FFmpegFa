namespace FFmpegFaWinform
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSubtitleFile1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSubtitleFile2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAudioFile1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAudioFile2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnConvertVideo = new System.Windows.Forms.Button();
            this.btnConvertVideoWithSettings = new System.Windows.Forms.Button();
            this.btnConvertAudio = new System.Windows.Forms.Button();
            this.btnConvertAudioWithSettings = new System.Windows.Forms.Button();
            this.btnRemoveAudioFromVideo = new System.Windows.Forms.Button();
            this.btnExtractAudioFromVideo = new System.Windows.Forms.Button();
            this.btnResizeVideo = new System.Windows.Forms.Button();
            this.btnCropVideoOrAudio = new System.Windows.Forms.Button();
            this.btnBurnSubtitleIntoVideo = new System.Windows.Forms.Button();
            this.btnMergeSubtitlesToVideo = new System.Windows.Forms.Button();
            this.btnMergeAudioToVideo = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.txtProgress = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input file:";
            // 
            // txtInputFile
            // 
            this.txtInputFile.Location = new System.Drawing.Point(85, 18);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(278, 20);
            this.txtInputFile.TabIndex = 1;
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(85, 48);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(278, 20);
            this.txtOutputFile.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Output file:";
            // 
            // txtSubtitleFile1
            // 
            this.txtSubtitleFile1.Location = new System.Drawing.Point(85, 18);
            this.txtSubtitleFile1.Name = "txtSubtitleFile1";
            this.txtSubtitleFile1.Size = new System.Drawing.Size(278, 20);
            this.txtSubtitleFile1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Subtitle file 1:";
            // 
            // txtSubtitleFile2
            // 
            this.txtSubtitleFile2.Location = new System.Drawing.Point(85, 48);
            this.txtSubtitleFile2.Name = "txtSubtitleFile2";
            this.txtSubtitleFile2.Size = new System.Drawing.Size(278, 20);
            this.txtSubtitleFile2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Subtitle file 2:";
            // 
            // txtAudioFile1
            // 
            this.txtAudioFile1.Location = new System.Drawing.Point(85, 20);
            this.txtAudioFile1.Name = "txtAudioFile1";
            this.txtAudioFile1.Size = new System.Drawing.Size(278, 20);
            this.txtAudioFile1.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Audio file 1:";
            // 
            // txtAudioFile2
            // 
            this.txtAudioFile2.Location = new System.Drawing.Point(85, 50);
            this.txtAudioFile2.Name = "txtAudioFile2";
            this.txtAudioFile2.Size = new System.Drawing.Size(278, 20);
            this.txtAudioFile2.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Audio file 2:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtInputFile);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtOutputFile);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 80);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input and Output files";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtSubtitleFile1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtSubtitleFile2);
            this.groupBox2.Location = new System.Drawing.Point(6, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 85);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Subtitle files";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtAudioFile1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtAudioFile2);
            this.groupBox3.Location = new System.Drawing.Point(6, 180);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(369, 83);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Audio files";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnMergeAudioToVideo);
            this.groupBox4.Controls.Add(this.btnMergeSubtitlesToVideo);
            this.groupBox4.Controls.Add(this.btnBurnSubtitleIntoVideo);
            this.groupBox4.Controls.Add(this.btnCropVideoOrAudio);
            this.groupBox4.Controls.Add(this.btnResizeVideo);
            this.groupBox4.Controls.Add(this.btnExtractAudioFromVideo);
            this.groupBox4.Controls.Add(this.btnRemoveAudioFromVideo);
            this.groupBox4.Controls.Add(this.btnConvertAudioWithSettings);
            this.groupBox4.Controls.Add(this.btnConvertAudio);
            this.groupBox4.Controls.Add(this.btnConvertVideoWithSettings);
            this.groupBox4.Controls.Add(this.btnConvertVideo);
            this.groupBox4.Location = new System.Drawing.Point(6, 269);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(369, 196);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            // 
            // btnConvertVideo
            // 
            this.btnConvertVideo.Location = new System.Drawing.Point(10, 19);
            this.btnConvertVideo.Name = "btnConvertVideo";
            this.btnConvertVideo.Size = new System.Drawing.Size(170, 23);
            this.btnConvertVideo.TabIndex = 0;
            this.btnConvertVideo.Text = "Convert video";
            this.btnConvertVideo.UseVisualStyleBackColor = true;
            this.btnConvertVideo.Click += new System.EventHandler(this.OnConvertVideoClick);
            // 
            // btnConvertVideoWithSettings
            // 
            this.btnConvertVideoWithSettings.Location = new System.Drawing.Point(186, 19);
            this.btnConvertVideoWithSettings.Name = "btnConvertVideoWithSettings";
            this.btnConvertVideoWithSettings.Size = new System.Drawing.Size(170, 23);
            this.btnConvertVideoWithSettings.TabIndex = 1;
            this.btnConvertVideoWithSettings.Text = "Convert video with settings";
            this.btnConvertVideoWithSettings.UseVisualStyleBackColor = true;
            this.btnConvertVideoWithSettings.Click += new System.EventHandler(this.OnConvertVideoWithSettingsClick);
            // 
            // btnConvertAudio
            // 
            this.btnConvertAudio.Location = new System.Drawing.Point(10, 48);
            this.btnConvertAudio.Name = "btnConvertAudio";
            this.btnConvertAudio.Size = new System.Drawing.Size(170, 23);
            this.btnConvertAudio.TabIndex = 2;
            this.btnConvertAudio.Text = "Convert audio";
            this.btnConvertAudio.UseVisualStyleBackColor = true;
            this.btnConvertAudio.Click += new System.EventHandler(this.OnConvertAudioClick);
            // 
            // btnConvertAudioWithSettings
            // 
            this.btnConvertAudioWithSettings.Location = new System.Drawing.Point(186, 48);
            this.btnConvertAudioWithSettings.Name = "btnConvertAudioWithSettings";
            this.btnConvertAudioWithSettings.Size = new System.Drawing.Size(170, 23);
            this.btnConvertAudioWithSettings.TabIndex = 3;
            this.btnConvertAudioWithSettings.Text = "Convert audio with settings";
            this.btnConvertAudioWithSettings.UseVisualStyleBackColor = true;
            this.btnConvertAudioWithSettings.Click += new System.EventHandler(this.OnConvertAudioWithSettingsClick);
            // 
            // btnRemoveAudioFromVideo
            // 
            this.btnRemoveAudioFromVideo.Location = new System.Drawing.Point(10, 77);
            this.btnRemoveAudioFromVideo.Name = "btnRemoveAudioFromVideo";
            this.btnRemoveAudioFromVideo.Size = new System.Drawing.Size(170, 23);
            this.btnRemoveAudioFromVideo.TabIndex = 4;
            this.btnRemoveAudioFromVideo.Text = "Remove audio from video";
            this.btnRemoveAudioFromVideo.UseVisualStyleBackColor = true;
            this.btnRemoveAudioFromVideo.Click += new System.EventHandler(this.OnRemoveAudioFromVideoClick);
            // 
            // btnExtractAudioFromVideo
            // 
            this.btnExtractAudioFromVideo.Location = new System.Drawing.Point(186, 77);
            this.btnExtractAudioFromVideo.Name = "btnExtractAudioFromVideo";
            this.btnExtractAudioFromVideo.Size = new System.Drawing.Size(170, 23);
            this.btnExtractAudioFromVideo.TabIndex = 5;
            this.btnExtractAudioFromVideo.Text = "Extract audio from video";
            this.btnExtractAudioFromVideo.UseVisualStyleBackColor = true;
            this.btnExtractAudioFromVideo.Click += new System.EventHandler(this.OnExtractAudioFromVideoClick);
            // 
            // btnResizeVideo
            // 
            this.btnResizeVideo.Location = new System.Drawing.Point(10, 106);
            this.btnResizeVideo.Name = "btnResizeVideo";
            this.btnResizeVideo.Size = new System.Drawing.Size(170, 23);
            this.btnResizeVideo.TabIndex = 6;
            this.btnResizeVideo.Text = "Resize video";
            this.btnResizeVideo.UseVisualStyleBackColor = true;
            this.btnResizeVideo.Click += new System.EventHandler(this.OnResizeVideoClick);
            // 
            // btnCropVideoOrAudio
            // 
            this.btnCropVideoOrAudio.Location = new System.Drawing.Point(186, 106);
            this.btnCropVideoOrAudio.Name = "btnCropVideoOrAudio";
            this.btnCropVideoOrAudio.Size = new System.Drawing.Size(170, 23);
            this.btnCropVideoOrAudio.TabIndex = 7;
            this.btnCropVideoOrAudio.Text = "Crop video or audio";
            this.btnCropVideoOrAudio.UseVisualStyleBackColor = true;
            this.btnCropVideoOrAudio.Click += new System.EventHandler(this.OnCropVideoOrAudioClick);
            // 
            // btnBurnSubtitleIntoVideo
            // 
            this.btnBurnSubtitleIntoVideo.Location = new System.Drawing.Point(10, 135);
            this.btnBurnSubtitleIntoVideo.Name = "btnBurnSubtitleIntoVideo";
            this.btnBurnSubtitleIntoVideo.Size = new System.Drawing.Size(170, 23);
            this.btnBurnSubtitleIntoVideo.TabIndex = 8;
            this.btnBurnSubtitleIntoVideo.Text = "Burn subtitle into video";
            this.btnBurnSubtitleIntoVideo.UseVisualStyleBackColor = true;
            this.btnBurnSubtitleIntoVideo.Click += new System.EventHandler(this.OnBurnSubtitleIntoVideoClick);
            // 
            // btnMergeSubtitlesToVideo
            // 
            this.btnMergeSubtitlesToVideo.Location = new System.Drawing.Point(186, 135);
            this.btnMergeSubtitlesToVideo.Name = "btnMergeSubtitlesToVideo";
            this.btnMergeSubtitlesToVideo.Size = new System.Drawing.Size(170, 23);
            this.btnMergeSubtitlesToVideo.TabIndex = 9;
            this.btnMergeSubtitlesToVideo.Text = "Merge subtitles to video";
            this.btnMergeSubtitlesToVideo.UseVisualStyleBackColor = true;
            this.btnMergeSubtitlesToVideo.Click += new System.EventHandler(this.OnMergeSubtitlesToVideoClick);
            // 
            // btnMergeAudioToVideo
            // 
            this.btnMergeAudioToVideo.Location = new System.Drawing.Point(105, 164);
            this.btnMergeAudioToVideo.Name = "btnMergeAudioToVideo";
            this.btnMergeAudioToVideo.Size = new System.Drawing.Size(170, 23);
            this.btnMergeAudioToVideo.TabIndex = 10;
            this.btnMergeAudioToVideo.Text = "Merge audios to video";
            this.btnMergeAudioToVideo.UseVisualStyleBackColor = true;
            this.btnMergeAudioToVideo.Click += new System.EventHandler(this.OnMergeAudioToVideoClick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(2, 473);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(379, 23);
            this.progressBar.TabIndex = 16;
            // 
            // txtProgress
            // 
            this.txtProgress.AutoSize = true;
            this.txtProgress.BackColor = System.Drawing.Color.Transparent;
            this.txtProgress.Location = new System.Drawing.Point(3, 501);
            this.txtProgress.Name = "txtProgress";
            this.txtProgress.Size = new System.Drawing.Size(76, 13);
            this.txtProgress.TabIndex = 17;
            this.txtProgress.Text = "Progress result";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(383, 518);
            this.Controls.Add(this.txtProgress);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FFmpegFa Winform";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSubtitleFile1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSubtitleFile2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAudioFile1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAudioFile2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnMergeAudioToVideo;
        private System.Windows.Forms.Button btnMergeSubtitlesToVideo;
        private System.Windows.Forms.Button btnBurnSubtitleIntoVideo;
        private System.Windows.Forms.Button btnCropVideoOrAudio;
        private System.Windows.Forms.Button btnResizeVideo;
        private System.Windows.Forms.Button btnExtractAudioFromVideo;
        private System.Windows.Forms.Button btnRemoveAudioFromVideo;
        private System.Windows.Forms.Button btnConvertAudioWithSettings;
        private System.Windows.Forms.Button btnConvertAudio;
        private System.Windows.Forms.Button btnConvertVideoWithSettings;
        private System.Windows.Forms.Button btnConvertVideo;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label txtProgress;
    }
}

