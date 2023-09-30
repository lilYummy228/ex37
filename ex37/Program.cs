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
            const string CommandDealDamageWithSword = "3";

            int enemyHealth = 200;
            int heroHealth = 100;
            int enemyDamage = 20;
            bool isOpen = true;

            Sword sword = new Sword(20);

            Arrow arrow = new Arrow("Деревянная стрела");

            Quiver quiver = new Quiver(arrow, 6);

            Bow bow = new Bow(0, quiver);

            while (isOpen)
            {
                Console.WriteLine($"Здоровье врага: {enemyHealth} HP\nВаше здоровье: {heroHealth}");
                Console.Write($"\n{CommandTakeAShot} - сделать выстрел из лука\n{CommandLookInQuiver} - посмотреть в колчан\n" +
                    $"{CommandDealDamageWithSword} - ударить мечом\n\nКакое действие выполнить? ");
                string chosenAction = Console.ReadLine();

                switch (chosenAction)
                {
                    case CommandTakeAShot:
                        isOpen = DealDamageWithBow(ref enemyHealth, bow);
                        break;

                    case CommandLookInQuiver:
                        quiver.ShowAllArrows();
                        break;

                    case CommandDealDamageWithSword:
                        isOpen = DealDamageWithSword(ref enemyHealth, sword, ref heroHealth, enemyDamage);
                        break;

                    default:
                        Console.WriteLine("Неверная команда...");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        static bool DealDamageWithBow(ref int enemyHealth, Bow bow)
        {
            int damage = bow.TakeAShot(bow.Damage);

            enemyHealth -= damage;
            Console.WriteLine($"Вы нанесли {damage} урона");

            return FinishHim(enemyHealth);
        }

        static bool DealDamageWithSword(ref int enemyHealth, Sword sword, ref int heroHealth, int enemyDamage)
        {
            enemyHealth -= sword.Damage;
            heroHealth -= enemyDamage;
            Console.WriteLine($"Вы нанесли {sword.Damage} урона");

            return FinishHim(enemyHealth);
        }

        static bool FinishHim(int enemyHealth)
        {
            if (enemyHealth <= 0)
            {
                Console.WriteLine("Враг повержен!");
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    class Sword
    {
        public int Damage;

        public Sword(int damage)
        {
            Damage = damage;
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
            int successShot = random.Next(0, 5);

            if (Quiver.IsEmpty())
            {
                if (successShot > 0)
                {
                    damage += random.Next(20, 41);
                    Quiver.ArrowCount--;
                }
                else
                {
                    Quiver.ArrowCount--;
                    Console.WriteLine("Вы промазали...");
                }
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
                Console.WriteLine("Стрелы кончились...");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
