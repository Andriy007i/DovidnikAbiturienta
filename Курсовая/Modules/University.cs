using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Modules
{
    public class University
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public List<Specialty> Specialties { get; set; }
        public string PhoneNumber { get; set; }
        
        public University(string name, string city, string adress, string phoneNumber)
        {
            Name = name;
            City = city;
            Adress = Adress;
            PhoneNumber = phoneNumber;
            Specialties = new List<Specialty>();
        }
        
        public override string ToString()
        {
            return $"University: {Name}, City: {City}, Adress: {Adress}, Phone: {PhoneNumber}";
        }
    }
}
