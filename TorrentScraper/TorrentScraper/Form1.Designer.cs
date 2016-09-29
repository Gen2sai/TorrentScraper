namespace TorrentScraper
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
            this.btnHorribleSubs = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btndamedesuyo = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblHiRes = new System.Windows.Forms.Label();
            this.lblMidRes = new System.Windows.Forms.Label();
            this.lblLowRes = new System.Windows.Forms.Label();
            this.btnCheck = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.HorribleSubsWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblTorStatus = new System.Windows.Forms.Label();
            this.btnTorHiRes = new System.Windows.Forms.Button();
            this.btnTorMidRes = new System.Windows.Forms.Button();
            this.btnTorLowRes = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTorrentPath = new System.Windows.Forms.Button();
            this.txtTorrentPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnHiRes = new System.Windows.Forms.Button();
            this.btnMidRes = new System.Windows.Forms.Button();
            this.btnLowRes = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFilePath = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.CheckingWorker = new System.ComponentModel.BackgroundWorker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkListAnime = new System.Windows.Forms.CheckedListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.damedesuyoWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHorribleSubs
            // 
            this.btnHorribleSubs.Location = new System.Drawing.Point(6, 19);
            this.btnHorribleSubs.Name = "btnHorribleSubs";
            this.btnHorribleSubs.Size = new System.Drawing.Size(75, 23);
            this.btnHorribleSubs.TabIndex = 0;
            this.btnHorribleSubs.Text = "HorribleSubs";
            this.btnHorribleSubs.UseVisualStyleBackColor = true;
            this.btnHorribleSubs.Click += new System.EventHandler(this.btnHorribleSubs_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btndamedesuyo);
            this.groupBox1.Controls.Add(this.btnHorribleSubs);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(90, 441);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SiteListing";
            // 
            // btndamedesuyo
            // 
            this.btndamedesuyo.Location = new System.Drawing.Point(6, 45);
            this.btndamedesuyo.Name = "btndamedesuyo";
            this.btndamedesuyo.Size = new System.Drawing.Size(75, 23);
            this.btndamedesuyo.TabIndex = 1;
            this.btndamedesuyo.Text = "damedesuyo";
            this.btndamedesuyo.UseVisualStyleBackColor = true;
            this.btndamedesuyo.Click += new System.EventHandler(this.btndamedesuyo_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(108, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 441);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AnimeListing";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(463, 416);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblHiRes);
            this.groupBox3.Controls.Add(this.lblMidRes);
            this.groupBox3.Controls.Add(this.lblLowRes);
            this.groupBox3.Controls.Add(this.btnCheck);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Location = new System.Drawing.Point(589, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(369, 110);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CheckList";
            // 
            // lblHiRes
            // 
            this.lblHiRes.AutoSize = true;
            this.lblHiRes.Location = new System.Drawing.Point(6, 71);
            this.lblHiRes.Name = "lblHiRes";
            this.lblHiRes.Size = new System.Drawing.Size(0, 13);
            this.lblHiRes.TabIndex = 4;
            // 
            // lblMidRes
            // 
            this.lblMidRes.AutoSize = true;
            this.lblMidRes.Location = new System.Drawing.Point(6, 58);
            this.lblMidRes.Name = "lblMidRes";
            this.lblMidRes.Size = new System.Drawing.Size(0, 13);
            this.lblMidRes.TabIndex = 3;
            // 
            // lblLowRes
            // 
            this.lblLowRes.AutoSize = true;
            this.lblLowRes.Location = new System.Drawing.Point(6, 45);
            this.lblLowRes.Name = "lblLowRes";
            this.lblLowRes.Size = new System.Drawing.Size(0, 13);
            this.lblLowRes.TabIndex = 2;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(288, 81);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 1;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(357, 20);
            this.textBox1.TabIndex = 0;
            // 
            // HorribleSubsWorker
            // 
            this.HorribleSubsWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.HorribleSubsWorker_DoWork);
            this.HorribleSubsWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.HorribleSubsWorker_RunWorkerCompleted);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblTorStatus);
            this.groupBox4.Controls.Add(this.btnTorHiRes);
            this.groupBox4.Controls.Add(this.btnTorMidRes);
            this.groupBox4.Controls.Add(this.btnTorLowRes);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.btnTorrentPath);
            this.groupBox4.Controls.Add(this.txtTorrentPath);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.lblStatus);
            this.groupBox4.Controls.Add(this.btnHiRes);
            this.groupBox4.Controls.Add(this.btnMidRes);
            this.groupBox4.Controls.Add(this.btnLowRes);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.btnFilePath);
            this.groupBox4.Controls.Add(this.txtFilePath);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(589, 273);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(363, 180);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DownloadList";
            // 
            // lblTorStatus
            // 
            this.lblTorStatus.AutoSize = true;
            this.lblTorStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTorStatus.Location = new System.Drawing.Point(275, 148);
            this.lblTorStatus.Name = "lblTorStatus";
            this.lblTorStatus.Size = new System.Drawing.Size(82, 20);
            this.lblTorStatus.TabIndex = 15;
            this.lblTorStatus.Text = "Pushing ...";
            // 
            // btnTorHiRes
            // 
            this.btnTorHiRes.Location = new System.Drawing.Point(282, 122);
            this.btnTorHiRes.Name = "btnTorHiRes";
            this.btnTorHiRes.Size = new System.Drawing.Size(75, 23);
            this.btnTorHiRes.TabIndex = 14;
            this.btnTorHiRes.Text = "[1080p]";
            this.btnTorHiRes.UseVisualStyleBackColor = true;
            this.btnTorHiRes.Click += new System.EventHandler(this.btnTorHiRes_Click);
            // 
            // btnTorMidRes
            // 
            this.btnTorMidRes.Location = new System.Drawing.Point(185, 122);
            this.btnTorMidRes.Name = "btnTorMidRes";
            this.btnTorMidRes.Size = new System.Drawing.Size(75, 23);
            this.btnTorMidRes.TabIndex = 13;
            this.btnTorMidRes.Text = "[720p]";
            this.btnTorMidRes.UseVisualStyleBackColor = true;
            this.btnTorMidRes.Click += new System.EventHandler(this.btnTorMidRes_Click);
            // 
            // btnTorLowRes
            // 
            this.btnTorLowRes.Location = new System.Drawing.Point(89, 123);
            this.btnTorLowRes.Name = "btnTorLowRes";
            this.btnTorLowRes.Size = new System.Drawing.Size(75, 23);
            this.btnTorLowRes.TabIndex = 12;
            this.btnTorLowRes.Text = "[480p]";
            this.btnTorLowRes.UseVisualStyleBackColor = true;
            this.btnTorLowRes.Click += new System.EventHandler(this.btnTorLowRes_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Push to Torrent";
            // 
            // btnTorrentPath
            // 
            this.btnTorrentPath.Location = new System.Drawing.Point(325, 94);
            this.btnTorrentPath.Name = "btnTorrentPath";
            this.btnTorrentPath.Size = new System.Drawing.Size(32, 23);
            this.btnTorrentPath.TabIndex = 10;
            this.btnTorrentPath.Text = "...";
            this.btnTorrentPath.UseVisualStyleBackColor = true;
            this.btnTorrentPath.Click += new System.EventHandler(this.btnTorrentPath_Click);
            // 
            // txtTorrentPath
            // 
            this.txtTorrentPath.Location = new System.Drawing.Point(89, 97);
            this.txtTorrentPath.Name = "txtTorrentPath";
            this.txtTorrentPath.Size = new System.Drawing.Size(230, 20);
            this.txtTorrentPath.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "UtorrentPath :";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(297, 71);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(60, 20);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Done...";
            // 
            // btnHiRes
            // 
            this.btnHiRes.Location = new System.Drawing.Point(282, 45);
            this.btnHiRes.Name = "btnHiRes";
            this.btnHiRes.Size = new System.Drawing.Size(75, 23);
            this.btnHiRes.TabIndex = 6;
            this.btnHiRes.Text = "[1080p]";
            this.btnHiRes.UseVisualStyleBackColor = true;
            this.btnHiRes.Click += new System.EventHandler(this.btnHiRes_Click);
            // 
            // btnMidRes
            // 
            this.btnMidRes.Location = new System.Drawing.Point(185, 45);
            this.btnMidRes.Name = "btnMidRes";
            this.btnMidRes.Size = new System.Drawing.Size(75, 23);
            this.btnMidRes.TabIndex = 5;
            this.btnMidRes.Text = "[720p]";
            this.btnMidRes.UseVisualStyleBackColor = true;
            this.btnMidRes.Click += new System.EventHandler(this.btnMidRes_Click);
            // 
            // btnLowRes
            // 
            this.btnLowRes.Location = new System.Drawing.Point(89, 45);
            this.btnLowRes.Name = "btnLowRes";
            this.btnLowRes.Size = new System.Drawing.Size(75, 23);
            this.btnLowRes.TabIndex = 4;
            this.btnLowRes.Text = "[480p]";
            this.btnLowRes.UseVisualStyleBackColor = true;
            this.btnLowRes.Click += new System.EventHandler(this.btnLowRes_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Write to Text :";
            // 
            // btnFilePath
            // 
            this.btnFilePath.Location = new System.Drawing.Point(325, 18);
            this.btnFilePath.Name = "btnFilePath";
            this.btnFilePath.Size = new System.Drawing.Size(32, 23);
            this.btnFilePath.TabIndex = 2;
            this.btnFilePath.Text = "...";
            this.btnFilePath.UseVisualStyleBackColor = true;
            this.btnFilePath.Click += new System.EventHandler(this.btnFilePath_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(66, 20);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(253, 20);
            this.txtFilePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "FilePath :";
            // 
            // CheckingWorker
            // 
            this.CheckingWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CheckingWorker_DoWork);
            this.CheckingWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CheckingWorker_RunWorkerCompleted);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkListAnime);
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Location = new System.Drawing.Point(589, 128);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(357, 139);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "DownloadList";
            // 
            // chkListAnime
            // 
            this.chkListAnime.CheckOnClick = true;
            this.chkListAnime.Enabled = false;
            this.chkListAnime.FormattingEnabled = true;
            this.chkListAnime.Location = new System.Drawing.Point(12, 39);
            this.chkListAnime.Name = "chkListAnime";
            this.chkListAnime.Size = new System.Drawing.Size(339, 94);
            this.chkListAnime.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(143, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Enable Download by List";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // damedesuyoWorker
            // 
            this.damedesuyoWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.damedesuyoWorker_DoWork);
            this.damedesuyoWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.damedesuyoWorker_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 465);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHorribleSubs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.ComponentModel.BackgroundWorker HorribleSubsWorker;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblLowRes;
        private System.Windows.Forms.Label lblHiRes;
        private System.Windows.Forms.Label lblMidRes;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnFilePath;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnHiRes;
        private System.Windows.Forms.Button btnMidRes;
        private System.Windows.Forms.Button btnLowRes;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker CheckingWorker;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnTorrentPath;
        private System.Windows.Forms.TextBox txtTorrentPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblTorStatus;
        private System.Windows.Forms.Button btnTorHiRes;
        private System.Windows.Forms.Button btnTorMidRes;
        private System.Windows.Forms.Button btnTorLowRes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox chkListAnime;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btndamedesuyo;
        private System.ComponentModel.BackgroundWorker damedesuyoWorker;
    }
}

