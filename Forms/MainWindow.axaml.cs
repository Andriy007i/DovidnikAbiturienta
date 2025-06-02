using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Dovidnik_Abiturienta.Modules;
using Avalonia.Layout;
using System.Threading.Tasks;
using System;
using Avalonia; 
using Avalonia.Controls.Shapes; 

namespace Dovidnik_Abiturienta.Forms
{
    public partial class MainWindow : Window
    {
        private UniversityRepository _repository;
        private const string SaveFile = "universities_data.json";
        private bool _isClosingConfirmed = false;
        private Specialty? _selectedSpecialty;
        private const string AnyCityOption = "Будь-яке місто";

        public MainWindow()
        {
            InitializeComponent();
            _repository = new UniversityRepository();

            if (_repository.GetAllUniversities() == null || !_repository.GetAllUniversities().Any())
            {
                _repository.InitializeDefaultData();
            }

            if (File.Exists(SaveFile))
            {
                try
                {
                    var json = File.ReadAllText(SaveFile);
                    var loadedUniversities = JsonSerializer.Deserialize<List<University>>(json);
                    if (loadedUniversities != null && loadedUniversities.Any())
                    {
                        _repository = new UniversityRepository();
                        _repository.GetAllUniversities().AddRange(loadedUniversities);
                        foreach (var uni in _repository.GetAllUniversities())
                        {
                            foreach (var spec in uni.Specialties)
                            {
                                spec.University = uni;
                            }
                        }
                    }
                }
                catch { }
            }

            InitializeCityFilter();
            InitializeOtherComponents();
        }

        private void InitializeCityFilter()
        {
            var cities = new List<string> { AnyCityOption };
    
            if (_repository.GetAllUniversities() != null)
        {
            cities.AddRange(_repository.GetAllUniversities()
            .Select(u => u.City)
            .Where(c => !string.IsNullOrEmpty(c))
            .Distinct()
            .OrderBy(c => c));
        }

            CityFilterComboBox.ItemsSource = cities; // Только установка ItemsSource
            CityFilterComboBox.SelectedIndex = 0;
    }
        private void InitializeOtherComponents()
        {
            SearchButton.Click += SearchButton_Click;
            AddEntityButton.Click += AddEntityButton_Click;
            EditEntityButton.Click += EditEntityButton_Click;
            SortPriceComboBox.SelectionChanged += SortPriceComboBox_SelectionChanged;
            SortQuantityComboBox.SelectionChanged += SortQuantityComboBox_SelectionChanged;
            CityFilterComboBox.SelectionChanged += CityFilterComboBox_SelectionChanged;
            ResultsListBox.SelectionChanged += ResultsListBox_SelectionChanged;
            this.Closing += MainWindow_Closing;
        }

        private void SearchButton_Click(object? sender, RoutedEventArgs e)
        {
            UpdateSearchResults();
        }

        private void UpdateSearchResults()
        {
            if (_repository.GetAllUniversities() == null || !_repository.GetAllUniversities().Any())
            {
                ResultsListBox.ItemsSource = new List<string> { "Немає даних для відображення" };
                SpecialtyDetailsTextBlock.Text = "Немає даних для відображення";
                return;
            }

            var uniQuery = UniversitySearchBox.Text?.Trim().ToLower() ?? "";
            var specQuery = SpecialtySearchBox.Text?.Trim().ToLower() ?? "";
            var selectedCity = CityFilterComboBox.SelectedItem as string;

            var filtered = _repository.GetAllUniversities().AsEnumerable();

            if (!string.IsNullOrEmpty(uniQuery))
            {
                filtered = filtered.Where(u => u.Name.ToLower().Contains(uniQuery));
            }

            if (!string.IsNullOrEmpty(selectedCity) && selectedCity != AnyCityOption)
            {
                filtered = filtered.Where(u => u.City == selectedCity);
            }

            var results = new List<string>();
            foreach (var uni in filtered)
            {
                var specialties = uni.Specialties.AsEnumerable();
                if (!string.IsNullOrEmpty(specQuery))
                {
                    specialties = specialties.Where(s => s.Name.ToLower().Contains(specQuery));
                }

                if (!specialties.Any() && string.IsNullOrEmpty(specQuery))
                {
                    results.Add(uni.ToString());
                }
                else
                {
                    foreach (var spec in specialties)
                    {
                        results.Add($"{uni.Name} - {spec.Name} (Код: {spec.Code})");
                    }
                }
            }

            ResultsListBox.ItemsSource = results.Any() ? results : new List<string> { "Нічого не знайдено" };
            UpdateSpecialtyDetails();
        }

