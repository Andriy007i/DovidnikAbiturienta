using System;
using System.Text.Json.Serialization;

namespace Dovidnik_Abiturienta.Modules
{
    public class Specialty
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("dayTimeCompetition")]
        public int DayTimeCompetition { get; set; }

        [JsonPropertyName("distantCompetition")]
        public int DistantCompetition { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("university")]
        public University? University { get; set; }

        public Specialty()
        {
        }

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