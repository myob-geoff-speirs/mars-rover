using System;
using System.Linq;

namespace MarsRover
{
    public class MarsRover
    {
        public static Rover Process(int startX, int startY, Direction startDirection, char[] commands, int wrapDistance = 10, Obstacle[] obstacles = null){
            var rover = new Rover(startX, startY, startDirection);
            return ProcessCommands(rover, commands, wrapDistance, obstacles);
        }

        private static Rover ProcessCommands(Rover startingRover, char[] commands, int wrapDistance, Obstacle[] obstacles){
            return commands.Aggregate(startingRover, (prevRover, currCommand) => {
                var nextRover = ProcessCommand(prevRover, currCommand, wrapDistance);

                if (RoverWillCrash(nextRover, obstacles))
                    return new Rover(prevRover.x, prevRover.y, prevRover.direction, true);

                return nextRover;
            });
        }

        private static Rover ProcessCommand(Rover rover, char command, int wrapDistance){
            return command switch {
                var c when c == 'f' || c == 'b' => MoveRover(rover, command, wrapDistance),
                var c when c == 'l' || c == 'r' => RotateRover(rover, command),
                _ => throw new ArgumentException($"Unhandled Command: {command}")
            };
        }

        private static Rover MoveRover(Rover rover, char command, int wrapDistance){
            var distance = distanceForCommand(command);
            var desiredRover = rover.MoveDistance(distance);
            return WrapRover(rover, desiredRover, wrapDistance);
        }
        private static int distanceForCommand(char command){
            return command switch {
                'f' => 1,
                'b' => -1,
                _ => throw new ArgumentException($"Unhandled Movement Command: {command}")
            };
        }

        private static Rover WrapRover(Rover rover, Rover desiredRover, int wrapDistance){
            return desiredRover switch {
                var r when Math.Abs(r.x) > wrapDistance => new Rover(rover.x * -1, rover.y, rover.direction),
                var r when Math.Abs(r.y) > wrapDistance => new Rover(rover.x, rover.y * -1, rover.direction),
                _ => desiredRover
            };
        }

        private static Rover RotateRover(Rover rover, char command){
            return command switch {
                'l' => rover.RotateLeft(),
                'r' => rover.RotateRight(),
                _ => throw new ArgumentException($"Unhandled Rotation Command: {command}")
            };
        }
        private static bool RoverWillCrash(Rover nextRover, Obstacle[] obstacles){
            if (obstacles == null) return false;

            return obstacles.Any(obstacle => 
                obstacle.x == nextRover.x && 
                obstacle.y == nextRover.y);
        }
    }
}
