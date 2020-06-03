using System;

namespace MarsRover
{
    public struct Rover
    {
        public int x {get;}
        public int y {get;}
        
        public Direction direction {get;}

        public Rover(int x = 0 , int y = 0, Direction direction = Direction.North){
          this.x = x;
          this.y = y;
          this.direction = direction;
        }
    }
}