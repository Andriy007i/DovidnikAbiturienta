
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dovidnik_Abiturienta.Modules
{
    public class Specialty
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; } = "No description yet";
        public int DayTimeCompetition { get; set; }
        public int DistantCompetition { get; set; }
        public int Price { get; set; }

        public Specialty(string name, string code, int daytimeCompetition, int distantCompetition, int price)
        {
            Name = name;
            Code = code;
            DayTimeCompetition = daytimeCompetition;
            DistantCompetition = distantCompetition;
            Price = price;
        }

        public override string ToString()
        {
            return $"Спеціальність: {Name} (Номер:{Code}, Усього місць на денну: {DayTimeCompetition},Заочно: {DistantCompetition} Вартість:{Price})";
        }


    }
}