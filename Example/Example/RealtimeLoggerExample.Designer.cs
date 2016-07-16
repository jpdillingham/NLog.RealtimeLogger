namespace RealtimeLogger
{
    partial class RealtimeLoggerExample
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
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.btnAddLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLogs
            // 
            this.txtLogs.Location = new System.Drawing.Point(12, 12);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.Size = new System.Drawing.Size(722, 378);
            this.txtLogs.TabIndex = 0;
            // 
            // btnAddLog
            // 
            this.btnAddLog.Location = new System.Drawing.Point(12, 396);
            this.btnAddLog.Name = "btnAddLog";
            this.btnAddLog.Size = new System.Drawing.Size(722, 29);
            this.btnAddLog.TabIndex = 1;
            this.btnAddLog.Text = "Add Log";
            this.btnAddLog.UseVisualStyleBackColor = true;
            this.btnAddLog.Click += new System.EventHandler(this.btnAddLog_Click);
            // 
            // RealtimeLoggerExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 436);
            this.Controls.Add(this.btnAddLog);
            this.Controls.Add(this.txtLogs);
            this.Name = "RealtimeLoggerExample";
            this.Text = "RealtimeLogger Example";
            this.Load += new System.EventHandler(this.RealtimeLoggerExample_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLogs;
        private System.Windows.Forms.Button btnAddLog;
    }
}

