using System;

namespace Dovidnik_Abiturienta.Modules
{
    public class Specialty
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int DayTimeCompetition { get; set; }
        public int DistantCompetition { get; set; }
        public int Price { get; set; }
        public University? University { get; set; } 

        public Specialty(string name, string code, int dayTimeCompetition, int distantCompetition, int price, University? university = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Code = code ?? throw new ArgumentNullException(nameof(code));
            DayTimeCompetition = dayTimeCompetition;
            DistantCompetition = distantCompetition;
            Price = price;
            University = university;
        }

        public override string ToString()
        {
            return $"{Name} (Код: {Code})";
        }
    }
}