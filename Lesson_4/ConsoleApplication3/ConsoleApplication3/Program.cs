using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleApplication3.Cosmos;
using ConsoleApplication3.FirstOrder;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            Cosmos.Service1Client cosmosClient = new Cosmos.Service1Client();
            FirstOrder.Service1Client firstClient = new FirstOrder.Service1Client();

            cosmosClient.InitializeGame();

            List<Starship> _starships = new List<Starship>();
            bool _anySystem = true;
            int _gold = 1000;
            int _imperiumMoneyAskCount = 4;

            while (true)
            {
                Console.WriteLine("Ilość złota: {0}, ilość próśb o złoto: {1}", _gold, _imperiumMoneyAskCount);

                
                Console.WriteLine("a) Ask empire for gold.");
                Console.WriteLine("b) Buy a ship for gold.");
                Console.WriteLine("c) Send a ship into space.");
                Console.WriteLine("d) Finish the game.");

                var key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key == ConsoleKey.A)
                {
                    if (_imperiumMoneyAskCount > 0)
                    {
                        _gold += firstClient.GetMoneyFromImperium();
                        _imperiumMoneyAskCount--;
                    }
                    // w przeciwnym wypadku powrót do menu
                    else
                    {
                        continue;
                    }
                }
                else if (key.Key == ConsoleKey.B)
                {
                    Console.WriteLine("Aktualne złoto: {0}. Enter amount you wish to buy a ship", _gold);
                    var line = Console.ReadLine();
                    int amount = int.Parse(line);
                    if (amount > _gold)
                    {
                        Console.WriteLine("Wrong amount. Return to menu.");
                        continue;
                    }
                    else
                    {
                        Starship s = cosmosClient.GetStarship(amount);
                        _starships.Add(s);
                        _gold -= amount;
                    }
                }
                else if (key.Key == ConsoleKey.C)
                {
                    // jeśli nie ma żadnych systemów to powrót do menu
                    if (cosmosClient.GetSystem() == null)
                    {
                        _anySystem = false;
                        Console.WriteLine("No systems.");
                        continue;
                    }
                    Console.WriteLine("System {0}, distance {1}.",
                        cosmosClient.GetSystem().Name,
                        cosmosClient.GetSystem().BaseDistance);
                    // to improve
                    if (_starships.Any())
                    {
                        Console.WriteLine("{0} starships ready to journey.", _starships.Count());
                    }
                    // jeśli nie ma statków to powrót do menu
                    else
                    {
                        continue;
                    }
                    Console.WriteLine("Select ship, typing its number (or enter 'e' to exit)");
                    int i = 1;
                    foreach (var ship in _starships)
                    {
                        Console.Write("{0}. {1}, ", i, ship.ShipPower);
                        foreach (var person in ship.Crew)
                        {
                            Console.Write("{0} {1} {2}, ", person.Name, person.Nick, person.Age);
                            Console.WriteLine();
                        }
                        i++;
                    }

                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (char.IsLetter(userInput.KeyChar))
                    {
                        if (userInput.Key == ConsoleKey.E)
                        {
                            continue;
                        }
                    }
                    else if (char.IsDigit(userInput.KeyChar))
                    {
                        int index = int.Parse(userInput.KeyChar.ToString()) - 1 ;
                        Console.WriteLine("Count of starships : {0}. Index={1}", _starships.Count(), index);
                        if (index > _starships.Count() || index < 0)
                        {
                            Console.WriteLine("Index out of bound");
                            continue;
                        }

                        // sending starship to the selected system
                        Starship ship2 = new Starship();
                        ship2 = cosmosClient.SendStarship(_starships.ElementAt(index), cosmosClient.GetSystem().Name);
                        _starships.RemoveAt(index);
                        if (ship2.Gold != 0)
                        {
                            _gold += ship2.Gold;
                        }
                        if (ship2.Crew.Any())
                        {
                            _starships.Add(ship2);
                        }
                    }
                }
                else if (key.Key == ConsoleKey.D)
                {
                    if (_anySystem == false)
                    {
                        Console.WriteLine("Victory!");
                    }
                    else
                    {
                        Console.WriteLine("Failure");
                    }
                    break;
                }
            }
            
        }
    }
}
