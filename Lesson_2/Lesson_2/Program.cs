using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{

    public abstract class Animal
    {
        protected string Name { get; set; }
        private float Weight { get; set; }
        private bool HaveFur { get; set; }

        public abstract string Sound();
        public abstract string Trick();
        public abstract int CountLegs();
    }

    public class Circus : ICircus
    {
        public List<Animal> Animals = new List<Animal>();
        public string Name { get; set; }

        public Circus(Animal a)
        {
            Animals.Add(a);
        }
        public Circus(Circus c)
        {
            Animals = c.Animals;
        }
        public Circus(string name) : this()
        {
            Name = name;
        }

        public Circus()
        {
            Animals.Add(new Pony());
            Animals.Add(new Pony());
            Animals.Add(new Ant());
            Animals.Add(new Elephant());
            Animals.Add(new Giraffe());
            Animals.Add(new Giraffe());
            Animals.Add(new Cat());
        }

        public string AnimalsIntroduction()
        {
            string soundAssembling = "";
            foreach (var A in Animals)
            {
                soundAssembling = String.Format(soundAssembling, A.Sound());
            }
            return soundAssembling;
        }

        public int Patter(int howMuch)
        {
            int result = 0;
            foreach (var A in Animals)
            {
                result += A.CountLegs() * howMuch;
            }
            return result;
        }

        public string Show()
        {
            string trickAssembling = "";
            string trickAssembling2 = "";
            
            foreach (var A in Animals)
            {
                //trickAssembling = String.Format("{0}{1}", trickAssembling, A.Trick());
                trickAssembling2 = String.Join("\n", new String[] { trickAssembling2, A.Trick()});
            }
            return trickAssembling2;
        }
    }

    public class Zoo : IZoo
    {
        public List<Animal> Animals = new List<Animal>();
        string Name { get; set; }

        public Zoo()
        {
            Animals.Add(new Pony());
            Animals.Add(new Pony());
            Animals.Add(new Ant());
            Animals.Add(new Elephant());
            Animals.Add(new Giraffe());
            Animals.Add(new Cat());
            Animals.Add(new Giraffe());
            Animals.Add(new Cat());
        }


        public string Sounds()
        {
            string soundAssembling = "";
            foreach (var A in Animals)
            {
                String.Join("\n", new string[] { soundAssembling, A.Sound() });
            }
            return soundAssembling;
        }
    }

    public class Cat : Animal
    {
        string Color { get; set; }

        public Cat()
        {
            Name = "cat";
        }

        public Cat(string name)
        {
            this.Name = name;
        }

        public override int CountLegs()
        {
            return 2;
        }

        public override string Sound()
        {
            return "miau";
        }

        public override string Trick()
        {
            return "Standing on 2 legs";
        }
    }

    public class Pony : Animal
    {
        bool IsMagic { get; set; }

        public Pony()
        {
            Name = "pony";
        }
        public Pony(string name)
        {
            Name = name;
        }

        public override int CountLegs()
        {
            return 4;
        }

        public override string Sound()
        {
            return "ihhaa";
        }

        public override string Trick()
        {
            return "Jump over a car";
        }
    }

    public class Ant : Animal
    {
        bool IsQueen;

        public Ant()
        {
            Name = "ant";
        }

        public Ant(string name)
        {
            Name = name;
        }

        public override int CountLegs()
        {
            return 8;
        }

        public override string Sound()
        {
            return "szsz";
        }

        public override string Trick()
        {
            return "Doing somersaults";
        }
    }

    public class Elephant : Animal
    {

        public Elephant()
        {
            Name = "elephant";
        }

        public Elephant(string name)
        {
            Name = name;
        }
        public override int CountLegs()
        {
            return 4;
        }

        public override string Sound()
        {
            return "truu";
        }

        public override string Trick()
        {
            return "Eating hundreds kilograms of watermelons";
        }
    }

    public class Giraffe : Animal
    {
        public Giraffe()
        {
            Name = "giraffe";
        }
        public Giraffe(string name)
        {
            Name = name;
        }

        public override int CountLegs()
        {
            return 4;
        }

        public override string Sound()
        {
            return "bzzz";
        }

        public override string Trick()
        {
            return "Playing volleyball";
        }
    }

    public interface ICircus
    {
        string AnimalsIntroduction();
        string Show();
        int Patter(int howMuch);
    }

    public interface IZoo
    {
        string Sounds();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Circus Cyrk1 = new Circus("Krakowski");
            //Console.WriteLine(Cyrk1.Show());
            //Console.ReadLine();
            Circus Cyrk2 = new Circus("Wadowicki");
            Circus Cyrk3 = new Circus("Krynicki");

            Console.WriteLine("Cyrk1 is called {0}", Cyrk1.Name);

            Zoo Z1 = new Zoo();
            Zoo Z2 = new Zoo();
            Zoo Z3 = new Zoo();

            showOptions();
            var key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape)
            {
                showOptions();
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.A)
                {
                    Console.WriteLine();
                }
            }
        }

        public static void showOptions()
        {
            Console.WriteLine("Press 'a' to show Animals in chosen circous");
            Console.WriteLine("Press 'b' to show Program of  chosen circous");
            Console.WriteLine("Press 'c' to listen Sounds in chosen zoo");
            Console.WriteLine("Press 'd' to show First Found animal with fur in chosen zoo");
            Console.WriteLine("Press 'c' to show Names in chosen zoo");
        }
    }

}