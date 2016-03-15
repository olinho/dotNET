using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpaceClient.MyOwnService;

namespace SpaceClient
{

    class Program
    {
        static void Main(string[] args)
        {
            Starship starship = new Starship()
            {
                Crew = new Person[]
            { new Person() {Name = "Wanio", Age = 15 },
            new Person() { Name = "Warwara", Age = 35},
            new Person() { Name = "Tichon", Age = 28 },
            new Person() { Name = "Anulka", Age = 23 },
            new Person() { Name = "Taras", Age = 25 }},
                Captain = new Person() { Name = "JackSparrow", Age = 36 }
            };
            
            PresentCrew(starship);

            BlackHoleClient client = new BlackHoleClient();
            Starship olderStarship =  client.PullStarship(starship);
            Console.WriteLine(client.UltimateAnswer());

            PresentCrew(olderStarship);

            Console.ReadKey();
        }

        public static void PresentCrew(Starship starship)
        {
            Console.WriteLine("Captain {0}, age {1}", starship.Captain.Name, starship.Captain.Age);
            Console.WriteLine("Crew of WhiteStone:");
            foreach (var Person in starship.Crew)
            {
                Console.WriteLine("Imie: {0}, wiek: {1}", Person.Name, Person.Age);
            }
        }
    }
}
