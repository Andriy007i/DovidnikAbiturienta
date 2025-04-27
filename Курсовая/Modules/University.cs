using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Modules
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public List<Specialty> Specialties { get; set; } = new List<Specialty>();
        public string PhoneNumber { get; set; }
        
        public University()
        {
            Specialties = new List<Specialty>();
        }

        public void AddSpecialty(Specialty specialty)
        {
            if (specialty != null && !Specialties.Contains(specialty))
            {
                Specialties.Add(specialty);
        }

        public override string ToString()
        {
            return $"University: {Name}, City: {City}, Adress: {Adress}, Phone: {PhoneNumber}";
        }
    }
}
