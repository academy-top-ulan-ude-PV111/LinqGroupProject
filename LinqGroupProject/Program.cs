namespace LinqGroupProject
{
    class Company
    {
        public string? Title { set; get; }
    }
    class Person
    {
        public string? Name { set; get; }
        public Company? Company { set; get; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Company> companies = new List<Company>()
            {
                new Company{ Title = "Yandex" },
                new Company{ Title = "Mail Group" },
                new Company{ Title = "Google" },
                new Company{ Title = "Microsoft" },
            };
            List<Person> persons = new List<Person>
            {
                new Person{ Name = "Bob", Company = companies[0] },
                new Person{ Name = "Joe", Company = companies[1] },
                new Person{ Name = "Tim", Company = companies[2] },
                new Person{ Name = "Mike", Company = companies[3] },
                new Person{ Name = "Bill", Company = companies[0] },
                new Person{ Name = "Pill", Company = companies[2] },
                new Person{ Name = "Sam", Company = companies[1] },
                new Person{ Name = "Tom", Company = companies[1] },
                new Person{ Name = "Jim", Company = companies[3] },
            };
            var personsComp = from p in persons
                              group p by p.Company;
            foreach(var comp in personsComp)
            {
                Console.WriteLine(comp.Key.Title);
                foreach(var person in comp)
                {
                    Console.WriteLine($"\t{person.Name}");
                }
            }
            Console.WriteLine(new String('-', 10));

            var personsCompMethod = persons.GroupBy(p => p.Company);
            foreach (var comp in personsComp)
            {
                Console.WriteLine(comp.Key.Title);
                foreach (var person in comp)
                {
                    Console.WriteLine($"\t{person.Name}");
                }
            }
            Console.WriteLine(new String('-', 10));


            var personsCompNewOne = from p in persons
                              group p by p.Company into temp
                              select new 
                              { 
                                  Title = temp.Key.Title, 
                                  Count = temp.Count()
                              };
            foreach (var comp in personsCompNewOne)
            {
                Console.WriteLine($"{comp.Title} - {comp.Count}");
            }
            Console.WriteLine(new String('-', 10));

            var personsCompNewTwo = persons.GroupBy(p => p.Company)
                                           .Select(temp => new
                                           {
                                               Title = temp.Key.Title,
                                               Count = temp.Count()
                                           });
            foreach (var comp in personsCompNewTwo)
            {
                Console.WriteLine($"{comp.Title} - {comp.Count}");
            }
            Console.WriteLine(new String('-', 10));

            var compPersonsOne = from person in persons
                              group person by person.Company into temp
                              select new
                              { 
                                  Company = temp.Key.Title,
                                  Count = temp.Count(),
                                  Employes = from p in temp select p
                              };
            foreach(var comp in compPersonsOne)
            {
                Console.WriteLine($"{comp.Company} - {comp.Count}");
                foreach (var person in comp.Employes)
                    Console.WriteLine($"\t{person.Name}");
            }
            Console.WriteLine(new String('-', 10));

            var compPersonsTwo = persons.GroupBy(p => p.Company)
                                        .Select(temp => new
                                        {
                                            Company = temp.Key.Title,
                                            Count = temp.Count(),
                                            Employes = temp.Select(p => p)
                                        });

            foreach (var comp in compPersonsTwo)
            {
                Console.WriteLine($"{comp.Company} - {comp.Count}");
                foreach (var person in comp.Employes)
                    Console.WriteLine($"\t{person.Name}");
            }
            Console.WriteLine(new String('-', 10));
        }
    }
}