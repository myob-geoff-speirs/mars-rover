using System;
using Xunit;
using static MarsRover.MarsRover;

namespace MarsRover.Tests
{
    public class MarsRoverAsserts
    {
        public static void AssertRoversEqual(Rover rover1, Rover rover2){
            Assert.Equal(rover1.x, rover2.x);
            Assert.Equal(rover1.y, rover2.y);
            Assert.Equal(rover1.direction, rover2.direction);
            Assert.Equal(rover1.crashed, rover2.crashed);
        }
    }
}