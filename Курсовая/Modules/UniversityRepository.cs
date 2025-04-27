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
            univer1.Specialties.Add(new Specialty("Комп'ютерні науки",  25000));
            univer1.Specialties.Add(new Specialty("Юридичні науки", 70, 200, 40000));

            var univer2 = new Universitet("Харківський національний університет радіоелектроніки", "Харків");
            univer1.Specialties.Add(new Specialty("Комп'ютерна інженерія", 20, 50, 25000));
            univer1.Specialties.Add(new Specialty("Інженерія програмного забезпечення", 30, 40, 70000));

            Universities.Add(univer1);
            Universities.Add(univer2);
        }

            public List<Universitet> Search(string keyword)
        {
        return universities.Where(u =>
            u.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
            u.Specialty.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        }
    }
