using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex37
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandTakeAShot = "1";
            const string CommandLookInQuiver = "2";

            int health = 100;
            bool isOpen = true;

            Arrow arrow = new Arrow("Деревянная стрела");

            Quiver quiver = new Quiver(arrow, 10);

            Bow bow = new Bow(0, quiver);

            while (isOpen)
            {
                Console.WriteLine($"Здоровье врага: {health} HP");
                Console.Write($"{CommandTakeAShot} - сделать выстрел\n{CommandLookInQuiver} - посмотреть в колчан\nСделать выстрел?");
                int chosenAction = Convert.ToInt32(Console.ReadLine());
                switch (chosenAction)
                {
                    case 1:
                        int damage = bow.TakeAShot(bow.Damage);
                        health -= damage;
                        Console.WriteLine($"Вы нанесли {damage} урона");

                        if (health <= 0)
                        {
                            Console.WriteLine("Враг повержен!");
                            isOpen = false;
                        }

                        break;

                    case 2:
                        quiver.ShowAllArrows();
                        break;

                    default:
                        Console.WriteLine("Неверная команда...");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Arrow
    {
        public string Name;

        public Arrow(string name)
        {
            Name = name;
        }
    }

    class Bow
    {
        public int Damage;
        public Quiver Quiver;

        public Bow(int damage, Quiver quiver)
        {
            Damage = damage;
            Quiver = quiver;
        }

        public int TakeAShot(int damage)
        {
            Random random = new Random();

            if (Quiver.IsEmpty())
            {
                damage += random.Next(5, 11);
                Quiver.ArrowCount--;
            }

            return damage;
        }
    }

    class Quiver
    {
        public Arrow Arrow;
        public int ArrowCount;

        public Quiver(Arrow arrow, int arrowCount)
        {
            Arrow = arrow;
            ArrowCount = arrowCount;
        }

        public void ShowAllArrows()
        {
            Console.WriteLine($"В колчане {Arrow.Name} {ArrowCount} шт.");
        }

        public bool IsEmpty()
        {
            if (ArrowCount == 0)
            {
                Console.WriteLine("Стрелы кончились");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
