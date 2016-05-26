
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class GamesInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<GamesContext>
    {
        protected override void Seed(GamesContext context)
        {
            List<Game> games = new List<Game>()
            {
                new Game {AgeRate = 3,CreatorCompany = "ASB",Title = "CS",Year = 2000},
                new Game {AgeRate = 4, CreatorCompany="WWW", Title="Fifa", Year=2015 },
                new Game {AgeRate=3,CreatorCompany="NWM",Title="NHL",Year=1999 },
                new Game {AgeRate=12,CreatorCompany="SKI",Title="SkiJump",Year=2011 },
                new Game {AgeRate=10,CreatorCompany="YOW",Title="Muka",Year=2005 }
            };
            games.ForEach(x => context.Games.Add(x));
            context.SaveChanges();

            List<Store> stores = new List<Store>()
            {
                new Store {Address="Nowohucka",Name="Empik" },
                new Store {Address="Kozłowa",Name="MediaMarkt" },
                new Store {Address="Czarny Potok",Name="Biedronka" },
                new Store {Address="Biała",Name="4Games" }
            };
            stores.ForEach(x => context.Stores.Add(x));
            context.SaveChanges();

            List<CardShirt> cards = new List<CardShirt>()
            {
                new CardShirt { Name="MasterCard" },
                new CardShirt { Name="TescoCard" }
            };
            cards.ForEach(x => context.CardShirts.Add(x));
            context.SaveChanges();
        }
    }
}
