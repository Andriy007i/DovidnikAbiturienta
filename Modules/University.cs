using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dovidnik_Abiturienta.Modules
{
    public class University
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("specialties")]
        public List<Specialty> Specialties { get; set; }

        public University()
        {
            Specialties = new List<Specialty>();
        }

        public University(string name, string city, string address, string phoneNumber)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            City = city ?? throw new ArgumentNullException(nameof(city));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            PhoneNumber = phoneNumber ?? "Немає даних";
            Specialties = new List<Specialty>();
        }

        public override string ToString()
        {
            return $"{Name} ({City})";
        }
    }
}