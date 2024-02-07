using RabbitStarConsole.Abstract;
using RabbitStarConsole.Concrete;
using RabbitStarConsole.Entities;
using RabbitStarConsole.Enums;
using System;

namespace RabbitStarConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the size of the forest (e.g. 4 for 4x4, 8 for 8x8), 16 for 16x16): ");

            int forestSize;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out forestSize))
                {
                    if (forestSize == 4 || forestSize == 8 || forestSize == 16)
                        break;

                    Console.WriteLine("Invalid size! Please enter either 4, 8, or 16.");
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a numeric value.");
                }
            }

            IForest forest = new ForestStar(forestSize);
            IRabbit rabbit = new RabbitStar(forest);
            IRabbitManager rabbitManager = new RabbitStarManager(rabbit, forest);

            //Tavşanın konumu belirlendi.
            rabbit.SetLocation(new Location(0, 0), Direction.South);

            //Tavşan deliğinin konumu belirlendi.
            forest.SetObject(new Location(forestSize - 1, forestSize - 1), ForestObject.RabbitHole);

            //Engeller random bir şekilde dağıtıldı.
            forest.GenerateObstacle();

            forest.ShowArea();
            rabbit.ShowLocationInfo();

            while (true)
            {
                Console.WriteLine("Enter the rabbit's movement scenario (Forward: N, Back: P, Right: R, Left: L, Jump: J, Down: İ) Sample “N,N,L,J,N,N,İ,P,J”: ");
                string scenario = Console.ReadLine().ToUpper();

                if (rabbitManager.ExecuteOperation(scenario) != Result.Dead)
                {
                    forest.ShowArea();
                    rabbit.ShowLocationInfo();
                }
                else
                {
                    break;
                }
            }

            Console.ReadKey();
        }
    }
}