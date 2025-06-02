using Avalonia.Controls;
using Avalonia.Media;
using Dovidnik_Abiturienta.Modules;
using System.Collections.Generic;
using System.Linq;

namespace Dovidnik_Abiturienta.Forms
{
    public partial class CompareSpecialtiesWindow : Window
    {
        public CompareSpecialtiesWindow(List<Specialty> specialties)
        {
            BuildComparison(specialties);
        }

        private void BuildComparison(List<Specialty> specialties)
        {
            var mainPanel = new StackPanel { Margin = new Avalonia.Thickness(20), Spacing = 15 };

            var grid = new Grid { ColumnDefinitions = new ColumnDefinitions("150,200,200") };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); 


            var paramHeader = new TextBlock { Text = "Параметр", FontWeight = FontWeight.Bold, FontSize = 14, Margin = new Avalonia.Thickness(5) };
            var spec1Header = new TextBlock { Text = "Спеціальність 1", FontWeight = FontWeight.Bold, FontSize = 14, Margin = new Avalonia.Thickness(5) };
            var spec2Header = new TextBlock { Text = "Спеціальність 2", FontWeight = FontWeight.Bold, FontSize = 14, Margin = new Avalonia.Thickness(5) };

            Grid.SetColumn(paramHeader, 0);
            Grid.SetRow(paramHeader, 0);
            Grid.SetColumn(spec1Header, 1);
            Grid.SetRow(spec1Header, 0);
            Grid.SetColumn(spec2Header, 2);
            Grid.SetRow(spec2Header, 0);

            grid.Children.Add(paramHeader);
            grid.Children.Add(spec1Header);
            grid.Children.Add(spec2Header);


            var labels = new[] { "Назва", "Код", "Денна форма", "Заочна форма", "Ціна", "Галузь" };
            for (int i = 0; i < labels.Length; i++)
            {
                var label = new TextBlock { Text = labels[i], Margin = new Avalonia.Thickness(5) };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, i + 1);
                grid.Children.Add(label);
            }


            var spec1 = specialties[0];
            var spec1Data = new[]
            {
                spec1.Name,
                spec1.Code,
                spec1.DayTimeCompetition.ToString(),
                spec1.DistantCompetition.ToString(),
                $"{spec1.Price} грн",
            };

            for (int i = 0; i < spec1Data.Length; i++)
            {
                var data = new TextBlock { Text = spec1Data[i], Margin = new Avalonia.Thickness(5) };
                Grid.SetColumn(data, 1);
                Grid.SetRow(data, i + 1);
                grid.Children.Add(data);
            }

            var spec2 = specialties[1];
            var spec2Data = new[]
            {
                spec2.Name,
                spec2.Code,
                spec2.DayTimeCompetition.ToString(),
                spec2.DistantCompetition.ToString(),
                $"{spec2.Price} грн",
            };

            for (int i = 0; i < spec2Data.Length; i++)
            {
                var data = new TextBlock { Text = spec2Data[i], Margin = new Avalonia.Thickness(5) };
                Grid.SetColumn(data, 2);
                Grid.SetRow(data, i + 1);
                grid.Children.Add(data);
            }

            mainPanel.Children.Add(grid);

            var closeButton = new Button
            {
                Content = "Закрити",
                Width = 100,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                Background = Brushes.Gray,
                Foreground = Brushes.White,
                CornerRadius = new Avalonia.CornerRadius(5)
            };
            closeButton.Click += (s, e) => Close();
            mainPanel.Children.Add(closeButton);

            Content = mainPanel;
            Width = 600;
            Height = 400;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Title = "Порівняння спеціальностей";
        }
    }
}