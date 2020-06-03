using System;
using Xunit;
using static MarsRover.MarsRover;

namespace MarsRover.Tests
{
    public class MarsRoverTests
    {


        [Theory]
        [InlineData(Direction.North, 0, 1, Direction.North, 0, 2)]
        [InlineData(Direction.South, 0, -1, Direction.South, 0, -2)]
        [InlineData(Direction.East, 1, 0, Direction.East, 2, 0)]
        [InlineData(Direction.West, -1, 0, Direction.West, -2, 0)]
        public void MovesRoverForward(Direction startDirection, int startX, int startY, Direction expectedDirection, int expectedX, int expectedY){
        
            char[] commands = {'f'};
        
            Rover rover = MarsRover.Process(startX, startY, startDirection, commands);

            Assert.Equal(expectedX, rover.x);
            Assert.Equal(expectedY, rover.y);
            Assert.Equal(expectedDirection, rover.direction);
        }

        [Fact]
        public void ProcessesMultipleCommands(){
            
            Rover rover = MarsRover.Process(0, 0, Direction.North, new []{'f', 'f'});

            Assert.Equal(0, rover.x);
            Assert.Equal(2, rover.y);
            Assert.Equal(Direction.North, rover.direction);
        }
    }
}
