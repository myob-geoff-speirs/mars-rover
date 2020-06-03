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

        private static Rover ProcessCommand(Rover rover, char command){
            var moveDistance = command switch {
                'f' => 1,
                'b' => -1,
                _ => throw new ArgumentException($"Unhandled Command: {command}")
            };

            return rover.direction switch {
                Direction.North => new Rover(rover.x, rover.y + moveDistance, rover.direction),
                Direction.South => new Rover(rover.x, rover.y - moveDistance, rover.direction),
                Direction.East => new Rover(rover.x + moveDistance, rover.y, rover.direction),
                Direction.West => new Rover(rover.x - moveDistance, rover.y, rover.direction),
                _ => throw new ArgumentException($"Unhandled Direction: {rover.direction}")
            };
        }
    }
}
