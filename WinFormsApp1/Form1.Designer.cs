namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.LightGray;
            label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Image = Properties.Resources.Sem_título;
            label1.Location = new Point(0, -1);
            label1.MinimumSize = new Size(64, 64);
            label1.Name = "label1";
            label1.Size = new Size(64, 64);
            label1.TabIndex = 1;
            label1.Text = "R";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click_2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(64, 61);
            Controls.Add(label1);
            ForeColor = SystemColors.Info;
            MaximizeBox = false;
            MaximumSize = new Size(80, 100);
            MinimizeBox = false;
            MinimumSize = new Size(64, 64);
            Name = "Form1";
            Opacity = 0.7D;
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
    }
}
