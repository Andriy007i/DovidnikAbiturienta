using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Dovidnik_Abiturienta.Modules;
using System;
using System.Collections.Generic;

namespace Dovidnik_Abiturienta.Forms
{
    public partial class AddEntityWindow : Window
    {
        private List<University> _universities;
        private UniversityRepository _repository;

        public AddEntityWindow(List<University> universities, UniversityRepository repository) 
        {
            InitializeComponent();
            _universities = universities;
            _repository = repository;

            UniversityComboBox.ItemsSource = _universities;

            EntityTypeComboBox.SelectionChanged += EntityTypeComboBox_SelectionChanged;

            AddButton.Click += AddButton_Click;
        }

        private void EntityTypeComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (EntityTypeComboBox.SelectedIndex == 0) 
            {
                UniversityPanel.IsVisible = true;
                SpecialtyPanel.IsVisible = false;
            }
            else 
            {
                UniversityPanel.IsVisible = false;
                SpecialtyPanel.IsVisible = true;
            }
        }

        private void AddButton_Click(object? sender, RoutedEventArgs e)
        {
            if (EntityTypeComboBox.SelectedIndex == 0) 
            {
                var name = UniversityNameTextBox.Text?.Trim();
                var city = CityTextBox.Text?.Trim();
                var address = AddressTextBox.Text?.Trim();
                var phone = PhoneTextBox.Text?.Trim();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(address))
                {
                    ShowError("Будь ласка, заповніть усі обов'язкові поля (назва, місто, адреса).");
                    return;
                }

                var university = new University(name, city, address, phone ?? "Немає даних");
                _universities.Add(university);
                System.Diagnostics.Debug.WriteLine($"Додано університет: {university.Name}");
            }
            else 
            {
                var selectedUniversity = UniversityComboBox.SelectedItem as University;
                var name = SpecialtyNameTextBox.Text?.Trim();
                var code = SpecialtyCodeTextBox.Text?.Trim();
                var dayTimeCompetitionText = DayTimeCompetitionTextBox.Text?.Trim();
                var distantCompetitionText = DistantCompetitionTextBox.Text?.Trim();
                var priceText = PriceTextBox.Text?.Trim();

                if (selectedUniversity == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
                {
                    ShowError("Будь ласка, оберіть університет і заповніть усі обов'язкові поля (назва, код).");
                    return;
                }

                if (!int.TryParse(dayTimeCompetitionText, out int dayTimeCompetition) ||
                    !int.TryParse(distantCompetitionText, out int distantCompetition) ||
                    !int.TryParse(priceText, out int price))
                {
                    ShowError("Місця та вартість повинні бути числовими значеннями.");
                    return;
                }

                var specialty = new Specialty(name, code, dayTimeCompetition, distantCompetition, price, selectedUniversity);
                selectedUniversity.Specialties.Add(specialty);
                System.Diagnostics.Debug.WriteLine($"Додано вартість: {specialty.Name} в {selectedUniversity.Name}");
            }

            this.Close();
        }

        private async void ShowError(string message)
        {
            var dialog = new Window
            {
                Title = "Помилка",
                Width = 300,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                Background = Brushes.White
            };

            var stackPanel = new StackPanel { Margin = new Avalonia.Thickness(20), Spacing = 15 };
            stackPanel.Children.Add(new TextBlock { Text = message });

            var okButton = new Button
            {
                Content = "OK",
                Width = 80,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Background = Brushes.Gray,
                Foreground = Brushes.White,
                CornerRadius = new Avalonia.CornerRadius(5)
            };
            okButton.Click += (s, args) => dialog.Close();

            stackPanel.Children.Add(okButton);
            dialog.Content = stackPanel;

            await dialog.ShowDialog(this);
        }
    }
}