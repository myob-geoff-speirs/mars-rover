using System;
using System.Linq;

namespace MarsRover
{
    public class MarsRover
    {
        public static Rover Process(int startX, int startY, Direction startDirection, char[] commands){
            var rover = new Rover(startX, startY, startDirection);
            return commands.Aggregate(rover, (prevRover, currCommand) => ProcessCommand(prevRover, currCommand));
        }

        public static Rover ProcessCommand(Rover rover, char commands){
            switch (rover.direction){
                case Direction.North:
                    return new Rover(rover.x, rover.y + 1, rover.direction);
                case Direction.South:
                    return new Rover(rover.x, rover.y - 1, rover.direction);
                case Direction.East:
                    return new Rover(rover.x + 1, rover.y, rover.direction);
                case Direction.West:
                    return new Rover(rover.x - 1, rover.y, rover.direction);
                default:
                    throw new ArgumentException($"Unhandled Direction: {rover.direction}");
            }
        }
    }
}
