using RabbitStarConsole.Abstract;
using RabbitStarConsole.Entities;
using RabbitStarConsole.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitStarConsole.Concrete
{
    public class ForestStar : Forest, IForest
    {
        public ForestStar(int size)
        {
            Size = size;
            Area = new char[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Area[i, j] = '.';
            }
        }

        public void SetObject(Location location, ForestObject forestObject)
        {
            Area[location.PointX, location.PointY] = (char)forestObject;
        }
        public void ClearObject(Location location)
        {
            Area[location.PointX, location.PointY] = '.';
        }
        public void GenerateObstacle()
        {
            //Engelleri listeye atadık.
            var forestObjects = Enum.GetValues(typeof(ForestObject))
                        .Cast<ForestObject>()
                        .Where(obj => obj != ForestObject.Rabbit && obj != ForestObject.RabbitHole)
                        .ToList();

            var maxCount = Size == 4 ? 2 : 4;
            var random = new Random();

            var obstacleList = new List<ForestObject>();
            foreach (var obj in forestObjects)
            {
                int randomCount = random.Next(1, maxCount + 1);
                for (int i = 0; i < randomCount; i++)
                    obstacleList.Add(obj);
            }

            //Engel listesini karıştırdık.
            obstacleList = obstacleList.OrderBy(obj => random.Next()).ToList();


            var availablePositions = Enumerable.Range(0, Size)
                                   .SelectMany(i => Enumerable.Range(0, Size)
                                                               .Where(j => (i != 0 || j != 0) && (i != (Size - 1) || j != (Size - 1)))
                                                               .Select(j => new Location(i, j)))
                                   .ToList();

            //Engelleri random bir şekilde koordinatlara atadık.
            foreach (var obstacle in obstacleList)
            {
                int index = random.Next(0, availablePositions.Count);
                var position = availablePositions[index];
                availablePositions.RemoveAt(index);
                SetObject(position, obstacle);
            }
        }

        public int GetSize()
        {
            return Size;
        }
        public char[,] GetArea()
        {
            return Area;
        }
        public void ShowArea()
        {
            Console.WriteLine();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if(Area[i, j] == (char)ForestObject.Rabbit)
                        Console.BackgroundColor = ConsoleColor.Blue;
                    else if (Area[i, j] == (char)ForestObject.RabbitHole)
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    else if (Area[i, j] == (char)ForestObject.Wolf || Area[i, j] == (char)ForestObject.Fox)
                        Console.BackgroundColor = ConsoleColor.Red;
                    else if (Area[i, j] == (char)ForestObject.WireBarbed || Area[i, j] == (char)ForestObject.WireFence)
                        Console.BackgroundColor = ConsoleColor.DarkYellow;

                    Console.Write(Area[i, j]);
                    Console.ResetColor();
                    Console.Write(" ");                    
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}