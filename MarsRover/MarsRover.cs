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
            var distance = distanceForCommand(command);
            return moveRover(rover, distance);
        }

        private static int distanceForCommand(char command){
            return command switch {
                'f' => 1,
                'b' => -1,
                _ => throw new ArgumentException($"Unhandled Command: {command}")
            };
        }

        private static Rover moveRover(Rover rover, int distance){
            return rover.direction switch {
                Direction.North => new Rover(rover.x, rover.y + distance, rover.direction),
                Direction.South => new Rover(rover.x, rover.y - distance, rover.direction),
                Direction.East => new Rover(rover.x + distance, rover.y, rover.direction),
                Direction.West => new Rover(rover.x - distance, rover.y, rover.direction),
                _ => throw new ArgumentException($"Unhandled Direction: {rover.direction}")
            };
        }
    }
}
