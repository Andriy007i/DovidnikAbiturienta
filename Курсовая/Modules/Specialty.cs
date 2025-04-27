using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Modules
{
    public class Specialty
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; } = "No description yet";
        public int Competition { get; set; }
        public int Price { get; set; }

        public Specialty(string name,string code,int competition, int price) 
        {
            Name = name;
            Code = code;
            Competition = competition;
            Price = price;
        }

        public override string ToString()
        {
            return $"Спеціальність: {Name} (Номер:{Code}, Усього місць: {Competition}, Вартість:{Price})";
        }
        public double CompetitionPerPlace(int applications)
        {
            return applications / (double)TotalPlaces;
        }

        
    }
}
