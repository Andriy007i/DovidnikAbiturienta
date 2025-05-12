using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Dovidnik_Abiturienta.Modules;

namespace Dovidnik_Abiturienta.Forms
{
    public partial class MainForm : Form
    {
        private List<University> universities;

        public MainForm()
        {
            InitializeComponent();
            InitializeData();
            DisplayUniversities();
        }

        private void InitializeData()
        {
            universities = new List<University>();

            var kpi = new University("КПІ", "Київ", "просп. Перемоги, 37", "044-204-81-91");
            kpi.Specialties.Add(new Specialty("Комп’ютерні науки", "122", 150, 50, 30000));
            kpi.Specialties.Add(new Specialty("Інженерія програмного забезпечення", "121", 100, 30, 32000));

            var lnu = new University("ЛНУ", "Львів", "Університетська, 1", "032-239-41-00");
            lnu.Specialties.Add(new Specialty("Право", "081", 120, 40, 28000));
            lnu.Specialties.Add(new Specialty("Міжнародні відносини", "291", 80, 20, 35000));

            universities.Add(kpi);
            universities.Add(lnu);
        }
        

        private void DisplayUniversities()
        {
            listBoxUniversities.DataSource = null;
            listBoxUniversities.DataSource = universities;
        }

        private void listBoxUniversities_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedUniversity = listBoxUniversities.SelectedItem as University;
            if (selectedUniversity != null)
            {
                listBoxSpecialties.DataSource = null;
                listBoxSpecialties.DataSource = selectedUniversity.Specialties;
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string query = textBoxSearch.Text.ToLower();
            var filtered = universities.Where(u => u.Name.ToLower().Contains(query) || u.City.ToLower().Contains(query)).ToList();
            listBoxUniversities.DataSource = filtered;
        }
    }
}
