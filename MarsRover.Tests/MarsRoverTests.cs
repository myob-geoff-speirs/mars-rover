using System;
using Xunit;
using static MarsRover.MarsRover;

namespace MarsRover.Tests
{
    public class MarsRoverTests
    {
        private void AssertRoversEqual(Rover rover1, Rover rover2){
            Assert.Equal(rover1.x, rover2.x);
            Assert.Equal(rover1.y, rover2.y);
            Assert.Equal(rover1.direction, rover2.direction);
        }

        [Theory]
        [InlineData(Direction.North, 0, 1, Direction.North, 0, 2)]
        [InlineData(Direction.South, 0, -1, Direction.South, 0, -2)]
        [InlineData(Direction.East, 1, 0, Direction.East, 2, 0)]
        [InlineData(Direction.West, -1, 0, Direction.West, -2, 0)]
        public void MovesRoverForward(Direction startDirection, int startX, int startY, Direction expectedDirection, int expectedX, int expectedY){        
            Rover rover = MarsRover.Process(startX, startY, startDirection, new []{'f'});
            
            Rover expectedRover = new Rover(expectedX, expectedY, expectedDirection);
            AssertRoversEqual(expectedRover, rover);
        }

        [Fact]
        public void ProcessesMultipleCommands(){
            Rover rover = MarsRover.Process(0, 0, Direction.North, new []{'f', 'f'});

            Rover expectedRover = new Rover(0, 2, Direction.North);
            AssertRoversEqual(expectedRover, rover);
        }

        [Fact]
        public void MovesRoverBackward(){
            Rover rover = MarsRover.Process(0, 0, Direction.North, new []{'b', 'b'});

            Rover expectedRover = new Rover(0, -2, Direction.North);
            AssertRoversEqual(expectedRover, rover);
        }
    }
}
