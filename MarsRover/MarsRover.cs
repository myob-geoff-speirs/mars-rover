using System;
using System.Linq;

namespace MarsRover
{
    public class MarsRover
    {
        public static Rover Process(int startX, int startY, Direction startDirection, char[] charCommands, int wrapDistance = 10, Obstacle[] obstacles = null){
            Command[] commands = CommandValidator.Validate(charCommands);
            var rover = new Rover(startX, startY, startDirection);
            return ProcessCommands(rover, commands, wrapDistance, obstacles);
        }

        private static Rover ProcessCommands(Rover startingRover, Command[] commands, int wrapDistance, Obstacle[] obstacles){
            return commands.Aggregate(startingRover, (prevRover, currCommand) => {
                var nextRover = ProcessCommand(prevRover, currCommand, wrapDistance);

                if (RoverWillCrash(nextRover, obstacles))
                    return new Rover(prevRover.x, prevRover.y, prevRover.direction, true);

                return nextRover;
            });
        }

        private static Rover ProcessCommand(Rover rover, Command command, int wrapDistance){
            return command switch {
                var c when c == Command.MoveForward || c == Command.MoveBackward => MoveRover(rover, command, wrapDistance),
                var c when c == Command.TurnLeft || c == Command.TurnRight => RotateRover(rover, command),
                _ => throw new ArgumentException($"Unhandled Command: {command}")
            };
        }

        private static Rover MoveRover(Rover rover, Command command, int wrapDistance){
            var distance = distanceForCommand(command);
            var desiredRover = rover.MoveDistance(distance);
            return WrapRover(rover, desiredRover, wrapDistance);
        }
        private static int distanceForCommand(Command command){
            return command switch {
                Command.MoveForward => 1,
                Command.MoveBackward => -1,
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

        private static Rover RotateRover(Rover rover, Command command){
            return command switch {
                Command.TurnLeft => rover.RotateLeft(),
                Command.TurnRight => rover.RotateRight(),
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
