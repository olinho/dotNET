using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Space
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri address = new Uri("http://localhost:9002/BlackHole");

            ServiceHost blackHoleHost = new ServiceHost(typeof(BlackHole), address);

            try
            {
                // używając AddServiceEndpoint z typem interfejsu i WSHttpBinding zlecamy, aby ServiceHost 
                // nasłuchiwał (wystawił endpoint) po protokole Http
                blackHoleHost.AddServiceEndpoint(typeof(IBlackHole), new WSHttpBinding(), "TestBlackHoleServiceEndpoint");
                ServiceMetadataBehavior smd = new ServiceMetadataBehavior();
                smd.HttpGetEnabled = true;
                blackHoleHost.Description.Behaviors.Add(smd);

                // Open connection which allows to execute appropriate actions
                blackHoleHost.Open();
                Console.WriteLine("Ready, steady, go!");
                Console.ReadLine();

                // Close cleans up after itself
                blackHoleHost.Close();
            }
            catch (CommunicationException ex)
            {
                // najpierw wyjątek zostanie dopasowany do 
                // communication exception, a  jeśli typ nie będzie się zgadzał
                // to wejdzie do Exception, który jest na samej górze
                // hierarchii wyjątków
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // metoda Abort wycofuje ServiceHost z działania
                blackHoleHost.Abort();
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Starship
    {
        public string Name { get; set; }
        public Person Captain { get; set; }
        public List<Person> Crew { get; set; }
    }

    public class Planet
    {
        public string Name { get; set; }
        public int Mass { get; set; }
    }

    [ServiceContract]
    interface IBlackHole
    {
        [OperationContract]
        Starship PullStarship(Starship ship);

        [OperationContract]
        string UltimateAnswer();
    }

    public class BlackHole : IBlackHole
    {
        public string UltimateAnswer()
        {
            Console.WriteLine("Calling UltimateAnswer()");
            return 42.ToString();
        }

        public Starship PullStarship(Starship ship)
        {
            Console.WriteLine("Calling PullStarship()");
            if (ship.Captain.Age <= 40)
            {
                foreach (var P in ship.Crew)
                {
                    P.Age += 20;
                }
            }
            return ship;
        }
    }

}
