using System.Collections.Generic;

namespace Dovidnik_Abiturienta.Modules
{
    public class University
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public List<Specialty> Specialties { get; set; }

        public University(string name, string city, string adress, string phoneNumber)
        {
            Name = name ?? throw new System.ArgumentNullException(nameof(name));
            City = city ?? throw new System.ArgumentNullException(nameof(city));
            Adress = adress ?? throw new System.ArgumentNullException(nameof(adress));
            PhoneNumber = phoneNumber ?? "Немає даних";
            Specialties = new List<Specialty>();
        }

        public override string ToString()
        {
            return $"{Name} ({City})";
        }
    }
}