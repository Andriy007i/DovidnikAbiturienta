using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Modules
{
    internal class UniversityFacade
    {
        public List<Universitet> Universities { get; private set; }

        public UniversityFacade()
        {
            Universities = new List<Universitet>();
            SeedData();
        }
        private void SeedData()
        {
            var univer1 = new Universitet("Київський національний університет", "Київ");
            univer1.Specialties.Add(new Specialty("Комп'ютерні науки", 50, 100, 30000));
            univer1.Specialties.Add(new Specialty("Юридичні науки", 70, 200, 40000));

            var univer2 = new Universitet("Харківський національний університет радіоелектроніки", "Харків");
            univer1.Specialties.Add(new Specialty("Комп'ютерна інженерія", 20, 50, 25000));
            univer1.Specialties.Add(new Specialty("Інженерія програмного забезпечення", 30, 40, 70000));

            Universities.Add(univer1);
            Universities.Add(univer2);
        }

            public List<Universitet> SearchByCity(string city)
        {
            return Universities.Where(u => u.City.ToLower().Contains(city.ToLower())).ToList();
        }
        public List<Specialty> SearchBySpecialty(string SpecialtyName)
        {
            return Universities.SelectMany(u => u.Specialties)
            .Where(s => s.Name.ToLower().Contains(SpecialtyName.ToLower()))
            .ToList();
        }
        }
    }
