using System.Collections.Generic;
using System.Linq;

namespace Dovidnik_Abiturienta.Modules
{
    public class UniversityRepository
    {
        private List<University> Universities { get; set; }

        public UniversityRepository()
        {
            Universities = new List<University>();
        }

        public void InitializeDefaultData()
        {
            var kpi = new University("КПІ", "Київ", "просп. Перемоги, 37", "044-204-81-91");
            kpi.Specialties.Add(new Specialty("Комп’ютерні науки", "122", 150, 50, 30000, kpi));
            kpi.Specialties.Add(new Specialty("Інженерія ПЗ", "121", 100, 30, 32000, kpi) );

            var lnu = new University("ЛНУ", "Львів", "Університетська, 1", "032-239-41-00");
            lnu.Specialties.Add(new Specialty("Право", "081", 120, 40, 28000, lnu) );
            Universities.Add(kpi);
            Universities.Add(lnu);
        }

        public List<University> GetAllUniversities()
        {
            return Universities;
        }

        public void AddUniversity(University university)
        {
            Universities.Add(university);
        }

        public void RemoveUniversity(University university)
        {
            Universities.Remove(university);
        }

        public void AddSpecialty(University university, Specialty specialty)
        {
            university.Specialties.Add(specialty);
        }

        public void RemoveSpecialty(University university, Specialty specialty)
        {
            university.Specialties.Remove(specialty);
        }

        public List<University> SearchUniversities(string query)
        {
            if (string.IsNullOrEmpty(query))
                return Universities;

            return Universities.Where(u => u.Name.ToLower().Contains(query.ToLower())).ToList();
        }

        public List<University> SearchSpecialties(string query)
        {
            if (string.IsNullOrEmpty(query))
                return Universities;

            var result = new List<University>();
            foreach (var uni in Universities)
            {
                var matchingSpecialties = uni.Specialties.Where(s => s.Name.ToLower().Contains(query.ToLower())).ToList();
                if (matchingSpecialties.Any())
                {
                    var uniCopy = new University(uni.Name, uni.City, uni.Address, uni.PhoneNumber)
                    {
                        Specialties = matchingSpecialties
                    };
                    result.Add(uniCopy);
                }
            }
            return result;
        }
    }
}