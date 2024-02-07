using RabbitStarConsole.Abstract;
using RabbitStarConsole.Entities;
using RabbitStarConsole.Enums;
using System;

namespace RabbitStarConsole.Concrete
{
    public class RabbitStar : Rabbit, IRabbit
    {
        private readonly IForest _forest;

        public RabbitStar(IForest forest)
        {
            _forest = forest;
        }

        public bool IsOutOfBorder()
        {
            return Location.PointX < 0 || Location.PointY < 0 || Location.PointX > _forest.GetSize() || Location.PointY > _forest.GetSize();
        }
        public bool IsWireBarbed(Location location)
        {
            return _forest.GetArea()[location.PointX, location.PointY] == (char)ForestObject.WireBarbed;
        }
        public bool IsWireFence(Location location)
        {
            return _forest.GetArea()[location.PointX, location.PointY] == (char)ForestObject.WireFence;
        }
        public bool IsDead()
        {
            return _forest.GetArea()[Location.PointX, Location.PointY] == (char)ForestObject.Wolf || _forest.GetArea()[Location.PointX, Location.PointY] == (char)ForestObject.Fox;
        }
        public bool IsHole()
        {
            return _forest.GetArea()[Location.PointX, Location.PointY] == (char)ForestObject.RabbitHole;
        }

        public Location GetLocation()
        {
            return Location;
        }
        public Direction GetDirection()
        {
            return Direction;
        }

        public void SetLocation(Location location, Direction direction = Direction.North)
        {
            if (Location != null)
            {
                Location.PointX = location.PointX;
                Location.PointY = location.PointY;
            }
            else
            {
                Location = new Location(location.PointX, location.PointY);
            }

            Direction = direction;

            _forest.SetObject(Location, ForestObject.Rabbit);
        }
        public void SetDirection(Direction direction)
        {
            Direction = direction;
        }

        public void ShowLocationInfo()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine($"Location: {Location.PointX + 1},{Location.PointY + 1} Direction: {GetDirectionUniCode()} ({Direction})");
            Console.WriteLine();

            Console.ResetColor();
        }

        private char GetDirectionUniCode()
        {
            if (Direction == Direction.North)
                return '\u2191';
            if (Direction == Direction.South)
                return '\u2193';
            if (Direction == Direction.West)
                return '\u2190';
            else
                return '\u2192';
        }
    }
}