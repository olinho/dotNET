using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CosmicAdventureDTO;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    // jedna instancja dla wszystkich wywołań
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        private List<Systemik> _systems = new List<Systemik>();

        public Starship GetStarship(int money)
        {
            Starship starship = new Starship();
            Random r = new Random();
            starship.Crew.Add(new Person() { Name = "Wiola", Nick = "V", Age = r.Next(1, 20) });
            starship.Crew.Add(new Person() { Name = "Mateusz", Nick = "M", Age = r.Next(1, 20) });
            starship.Crew.Add(new Person() { Name = "Aleksander", Nick = "A", Age = r.Next(1, 20) });

            if ((money > 1000) & (money <= 3000))
            {
                starship.ShipPower = r.Next(10, 25);
            }
            else if ((money > 3001) & (money <= 10000))
            {
                starship.ShipPower = r.Next(20, 35);
            }
            else if (money > 10000)
            {
                starship.ShipPower = r.Next(35, 60);
            }
            starship.Gold = 0;

            return starship;
        }

        public Systemik GetSystem()
        {
            if (_systems.Any())
            {
                return _systems.First();
            }
            else
            {
                return null;
            }
        }

        public void InitializeGame()
        {
            Random r = new Random();
            for (int i=0; i<4; i++)
            {
                Systemik s1 = new Systemik()
                {
                    Name = "Systemik" + i.ToString(),
                    MinShipPower_ = r.Next(10, 40),
                    BaseDistance = r.Next(20, 120),
                    Gold_ = r.Next(3000, 7000)
                };
                _systems.Add(s1);
            }
            
        }

        public Starship SendStarship(Starship starship, string systemName)
        {
            if (_systems.Exists(x => x.Name == systemName))
            {
                Systemik sys = _systems.Find(x => x.Name == systemName);
                int starshipPower = starship.ShipPower;
                if (starshipPower <= 20)
                {
                    foreach (var Person in starship.Crew)
                    {
                        Person.Age += (2 * sys.BaseDistance) / 12;
                    }
                }
                else if ((starshipPower > 20) & (starshipPower <= 30))
                {
                    foreach (var Person in starship.Crew)
                    {
                        Person.Age += (2 * sys.BaseDistance) / 6;
                    }
                }
                else
                {
                    foreach (var Person in starship.Crew)
                    {
                        Person.Age += (2 * sys.BaseDistance) / 4;
                    }
                }
                // usunięcie osób powyżej 90 roku życia
                starship.Crew.RemoveAll(x => x.Age > 90);
                // zebranie skarbu
                if (starshipPower >= sys.MinShipPower_)
                {
                    starship.Gold = sys.Gold_;
                    _systems.RemoveAll(x => x.Name == sys.Name);
                }
                
            }
            // jeśli nazwa systemu nie istnieje, to usuwamy całą załogę
            else
            {
                starship.Crew.Clear();
            }
            return starship;
        }
    }
}
