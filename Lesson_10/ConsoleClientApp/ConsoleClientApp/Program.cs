using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string serviceUri = "http://localhost:4271/";
            var container = new ConsoleClientAppSpace.Default.Container(new Uri(serviceUri));

            Console.WriteLine("Removing positions titled `Fifa2002` from games list ...");
            container.Games.Where(x => x.Title == "Fifa2002").ToList().ForEach(x => container.DeleteObject(x));
            var servResponse = container.SaveChanges();
            foreach (var operationResponse in servResponse)
            {
                Console.WriteLine("Response: {0}", operationResponse.StatusCode);
            }
            Console.WriteLine();

            Console.WriteLine("All games: ");
            foreach (var game in container.Games)
            {
                Console.WriteLine("{0} : {1} : {2}", game.Title, game.Year, game.CreatorCompany);
            }
            Console.WriteLine();


            Console.WriteLine("Adding new game ...");
            container.AddToGames(new ConsoleClientAppSpace.Library.Game() { Title = "Fifa2002", AgeRate = 23, CreatorCompany = "EAGames", Year = 2002 });
            var serviceResp = container.SaveChanges();

            foreach (var operationResp in serviceResp)
            {
                Console.WriteLine("Response: {0}", operationResp.StatusCode);
            }
            Console.WriteLine();

            Console.WriteLine("All games: ");
            foreach (var game in container.Games)
            {
                Console.WriteLine("{0} : {1} : {2}", game.Title, game.Year, game.CreatorCompany);
            }
            Console.WriteLine();

            Console.WriteLine("All CardShirts:");
            foreach (var card in container.CardShirts)
            {
                Console.WriteLine("{0}. {1}", card.Id, card.Name);
            }
            Console.WriteLine();

            container.AddToStores(new ConsoleClientAppSpace.Library.Store() { Name = "Empik", Address = "Czarny Potok" });
            container.AddToStores(new ConsoleClientAppSpace.Library.Store() { Name = "Spożywczy", Address = "Czarny Potok" });
            container.SaveChanges();


            Console.WriteLine("All stores:");
            foreach (var store in container.Stores)
            {
                Console.WriteLine("{2}. {0} : {1} street", store.Name, store.Address, store.Id);
            }
            Console.WriteLine();

            Console.WriteLine("Removing stores from Czarny Potok street ...");
            container.Stores.Where(x => x.Address == "Czarny Potok").ToList().ForEach(x => container.DeleteObject(x));
            serviceResp = container.SaveChanges();
            foreach (var operationResp in serviceResp)
            {
                Console.WriteLine("Response: {0}", operationResp.StatusCode);
            }

            Console.WriteLine("All stores:");
            foreach (var store in container.Stores)
            {
                Console.WriteLine("{2}. {0} : {1} street", store.Name, store.Address, store.Id);
            }
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
