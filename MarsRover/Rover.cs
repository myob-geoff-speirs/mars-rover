using System;

namespace MarsRover
{
    public struct Rover
    {
        public readonly int x {get;}
        public readonly int y {get;}
        public readonly Direction direction {get;}
        public readonly bool crashed {get;}

        public Rover(int x = 0 , int y = 0, Direction direction = Direction.North, bool crashed = false){
          this.x = x;
          this.y = y;
          this.direction = direction;
          this.crashed = crashed;
        }
        public Rover RotateLeft(){
            var newDirection =  direction switch {
                Direction.North => Direction.West,
                Direction.West => Direction.South,
                Direction.South => Direction.East,
                Direction.East => Direction.North,
                _ => throw new ArgumentException($"Unhandled Direction: {direction}")
            };
            return new Rover(x, y, newDirection);
        }
        public Rover RotateRight(){
            var newDirection =  direction switch {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new ArgumentException($"Unhandled Direction: {direction}")
            };
            return new Rover(x, y, newDirection);
        }
    }
}