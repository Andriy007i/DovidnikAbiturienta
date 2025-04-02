using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Modules
{
    internal class Specialty
    {
        public string Name { get; set; }
        public int BudgetPlaces { get; set; }
        public int TotalPlaces { get; set; }
        public decimal StudyCost { get; set; }

        public Specialty(string name,int budgetPlaces,int totalPlaces, decimal studyCost) 
        {
            Name = name;
            BudgetPlaces = budgetPlaces;
            TotalPlaces = totalPlaces;
            StudyCost = studyCost;
        }
        public double CompetitionPerPlace(int applications)
        {
            return applications / (double)TotalPlaces;
        }

        public override string ToString()
        {
            return $"{Name} (Бюджет:{BudgetPlaces}, Усього місць: {TotalPlaces}, Вартість:{StudyCost})";
        }
    }
}
