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
        public List<University> universities;

        public UniversityRepository()
        {
            universities = new List<University>();
            SeedData();
        }
        
        private void SeedData()
        {
            var knu = new Universitet("Київський національний університет", "Київ" ,"проспект Перемоги, 37","+380995485775");
            univer1.Specialties.Add(new Specialty("Комп'ютерні науки", 122, 300, 100, 25000));
            univer1.Specialties.Add(new Specialty("Юридичні науки", 112, 200, 50, 40000));

            var khnure = new Universitet("Харківський національний університет радіоелектроніки", "Харків", "проспект Науки, 14","0502805373");
            univer1.Specialties.Add(new Specialty("Комп'ютерна інженерія", 123, 50, 20, 50000));
            univer1.Specialties.Add(new Specialty("Інженерія програмного забезпечення", 121, 40, 10, 70000));

            Universities.Add(knu);
            Universities.Add(khnure);
        }

        public List<University> GetAll()
        {
            return universities;
        }
        
        public University GetUniversityByName(string name)
        {
            return Universities.FirstOrDefault(u => u.Name.ToLower() == name.ToLower());
        }
        
        public List<Specialty> GetSpecialtiesByName(string specialtyName)
        {
            return universities
                .SelectMany(u => u.Specialties)
                .Where(s => s.Name.ToLower() == specialtyName.ToLower())
                .ToList();
        }
        public Specialty GetSpecialtyWithMinimalCompetition(string SpecialtyName)
        {
            return GetSpecialtiesByName(specialtyName)
                .OrderBy(s => s.DayTimeCompetition)
                .FirstOrDefault():
        }
        }
    }
