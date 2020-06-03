using System;

namespace MarsRover
{
    public struct Obstacle
    {
        public int x {get;}
        public int y {get;}

        public Obstacle(int x, int y){
          this.x = x;
          this.y = y;
        }
    }
}