namespace Multithreading
{
    partial class BicolorSphere
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.BlueLab = new System.Windows.Forms.Label();
            this.RedLab6 = new System.Windows.Forms.Label();
            this.RedLab5 = new System.Windows.Forms.Label();
            this.RedLab4 = new System.Windows.Forms.Label();
            this.RedLab3 = new System.Windows.Forms.Label();
            this.RedLab2 = new System.Windows.Forms.Label();
            this.RedLab1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.BlueLab);
            this.groupBox1.Controls.Add(this.RedLab6);
            this.groupBox1.Controls.Add(this.RedLab5);
            this.groupBox1.Controls.Add(this.RedLab4);
            this.groupBox1.Controls.Add(this.RedLab3);
            this.groupBox1.Controls.Add(this.RedLab2);
            this.groupBox1.Controls.Add(this.RedLab1);
            this.groupBox1.Location = new System.Drawing.Point(41, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(718, 343);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "双色球";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(414, 212);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(91, 32);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(93, 212);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(91, 32);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // BlueLab
            // 
            this.BlueLab.AutoSize = true;
            this.BlueLab.ForeColor = System.Drawing.Color.Blue;
            this.BlueLab.Location = new System.Drawing.Point(617, 129);
            this.BlueLab.Name = "BlueLab";
            this.BlueLab.Size = new System.Drawing.Size(23, 15);
            this.BlueLab.TabIndex = 6;
            this.BlueLab.Text = "00";
            // 
            // RedLab6
            // 
            this.RedLab6.AutoSize = true;
            this.RedLab6.ForeColor = System.Drawing.Color.Red;
            this.RedLab6.Location = new System.Drawing.Point(513, 129);
            this.RedLab6.Name = "RedLab6";
            this.RedLab6.Size = new System.Drawing.Size(23, 15);
            this.RedLab6.TabIndex = 5;
            this.RedLab6.Text = "00";
            // 
            // RedLab5
            // 
            this.RedLab5.AutoSize = true;
            this.RedLab5.ForeColor = System.Drawing.Color.Red;
            this.RedLab5.Location = new System.Drawing.Point(422, 129);
            this.RedLab5.Name = "RedLab5";
            this.RedLab5.Size = new System.Drawing.Size(23, 15);
            this.RedLab5.TabIndex = 4;
            this.RedLab5.Text = "00";
            // 
            // RedLab4
            // 
            this.RedLab4.AutoSize = true;
            this.RedLab4.ForeColor = System.Drawing.Color.Red;
            this.RedLab4.Location = new System.Drawing.Point(331, 129);
            this.RedLab4.Name = "RedLab4";
            this.RedLab4.Size = new System.Drawing.Size(23, 15);
            this.RedLab4.TabIndex = 3;
            this.RedLab4.Text = "00";
            // 
            // RedLab3
            // 
            this.RedLab3.AutoSize = true;
            this.RedLab3.ForeColor = System.Drawing.Color.Red;
            this.RedLab3.Location = new System.Drawing.Point(240, 129);
            this.RedLab3.Name = "RedLab3";
            this.RedLab3.Size = new System.Drawing.Size(23, 15);
            this.RedLab3.TabIndex = 2;
            this.RedLab3.Text = "00";
            // 
            // RedLab2
            // 
            this.RedLab2.AutoSize = true;
            this.RedLab2.ForeColor = System.Drawing.Color.Red;
            this.RedLab2.Location = new System.Drawing.Point(149, 129);
            this.RedLab2.Name = "RedLab2";
            this.RedLab2.Size = new System.Drawing.Size(23, 15);
            this.RedLab2.TabIndex = 1;
            this.RedLab2.Text = "00";
            // 
            // RedLab1
            // 
            this.RedLab1.AutoSize = true;
            this.RedLab1.ForeColor = System.Drawing.Color.Red;
            this.RedLab1.Location = new System.Drawing.Point(58, 129);
            this.RedLab1.Name = "RedLab1";
            this.RedLab1.Size = new System.Drawing.Size(23, 15);
            this.RedLab1.TabIndex = 0;
            this.RedLab1.Text = "00";
            // 
            // BicolorSphere
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "BicolorSphere";
            this.Text = "BicolorSphere";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label BlueLab;
        private System.Windows.Forms.Label RedLab6;
        private System.Windows.Forms.Label RedLab5;
        private System.Windows.Forms.Label RedLab4;
        private System.Windows.Forms.Label RedLab3;
        private System.Windows.Forms.Label RedLab2;
        private System.Windows.Forms.Label RedLab1;
    }
}