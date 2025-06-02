using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Dovidnik_Abiturienta.Modules;
using System.Collections.Generic;
using System.Linq;

namespace Dovidnik_Abiturienta.Forms
{
    public partial class SelectSpecialtyForComparisonWindow : Window
    {
        private List<University> _universities;
        private Specialty _firstSpecialty;

        public SelectSpecialtyForComparisonWindow(List<University> universities, Specialty firstSpecialty)
        {
            _universities = universities ?? throw new ArgumentNullException(nameof(universities));
            _firstSpecialty = firstSpecialty ?? throw new ArgumentNullException(nameof(firstSpecialty));
            BuildWindow();
        }

        private void BuildWindow()
        {
            var stackPanel = new StackPanel { Margin = new Avalonia.Thickness(20), Spacing = 15 };

            var listBox = new ListBox
            {
                BorderThickness = new Avalonia.Thickness(1),
                BorderBrush = Brushes.Gray,
                Height = 300,
                Width = 400
            };

            var specialties = _universities.SelectMany(u => u.Specialties)
                .Where(s => s != _firstSpecialty)
                .Select(s => $"{s.University?.Name} - {s.Name} (Код: {s.Code})")
                .ToList();
            listBox.ItemsSource = specialties;

            stackPanel.Children.Add(new TextBlock { Text = "Оберіть спеціальність для порівняння:", FontSize = 16 });
            stackPanel.Children.Add(listBox);

            var compareButton = new Button
            {
                Content = "Порівняти",
                Width = 120,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Background = Brushes.Blue,
                Foreground = Brushes.White,
                CornerRadius = new Avalonia.CornerRadius(5)
            };
            compareButton.Click += async (s, e) =>
            {
                if (listBox.SelectedItem != null)
                {
                    var selectedItem = listBox.SelectedItem as string;
                    var specInfo = selectedItem?.Split(" - ");
                    if (specInfo != null && specInfo.Length > 1)
                    {
                        var specDetails = specInfo[1].Split(" (Код: ");
                        var specName = specDetails[0];
                        var specCode = specDetails[1].Replace(")", "");
                        var secondSpecialty = _universities
                            .SelectMany(u => u.Specialties)
                            .FirstOrDefault(s => s.Name == specName && s.Code == specCode);

                        if (secondSpecialty != null)
                        {
                            var compareWindow = new CompareSpecialtiesWindow(new List<Specialty> { _firstSpecialty, secondSpecialty });
                            await compareWindow.ShowDialog(this);
                            Close();
                        }
                    }
                }
            };
            stackPanel.Children.Add(compareButton);

            Content = stackPanel;
            Width = 450;
            Height = 400;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Title = "Вибір спеціальності для порівняння";
        }
    }
}