        private void SortPriceComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (SortPriceComboBox.SelectedIndex == -1) return;
            bool ascending = SortPriceComboBox.SelectedIndex == 0;
            SortSpecialties(s => s.Price, ascending);
        }

        private void SortQuantityComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (SortQuantityComboBox.SelectedIndex == -1) return;

            bool ascending;
            Func<Specialty, int> keySelector;

            switch (SortQuantityComboBox.SelectedIndex)
            {
                case 0:
                    keySelector = s => s.DayTimeCompetition;
                    ascending = true;
                    break;
                case 1:
                    keySelector = s => s.DayTimeCompetition;
                    ascending = false;
                    break;
                case 2:
                    keySelector = s => s.DistantCompetition;
                    ascending = true;
                    break;
                case 3:
                    keySelector = s => s.DistantCompetition;
                    ascending = false;
                    break;
                default:
                    return;
            }

            SortSpecialties(keySelector, ascending);
        }

        private void CityFilterComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            UpdateSearchResults();
        }

        private void SortSpecialties(Func<Specialty, int> keySelector, bool ascending)
        {
            if (_repository.GetAllUniversities() == null || !_repository.GetAllUniversities().Any()) return;

            var specQuery = SpecialtySearchBox.Text?.Trim().ToLower() ?? "";
            var uniQuery = UniversitySearchBox.Text?.Trim().ToLower() ?? "";
            var selectedCity = CityFilterComboBox.SelectedItem as string;

            var filtered = _repository.GetAllUniversities()
                .Where(u => string.IsNullOrEmpty(uniQuery) || u.Name.ToLower().Contains(uniQuery))
                .Where(u => string.IsNullOrEmpty(selectedCity) || selectedCity == AnyCityOption || u.City == selectedCity)
                .SelectMany(u => u.Specialties
                    .Where(s => string.IsNullOrEmpty(specQuery) || s.Name.ToLower().Contains(specQuery)));

            var sorted = ascending
                ? filtered.OrderBy(keySelector)
                : filtered.OrderByDescending(keySelector);

            var results = sorted
                .Select(s => $"{s.University?.Name ?? "Невідомий університет"} - {s.Name} (Код: {s.Code})")
                .ToList();

            ResultsListBox.ItemsSource = results.Any() ? results : new List<string> { "Нічого не знайдено" };
            UpdateSpecialtyDetails();
        }

        private void ResultsListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            UpdateSpecialtyDetails();
            if (ResultsListBox.SelectedItem != null)
            {
                var selectedItem = ResultsListBox.SelectedItem as string;
                var specInfo = selectedItem?.Split(" - ");
                if (specInfo != null && specInfo.Length > 1)
                {
                    var specDetails = specInfo[1].Split(" (Код: ");
                    var specName = specDetails[0];
                    var specCode = specDetails[1].Replace(")", "");
                    _selectedSpecialty = _repository.GetAllUniversities()
                        .SelectMany(u => u.Specialties)
                        .FirstOrDefault(s => s.Name == specName && s.Code == specCode);
                }
            }
        }

        private void UpdateSpecialtyDetails()
        {
            if (ResultsListBox.SelectedItems == null || ResultsListBox.SelectedItems.Count == 0)
            {
                SpecialtyDetailsTextBlock.Text = "Оберіть спеціальність для перегляду деталей";
                return;
            }

            var selectedItem = ResultsListBox.SelectedItem as string;
            if (selectedItem?.StartsWith("Університет:") == true)
            {
                SpecialtyDetailsTextBlock.Text = "Це університет, оберіть спеціальність для деталей.";
                return;
            }

            var specInfo = selectedItem?.Split(" - ");
            if (specInfo == null || specInfo.Length < 2) return;

            var uniName = specInfo[0];
            var specDetails = specInfo[1].Split(" (Код: ");
            var specName = specDetails[0];
            var specCode = specDetails[1].Replace(")", "");

            var specialty = _repository.GetAllUniversities()
                .SelectMany(u => u.Specialties)
                .FirstOrDefault(s => s.Name == specName && s.Code == specCode);

            if (specialty != null)
            {
                SpecialtyDetailsTextBlock.Text = $"Назва: {specialty.Name}\n" +
                                                $"Код: {specialty.Code}\n" +
                                                $"Денна форма: {specialty.DayTimeCompetition}\n" +
                                                $"Заочна форма: {specialty.DistantCompetition}\n" +
                                                $"Ціна: {specialty.Price} грн";
            }
            else
            {
                SpecialtyDetailsTextBlock.Text = "Деталі не знайдено.";
            }
        }

        private async void AddEntityButton_Click(object? sender, RoutedEventArgs e)
        {
            var addWindow = new AddEntityWindow(_repository.GetAllUniversities(), _repository);
            await addWindow.ShowDialog(this);
            UpdateSearchResults();
            InitializeCityFilter(); // Обновляем список городов после добавления
        }

        private async void EditEntityButton_Click(object? sender, RoutedEventArgs e)
        {
            var editWindow = new EditEntityWindow(_repository.GetAllUniversities(), _repository);
            await editWindow.ShowDialog(this);
            UpdateSearchResults();
            InitializeCityFilter(); // Обновляем список городов после редактирования
        }

        private async void CompareMenuItem_Click(object? sender, RoutedEventArgs e)
        {
            if (_selectedSpecialty == null)
            {
                await ShowError("Спочатку оберіть спеціальність!");
                return;
            }

            var selectWindow = new SelectSpecialtyForComparisonWindow(_repository.GetAllUniversities(), _selectedSpecialty);
            await selectWindow.ShowDialog(this);
        }

        private void MenuButton_Click(object? sender, RoutedEventArgs e)
        {
            if (ResultsListBox.SelectedItem != null)
            {
                var contextMenu = this.FindControl<ContextMenu>("ResultsListBox.ContextMenu");
                if (contextMenu != null)
                {
                    contextMenu.Open();
                }
            }
        }

        private async Task ShowError(string message)
        {
            var errorDialog = new Window
            {
                Title = "Помилка",
                Width = 300,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                Background = Brushes.White
            };
            var stackPanel = new StackPanel { Margin = new Thickness(20), Spacing = 15 };
            stackPanel.Children.Add(new TextBlock { Text = message });
            var okButton = new Button
            {
                Content = "OK",
                Width = 80,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = Brushes.Gray,
                Foreground = Brushes.White,
                CornerRadius = new CornerRadius(5)
            };
            okButton.Click += (s, args) => errorDialog.Close();
            stackPanel.Children.Add(okButton);
            errorDialog.Content = stackPanel;
            await errorDialog.ShowDialog(this);
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_isClosingConfirmed)
            {
                return;
            }

            e.Cancel = true;

            var dialog = new Window
            {
                Title = "Підтвердження",
                Width = 300,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                Background = Brushes.White
            };

            var stackPanel = new StackPanel
            {
                Margin = new Thickness(20),
                Spacing = 15
            };
            stackPanel.Children.Add(new TextBlock { Text = "Бажаєте закрити додаток?" });

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Spacing = 10
            };

            var yesButton = new Button
            {
                Content = "Так",
                Width = 80,
                Background = Brushes.Green,
                Foreground = Brushes.White,
                CornerRadius = new CornerRadius(5)
            };
            yesButton.Click += (s, args) =>
            {
                dialog.Close();
                SaveData();
                _isClosingConfirmed = true;
                this.Close();
            };

            var noButton = new Button
            {
                Content = "Ні",
                Width = 80,
                Background = Brushes.Red,
                Foreground = Brushes.White,
                CornerRadius = new CornerRadius(5)
            };
            noButton.Click += (s, args) =>
            {
                dialog.Close();
            };

            buttonPanel.Children.Add(yesButton);
            buttonPanel.Children.Add(noButton);
            stackPanel.Children.Add(buttonPanel);
            dialog.Content = stackPanel;

            dialog.ShowDialog(this);
        }

        private void SaveData()
        {
            try
            {
                var json = JsonSerializer.Serialize(_repository.GetAllUniversities());
                File.WriteAllText(SaveFile, json);
            }
            catch { }
        }
    }
}