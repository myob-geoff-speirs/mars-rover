using System;
using Xunit;
using static MarsRover.MarsRover;

namespace MarsRover.Tests
{
    public class RoverTests
    {

        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.West, Direction.South)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.East, Direction.North)]
        public void RoverRotatesLeft(Direction startDirection, Direction expectedDirection){        
            Rover startRover = new Rover(0, 0, startDirection);

            Rover rover = startRover.RotateLeft();
            
            Rover expectedRover = new Rover(0, 0, expectedDirection);
            MarsRoverAsserts.AssertRoversEqual(expectedRover, rover);
        }

        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        public void RoverRotatesRight(Direction startDirection, Direction expectedDirection){        
            Rover startRover = new Rover(0, 0, startDirection);

            Rover rover = startRover.RotateRight();
            
            Rover expectedRover = new Rover(0, 0, expectedDirection);
            MarsRoverAsserts.AssertRoversEqual(expectedRover, rover);
        }

        [Theory]
        [InlineData(Direction.North, 1, 0, 1)]
        [InlineData(Direction.South, 2, 0, -2)]
        [InlineData(Direction.East, 3, 3, 0)]
        [InlineData(Direction.West, 4, -4, 0)]
        public void MovesRoverDistance(Direction startDirection, int distance, int expectedX, int expectedY){        
            Rover startRover = new Rover(0, 0, startDirection);

            Rover rover = startRover.MoveDistance(distance);
            
            Rover expectedRover = new Rover(expectedX, expectedY, startDirection);
            MarsRoverAsserts.AssertRoversEqual(expectedRover, rover);
        }
    }
}
