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
            string name = txtUniversityName.Text;
            var university = repository.GetUniversityByName(name);

            if(university != null)
            {
            lstOutPut.Items.Clear();
            lstOutput.Items.Add($"Назва: {university.Name}");
            lstOutput.Items.Add($"Адреса: {university.Adress}");
            lstOutput.Items.Add("Спеціальності:");
            foreach (var spec in university.Specialties)
            {
                lstOutput.Items.Add($"- {spec.Name} (Денна: {spec.DayTimeCompetition}, Вечірня: {spec.EveningTimeCompetition}, Заочна: {spec.DistantCompetition}, Ціна: {spec.Price} грн)");
            }
            else{
            MessageBox.Show("Вуз не знайдено");
            }
        }
          
}
