using System;
using System.Windows.Forms;
using System.Linq;
using Курсовая.Modules;

namespace YourNamespace.Forms
{
    public partial class MainForm : Form
    {
        private UniversityRepository repository;

        public MainForm()
        {
            InitializeComponent();
            repository = new UniversityRepository();
        }

        private void btnFindUniversity_Click(object sender, EventArgs e)
        {
            lstResults.Items.Clear();
            var university = repository.GetUniversityByName(txtUniversity.Text);

            if(university != null)
            {
                lstResults.Items.Add(university.ToString());
                lstOutput.Items.Add("Спеціальності:");
                foreach (var specialty in university.Specialties)
                {
                    lstResults.Items.Add($"- {spec.Name}");
                }
                else
                {
                    MessageBox.Show("Вуз не знайдено");
                }
            }
        }
        private void btnFindSpecialty_Click(object sender, EventArgs e)
        {
            lstResults.Items.Clear();
            var specialties = repository.GetSpecialtiesByName(txtSpecialty.Text);

            if(specialties.Any())
            {
                foreach( var specialty in specialties)
                {
                    lstResults.Items.Add(specialty.ToString());
                }
            }
            else
            {
                MessageBox.Show("Спеціальність не знайдено");
            }
        }
        private void btnFindMinimalConpetition_Click(object sender, EventArgs e)
        {
            lstResults.Items.Clear();
            var specialty = repository.GetSpecialtyWithMinimalCompetition(txtSpecialty.Text);

            if(specialty != null)
            {
                lstResults.Items.Add($"Спеціальність з мінімальним конкурсом:");
                lstResults.Items.Add(specialty.ToString());
            }
            else
            {
                MessageBox.Show("Спеціальність не знайдено");
            }
        }
    }
          
}
