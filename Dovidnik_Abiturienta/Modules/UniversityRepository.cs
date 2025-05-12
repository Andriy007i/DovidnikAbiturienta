using System;
using System.Collections.Generic;
using System.Linq;
using Dovidnik_Abiturienta.Models;
using Dovidnik_Abiturienta.Modules;

namespace Dovidnik_Abiturienta.Models
{
    public class UniversityFacade
    {
        public List<University> Universities { get; set; }

        public UniversityFacade()
        {
            Universities = new List<University>();
            SeedData();
        }

        private void SeedData()
        {
            var knu = new University("Київський національний університет", "Київ", "проспект Перемоги, 37", "+380995485775");
            knu.Specialties.Add(new Specialty("Комп'ютерні науки", "122", 300, 100, 25000));
            knu.Specialties.Add(new Specialty("Юридичні науки", "112", 200, 50, 40000));

            var khnure = new University("ХНУРЕ", "Харків", "проспект Науки, 14", "0502805373");
            khnure.Specialties.Add(new Specialty("Комп'ютерна інженерія", "123", 50, 20, 50000));
            khnure.Specialties.Add(new Specialty("Інженерія ПЗ", "121", 40, 10, 70000));

            Universities.Add(knu);
            Universities.Add(khnure);
        }

        public List<University> GetAll()
        {
            return Universities;
        }

        public University GetUniversityByName(string name)
        {
            return Universities.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Specialty> GetSpecialtiesByName(string specialtyName)
        {
            return Universities
                .SelectMany(u => u.Specialties)
                .Where(s => s.Name.Equals(specialtyName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Specialty GetSpecialtyWithMinimalCompetition(string specialtyName)
        {
            return GetSpecialtiesByName(specialtyName)
                .OrderBy(s => s.DayTimeCompetition)
                .FirstOrDefault();
        }
    }
}
