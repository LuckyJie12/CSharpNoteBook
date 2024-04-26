namespace Multithreading
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
            this.btnAwait = new System.Windows.Forms.Button();
            this.ThreadCore = new System.Windows.Forms.Button();
            this.ParallelButton = new System.Windows.Forms.Button();
            this.TaskButton = new System.Windows.Forms.Button();
            this.ThreadPoolButtom = new System.Windows.Forms.Button();
            this.ThreadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAwait
            // 
            this.btnAwait.Location = new System.Drawing.Point(417, 154);
            this.btnAwait.Name = "btnAwait";
            this.btnAwait.Size = new System.Drawing.Size(102, 36);
            this.btnAwait.TabIndex = 11;
            this.btnAwait.Text = "AwaitAsync";
            this.btnAwait.UseVisualStyleBackColor = true;
            this.btnAwait.Click += new System.EventHandler(this.btnAwait_Click);
            // 
            // ThreadCore
            // 
            this.ThreadCore.Location = new System.Drawing.Point(417, 46);
            this.ThreadCore.Name = "ThreadCore";
            this.ThreadCore.Size = new System.Drawing.Size(102, 36);
            this.ThreadCore.TabIndex = 10;
            this.ThreadCore.Text = "线程扩展";
            this.ThreadCore.UseVisualStyleBackColor = true;
            this.ThreadCore.Click += new System.EventHandler(this.ThreadCore_Click);
            // 
            // ParallelButton
            // 
            this.ParallelButton.Location = new System.Drawing.Point(105, 357);
            this.ParallelButton.Name = "ParallelButton";
            this.ParallelButton.Size = new System.Drawing.Size(102, 36);
            this.ParallelButton.TabIndex = 9;
            this.ParallelButton.Text = "Parallel";
            this.ParallelButton.UseVisualStyleBackColor = true;
            this.ParallelButton.Click += new System.EventHandler(this.ParallelButton_Click);
            // 
            // TaskButton
            // 
            this.TaskButton.Location = new System.Drawing.Point(105, 262);
            this.TaskButton.Name = "TaskButton";
            this.TaskButton.Size = new System.Drawing.Size(102, 36);
            this.TaskButton.TabIndex = 8;
            this.TaskButton.Text = "Task";
            this.TaskButton.UseVisualStyleBackColor = true;
            this.TaskButton.Click += new System.EventHandler(this.TaskButton_Click);
            // 
            // ThreadPoolButtom
            // 
            this.ThreadPoolButtom.Location = new System.Drawing.Point(105, 154);
            this.ThreadPoolButtom.Name = "ThreadPoolButtom";
            this.ThreadPoolButtom.Size = new System.Drawing.Size(102, 36);
            this.ThreadPoolButtom.TabIndex = 7;
            this.ThreadPoolButtom.Text = "ThreadPool";
            this.ThreadPoolButtom.UseVisualStyleBackColor = true;
            this.ThreadPoolButtom.Click += new System.EventHandler(this.ThreadPool_Click);
            // 
            // ThreadButton
            // 
            this.ThreadButton.Location = new System.Drawing.Point(105, 46);
            this.ThreadButton.Name = "ThreadButton";
            this.ThreadButton.Size = new System.Drawing.Size(102, 36);
            this.ThreadButton.TabIndex = 6;
            this.ThreadButton.Text = "Thread";
            this.ThreadButton.UseVisualStyleBackColor = true;
            this.ThreadButton.Click += new System.EventHandler(this.ThreadButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAwait);
            this.Controls.Add(this.ThreadCore);
            this.Controls.Add(this.ParallelButton);
            this.Controls.Add(this.TaskButton);
            this.Controls.Add(this.ThreadPoolButtom);
            this.Controls.Add(this.ThreadButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAwait;
        private System.Windows.Forms.Button ThreadCore;
        private System.Windows.Forms.Button ParallelButton;
        private System.Windows.Forms.Button TaskButton;
        private System.Windows.Forms.Button ThreadPoolButtom;
        private System.Windows.Forms.Button ThreadButton;
    }
}