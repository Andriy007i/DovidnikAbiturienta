namespace Dovidnik_Abiturienta.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxUniversities;
        private System.Windows.Forms.ListBox listBoxSpecialties;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;

        private void InitializeComponent()
        {
            this.listBoxUniversities = new System.Windows.Forms.ListBox();
            this.listBoxSpecialties = new System.Windows.Forms.ListBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // listBoxUniversities
            this.listBoxUniversities.FormattingEnabled = true;
            this.listBoxUniversities.Location = new System.Drawing.Point(12, 50);
            this.listBoxUniversities.Size = new System.Drawing.Size(250, 300);
            this.listBoxUniversities.SelectedIndexChanged += new System.EventHandler(this.listBoxUniversities_SelectedIndexChanged);

            // listBoxSpecialties
            this.listBoxSpecialties.FormattingEnabled = true;
            this.listBoxSpecialties.Location = new System.Drawing.Point(280, 50);
            this.listBoxSpecialties.Size = new System.Drawing.Size(500, 300);

            // textBoxSearch
            this.textBoxSearch.Location = new System.Drawing.Point(100, 15);
            this.textBoxSearch.Size = new System.Drawing.Size(400, 20);
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);

            // labelSearch
            this.labelSearch.Text = "Пошук:";
            this.labelSearch.Location = new System.Drawing.Point(12, 18);

            // MainForm
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.listBoxUniversities);
            this.Controls.Add(this.listBoxSpecialties);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.labelSearch);
            this.Text = "Довідник абітурієнта";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
