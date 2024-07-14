namespace WindowsFormsApp1
{
    partial class frmUserInfo
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
            this.usPersonDetails1 = new WindowsFormsApp1.UsPersonDetails();
            this.usUserInfo1 = new WindowsFormsApp1.UsUserInfo();
            this.SuspendLayout();
            // 
            // usPersonDetails1
            // 
            this.usPersonDetails1.BackColor = System.Drawing.Color.White;
            this.usPersonDetails1.Location = new System.Drawing.Point(0, 1);
            this.usPersonDetails1.Name = "usPersonDetails1";
            this.usPersonDetails1.Size = new System.Drawing.Size(992, 429);
            this.usPersonDetails1.TabIndex = 0;
            // 
            // usUserInfo1
            // 
            this.usUserInfo1.Location = new System.Drawing.Point(58, 430);
            this.usUserInfo1.Name = "usUserInfo1";
            this.usUserInfo1.Size = new System.Drawing.Size(869, 150);
            this.usUserInfo1.TabIndex = 1;
            // 
            // frmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(994, 592);
            this.Controls.Add(this.usUserInfo1);
            this.Controls.Add(this.usPersonDetails1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUserInfo";
            this.Text = "frmUserInfo";
            this.Load += new System.EventHandler(this.frmUserInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UsPersonDetails usPersonDetails1;
        private UsUserInfo usUserInfo1;
    }
}