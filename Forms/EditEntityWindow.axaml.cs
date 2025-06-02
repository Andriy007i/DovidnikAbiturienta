using Avalonia.Controls;
using Avalonia.Interactivity;
using Dovidnik_Abiturienta.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dovidnik_Abiturienta.Forms
{
    public partial class EditEntityWindow : Window
    {
        private List<University> _universities;
        private UniversityRepository _repository; 
        private University? _selectedUniversity;
        private Specialty? _selectedSpecialty;

        public EditEntityWindow(List<University> universities, UniversityRepository repository) 
        {
            InitializeComponent();
            _universities = universities;
            _repository = repository;

            UpdateEntitiesListBox();

            EntitiesListBox.SelectionChanged += EntitiesListBox_SelectionChanged;
            SaveButton.Click += SaveButton_Click;
            DeleteButton.Click += DeleteButton_Click;
        }

        private void UpdateEntitiesListBox()
        {
            var items = new List<string>();
            foreach (var uni in _universities)
            {
                items.Add($"Університет: {uni.Name}");
                foreach (var spec in uni.Specialties)
                {
                    items.Add($"  Спеціальність: {spec.Name} (Код: {spec.Code})");
                }
            }
            EntitiesListBox.ItemsSource = items;
        }

        private void EntitiesListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (EntitiesListBox.SelectedIndex < 0) return;

            var selectedItem = EntitiesListBox.SelectedItem as string;
            if (selectedItem == null) return;

            _selectedUniversity = null;
            _selectedSpecialty = null;

            if (selectedItem.StartsWith("Університет:"))
            {
                var uniName = selectedItem.Replace("Університет: ", "");
                _selectedUniversity = _universities.FirstOrDefault(u => u.Name == uniName);
                UniversityEditPanel.IsVisible = true;
                SpecialtyEditPanel.IsVisible = false;

                if (_selectedUniversity != null)
                {
                    UniversityNameTextBox.Text = _selectedUniversity.Name;
                    CityTextBox.Text = _selectedUniversity.City;
                    AddressTextBox.Text = _selectedUniversity.Adress;
                    PhoneTextBox.Text = _selectedUniversity.PhoneNumber;
                }
            }
            else if (selectedItem.StartsWith("  Спеціальність:"))
            {
                var specInfo = selectedItem.Replace("  Спеціальність: ", "").Split(" (Код: ");
                var specName = specInfo[0];
                var specCode = specInfo[1].Replace(")", "");
                _selectedSpecialty = _universities
                    .SelectMany(u => u.Specialties)
                    .FirstOrDefault(s => s.Name == specName && s.Code == specCode);
                UniversityEditPanel.IsVisible = false;
                SpecialtyEditPanel.IsVisible = true;

                if (_selectedSpecialty != null)
                {
                    SpecialtyNameTextBox.Text = _selectedSpecialty.Name;
                    SpecialtyCodeTextBox.Text = _selectedSpecialty.Code;
                    DayTimeCompetitionTextBox.Text = _selectedSpecialty.DayTimeCompetition.ToString();
                    DistantCompetitionTextBox.Text = _selectedSpecialty.DistantCompetition.ToString();
                    PriceTextBox.Text = _selectedSpecialty.Price.ToString();
                }
            }

            SaveButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
        }

        private async void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            if (_selectedUniversity != null)
            {
                _selectedUniversity.Name = UniversityNameTextBox.Text?.Trim() ?? _selectedUniversity.Name;
                _selectedUniversity.City = CityTextBox.Text?.Trim() ?? _selectedUniversity.City;
                _selectedUniversity.Adress = AddressTextBox.Text?.Trim() ?? _selectedUniversity.Adress;
                _selectedUniversity.PhoneNumber = PhoneTextBox.Text?.Trim() ?? _selectedUniversity.PhoneNumber;
                System.Diagnostics.Debug.WriteLine($"Обновлен университет: {_selectedUniversity.Name}");
            }
            else if (_selectedSpecialty != null)
            {
                _selectedSpecialty.Name = SpecialtyNameTextBox.Text?.Trim() ?? _selectedSpecialty.Name;
                _selectedSpecialty.Code = SpecialtyCodeTextBox.Text?.Trim() ?? _selectedSpecialty.Code;
                if (int.TryParse(DayTimeCompetitionTextBox.Text, out int dayTime))
                    _selectedSpecialty.DayTimeCompetition = dayTime;
                if (int.TryParse(DistantCompetitionTextBox.Text, out int distant))
                    _selectedSpecialty.DistantCompetition = distant;
                if (int.TryParse(PriceTextBox.Text, out int price))
                    _selectedSpecialty.Price = price;
                System.Diagnostics.Debug.WriteLine($"Оновлена спеціальність: {_selectedSpecialty.Name}");
            }

            UpdateEntitiesListBox();
            await ShowNotification("Зміни збережено!");
        }

        private async void DeleteButton_Click(object? sender, RoutedEventArgs e)
        {
            if (_selectedUniversity != null)
            {
                _universities.Remove(_selectedUniversity);
                _repository.RemoveUniversity(_selectedUniversity);
                System.Diagnostics.Debug.WriteLine($"Видалено університет: {_selectedUniversity.Name}");
            }
            else if (_selectedSpecialty != null)
            {
                var uni = _universities.FirstOrDefault(u => u.Specialties.Contains(_selectedSpecialty));
                if (uni != null)
                {
                    uni.Specialties.Remove(_selectedSpecialty);
                    _repository.RemoveSpecialty(uni, _selectedSpecialty);
                    System.Diagnostics.Debug.WriteLine($"Видалена спеціальність: {_selectedSpecialty.Name}");
                }
            }

            UpdateEntitiesListBox();
            ClearEditFields();
            SaveButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            await ShowNotification("Елемент видалено!");
        }

        private async Task ShowNotification(string message)
        {
            NotificationTextBlock.Text = message;
            NotificationTextBlock.IsVisible = true;
            await Task.Delay(2000);
            NotificationTextBlock.IsVisible = false;
        }

        private void ClearEditFields()
        {
            UniversityEditPanel.IsVisible = false;
            SpecialtyEditPanel.IsVisible = false;
            _selectedUniversity = null;
            _selectedSpecialty = null;
            UniversityNameTextBox.Text = "";
            CityTextBox.Text = "";
            AddressTextBox.Text = "";
            PhoneTextBox.Text = "";
            SpecialtyNameTextBox.Text = "";
            SpecialtyCodeTextBox.Text = "";
            DayTimeCompetitionTextBox.Text = "";
            DistantCompetitionTextBox.Text = "";
            PriceTextBox.Text = "";
        }
    }
}