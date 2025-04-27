namespace Курсовая
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtUniversity;
        private System.Windows.Forms.TextBox txtSpecialty;
        private System.Windows.Forms.Button btnFindUniversity;
        private System.Windows.Forms.Button btnFindSpecialty;
        private System.Windows.Forms.Button btnFindMinimalCompetition;
        private System.Windows.Forms.ListBox lstResults;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        
        private void InitializeComponent()
        {
            this.txtUniversity = new System.Windows.Forms.TextBox();
            this.txtSpecialty = new System.Windows.Forms.TextBox();
            this.btnFindUniversity = new System.Windows.Forms.Button();
            this.btnFindSpecialty = new System.Windows.Forms.Button();
            this.btnFindMinimalCompetition = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.SuspendLayout();

            this.txtUniversity.Location = new System.Drawing.Point(12, 12);
            this.txtUniversity.Name = "txtUniversity";
            this.txtUniversity.Size = new System.Drawing.Size(300, 22);
            this.txtUniversity.TabIndex = 0;
            this.txtUniversity.PlaceholderText = "Введіть назву університету";

            this.txtSpecialty.Location = new System.Drawing.Point(12, 40);
            this.txtSpecialty.Name = "txtSpecialty";
            this.txtSpecialty.Size = new System.Drawing.Size(300, 22);
            this.txtSpecialty.TabIndex = 1;
            this.txtSpecialty.PlaceholderText = "Введіть назву спеціальності";

            this.btnFindUniversity.Location = new System.Drawing.Point(330, 10);
            this.btnFindUniversity.Name = "btnFindUniversity";
            this.btnFindUniversity.Size = new System.Drawing.Size(150, 25);
            this.btnFindUniversity.TabIndex = 2;
            this.btnFindUniversity.Text = "Пошук ВУЗу";
            this.btnFindUniversity.UseVisualStyleBackColor = true;
            this.btnFindUniversity.Click += new System.EventHandler(this.btnFindUniversity_Click);

            this.btnFindSpecialty.Location = new System.Drawing.Point(330, 40);
            this.btnFindSpecialty.Name = "btnFindSpecialty";
            this.btnFindSpecialty.Size = new System.Drawing.Size(150, 25);
            this.btnFindSpecialty.TabIndex = 3;
            this.btnFindSpecialty.Text = "Пошук спеціальності";
            this.btnFindSpecialty.UseVisualStyleBackColor = true;
            this.btnFindSpecialty.Click += new System.EventHandler(this.btnFindSpecialty_Click);

            this.btnFindMinimalCompetition.Location = new System.Drawing.Point(330, 70);
            this.btnFindMinimalCompetition.Name = "btnFindMinimalCompetition";
            this.btnFindMinimalCompetition.Size = new System.Drawing.Size(150, 25);
            this.btnFindMinimalCompetition.TabIndex = 4;
            this.btnFindMinimalCompetition.Text = "Мінімальний конкурс";
            this.btnFindMinimalCompetition.UseVisualStyleBackColor = true;
            this.btnFindMinimalCompetition.Click += new System.EventHandler(this.btnFindMinimalCompetition_Click);

            this.lstResults.FormattingEnabled = true;
            this.lstResults.ItemHeight = 16;
            this.lstResults.Location = new System.Drawing.Point(12, 100);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(468, 260);
            this.lstResults.TabIndex = 5;

            this.ClientSize = new System.Drawing.Size(492, 375);
            this.Controls.Add(this.lstResults);
            this.Controls.Add(this.btnFindMinimalCompetition);
            this.Controls.Add(this.btnFindSpecialty);
            this.Controls.Add(this.btnFindUniversity);
            this.Controls.Add(this.txtSpecialty);
            this.Controls.Add(this.txtUniversity);
            this.Name = "MainForm";
            this.Text = "Довідник Абітурієнта";
            this.ResumeLayout(false);
            this.PerformLayout();   
        }
    }
}

