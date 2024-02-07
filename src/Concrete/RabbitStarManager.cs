using RabbitStarConsole.Abstract;
using RabbitStarConsole.Entities;
using RabbitStarConsole.Enums;
using RabbitStarConsole.Utilities;
using System;
using System.Linq;

namespace RabbitStarConsole.Concrete
{
    public class RabbitStarManager : IRabbitManager
    {
        private readonly IRabbit _rabbit;
        private readonly IForest _forest;

        public RabbitStarManager(IRabbit rabbit, IForest forest)
        {
            _rabbit = rabbit;
            _forest = forest;
        }

        public Result ExecuteOperation(string scenario)
        {
            Result result = Result.Success;

            scenario.Split(',').ToList().ForEach(operation =>
            {
                int currentX = _rabbit.GetLocation().PointX;
                int currentY = _rabbit.GetLocation().PointY;

                switch (operation.Trim().Replace(" ", "")[0])
                {
                    case (char)Operation.Forward:
                        {
                            result = Move(Operation.Forward);
                            if (result == Result.Dead)
                                EndOfApp();
                            else if (result == Result.Invalid)
                                return;

                            break;
                        }
                    case (char)Operation.Backward:
                        {
                            result = Move(Operation.Backward);
                            if (result == Result.Dead)
                                EndOfApp();
                            else if (result == Result.Invalid)
                                return;

                            break;
                        }
                    case (char)Operation.Jump:
                        {
                            result = Move(Operation.Jump);
                            if (result == Result.Dead)
                                EndOfApp();
                            else if (result == Result.Invalid)
                                return;

                            break;
                        }
                    case (char)Operation.Down:
                        {
                            result = Move(Operation.Down);
                            if (result == Result.Dead)
                                EndOfApp();
                            else if (result == Result.Invalid)
                                return;

                            break;
                        }
                    case (char)Operation.Left:
                        TurnLeft();
                        break;
                    case (char)Operation.Right:
                        TurnRight();
                        break;
                    default:
                        result = Result.Invalid;
                        WriteHelper.Write(ConsoleColor.DarkYellow, $"Invalid command.");
                        break;
                }

                if (result == Result.Success)
                {
                    if (_rabbit.IsDead())
                    {
                        WriteHelper.Write(ConsoleColor.Red, $"***** Rabbit Star is dead! The application has ended! *****");
                        result = Result.Dead;
                    }
                    else if (_rabbit.IsHole())
                    {
                        WriteHelper.Write(ConsoleColor.DarkGreen, $"***** The rabbit star reached the hole! *****");
                        result = Result.Dead;
                    }
                    else
                    {
                        _forest.ClearObject(new Location(currentX, currentY));
                        _forest.SetObject(_rabbit.GetLocation(), ForestObject.Rabbit);
                    }
                }
            });

            return result;
        }

        private Result Move(Operation operation)
        {
            int deltaX = 0;
            int deltaY = 0;

            //Yöne göre step sayısını ayarlar.
            switch (_rabbit.GetDirection())
            {
                case Direction.North:
                    deltaX = operation == Operation.Forward ? -1 : operation == Operation.Backward ? 1 : -2;
                    break;
                case Direction.East:
                    deltaY = operation == Operation.Forward ? 1 : operation == Operation.Backward ? -1 : 2;
                    break;
                case Direction.South:
                    deltaX = operation == Operation.Forward ? 1 : operation == Operation.Backward ? -1 : 2;
                    break;
                case Direction.West:
                    deltaY = operation == Operation.Forward ? -1 : operation == Operation.Backward ? 1 : -2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Location newLocation = new Location(_rabbit.GetLocation().PointX + deltaX, _rabbit.GetLocation().PointY + deltaY);
            if (newLocation.PointX < 0 || newLocation.PointY < 0 || newLocation.PointX > _forest.GetSize() || newLocation.PointY > _forest.GetSize())
            {
                WriteHelper.Write(ConsoleColor.Red, $"***** The rabbit is out of border! *****");
                return Result.Dead;
            }

            bool isWireBarbed = _rabbit.IsWireBarbed(newLocation);
            bool isWireFence = _rabbit.IsWireFence(newLocation);

            //Sıradaki engel tel çit veya dikenli tel ise komut kontrolü yapılır.
            if (isWireBarbed || isWireFence)
            {
                WriteHelper.Write(ConsoleColor.DarkYellow, $"***** Incorrect command, should be -> {(isWireBarbed ? "I" : "J")} *****");
                return Result.Invalid;
            }

            _rabbit.GetLocation().PointX += deltaX;
            _rabbit.GetLocation().PointY += deltaY;

            if (_rabbit.IsOutOfBorder())
            {
                WriteHelper.Write(ConsoleColor.Red, $"***** The rabbit is out of border! *****");
                return Result.Dead;
            }

            return Result.Success;
        }
        private void TurnLeft()
        {
            _rabbit?.SetDirection(_rabbit.GetDirection() - 1 < Direction.North
                ? Direction.West
                : _rabbit.GetDirection() - 1);
        }
        private void TurnRight()
        {
            _rabbit?.SetDirection(_rabbit.GetDirection() + 1 > Direction.West
                ? Direction.North
                : _rabbit.GetDirection() + 1);
        }
        private void EndOfApp()
        {
            WriteHelper.Write(ConsoleColor.Red, $"***** The application has ended! *****");
        }
    }
}
