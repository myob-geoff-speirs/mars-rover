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

        private static Rover ProcessCommands(Rover rover, char[] commands, int wrapDistance, Obstacle[] obstacles){
            return commands.Aggregate(rover, (prevRover, currCommand) => {
                var desiredRover = ProcessCommand(prevRover, currCommand, wrapDistance);

                if (obstacles != null && obstacles.Any(obstacle => obstacle.x == desiredRover.x && obstacle.y == desiredRover.y))
                    return new Rover(prevRover.x, prevRover.y, prevRover.direction, true);

                return desiredRover;
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
            return MoveRoverDistance(rover, distance, wrapDistance);
        }
        private static int distanceForCommand(char command){
            return command switch {
                'f' => 1,
                'b' => -1,
                _ => throw new ArgumentException($"Unhandled Movement Command: {command}")
            };
        }

        private static Rover MoveRoverDistance(Rover rover, int distance, int wrapDistance){
            var desiredRover = rover.direction switch {
                Direction.North => new Rover(rover.x, rover.y + distance, rover.direction),
                Direction.South => new Rover(rover.x, rover.y - distance, rover.direction),
                Direction.East => new Rover(rover.x + distance, rover.y, rover.direction),
                Direction.West => new Rover(rover.x - distance, rover.y, rover.direction),
                _ => throw new ArgumentException($"Unhandled Direction: {rover.direction}")
            };

            return desiredRover switch {
                var r when Math.Abs(r.x) > wrapDistance => new Rover(rover.x * -1, rover.y, rover.direction),
                var r when Math.Abs(r.y) > wrapDistance => new Rover(rover.x, rover.y * -1, rover.direction),
                _ => desiredRover
            };
        }
        private static Rover RotateRover(Rover rover, char command){
            return command switch {
                'l' => RodateRoverLeft(rover),
                'r' => RotateRoverRight(rover),
                _ => throw new ArgumentException($"Unhandled Rotation Command: {command}")
            };
        }
        private static Rover RodateRoverLeft(Rover rover){
            var newDirection =  rover.direction switch {
                Direction.North => Direction.West,
                Direction.West => Direction.South,
                Direction.South => Direction.East,
                Direction.East => Direction.North,
                _ => throw new ArgumentException($"Unhandled Direction: {rover.direction}")
            };
            return new Rover(rover.x, rover.y, newDirection);
        }
        private static Rover RotateRoverRight(Rover rover){
            var newDirection =  rover.direction switch {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new ArgumentException($"Unhandled Direction: {rover.direction}")
            };
            return new Rover(rover.x, rover.y, newDirection);
        }
    }
}
