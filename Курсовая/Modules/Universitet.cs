using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Modules
{
    internal class Universitet
    {
        public string Name { get; set; }
        public string City { get; set; }
        public List<Specialty> Specialties { get; set; } = new List<Specialty>();

        public Universitet(string name,string city)
        {
            Name = name;
            City = city;
        }

        public override string ToString()
        {
            return $"{Name} ({City})";
        }
    }
}
