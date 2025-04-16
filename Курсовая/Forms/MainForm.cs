using System;
using System.Windows.Forms;
using System.Linq;

namespace YourNamespace.Forms
{
    public partial class MainForm : Form
    {
        private UniversityFacade universityFacade;

        public MainForm()
        {
            InitializeComponent();
            universityFacade = new UniversityFacade();
        }

        private void InitializeComponent()
        {
            this.Text = "Пошук університетів";
            this.Width = 600;
            this.Height = 300;

            var textBox = new TextBox
            {
                Name = "searchBox",
                Width = 300,
                Left = 150,
                Top = 50
            };

            var searchButton = new Button
            {
                Text = "Пошук",
                Left = 250,
                Top = 100,
                Width = 100
            };

            var resultsBox = new ListBox
            {
                Name = "resultsBox",
                Top = 150,
                Left = 50,
                Width = 500,
                Height = 100
            };

            searchButton.Click += (sender, args) =>
            {
                string keyword = textBox.Text;
                var results = universityFacade.Search(keyword);

                resultsBox.Items.Clear();
                if (results.Any())
                {
                    foreach (var u in results)
                    {
                        resultsBox.Items.Add($"{u.Name} — {u.Specialty} — Конкурс: {u.Competition}, Вартість: {u.Tuition} грн");
                    }
                }
                else
                {
                    resultsBox.Items.Add("Нічого не знайдено.");
                }
            };

            this.Controls.Add(textBox);
            this.Controls.Add(searchButton);
            this.Controls.Add(resultsBox);
        }
    }
}
