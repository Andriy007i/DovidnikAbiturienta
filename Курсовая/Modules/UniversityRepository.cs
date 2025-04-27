using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая.Modules
{
    public class UniversityFacade
    {
        public List<University> Universities { get; private set; }

        public UniversityRepository()
        {
            Universities = new List<University>();
            SeedData();
        }
        private void SeedData()
        {
            var univer1 = new Universitet("Київський національний університет", "Київ" ,"проспект Перемоги, 37","+380995485775");
            univer1.Specialties.Add(new Specialty("Комп'ютерні науки", 122, 300, 25000));
            univer1.Specialties.Add(new Specialty("Юридичні науки", 112, 200, 40000));

            var univer2 = new Universitet("Харківський національний університет радіоелектроніки", "Харків");
            univer1.Specialties.Add(new Specialty("Комп'ютерна інженерія", 123, 50, 50000));
            univer1.Specialties.Add(new Specialty("Інженерія програмного забезпечення", 121, 40, 70000));

            Universities.Add(univer1);
            Universities.Add(univer2);
        }
        public University GetUniversityByName(string name)
        {
            return Universities.FirstOrDefault(u => u.Name == name);
        }
        
        public List<University> GetSpecialtiesByName(string specialtyName)
        {
            return universities
                .SelectMany(u => u.Specialties)
                .Where(s => s.Name == specialtyName)
                .ToList();
        }
        public Specialty GetSpecialtyWithMinimalCompetition(string SpecialtyName)
        {
            return GetSpecialtiesByName(specialtyName)
                .OrderBy(s => s.Competition)
                .FirstOrDefault():
        }
        }
    }
