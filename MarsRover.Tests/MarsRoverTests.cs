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
            Rover rover = MarsRover.Process(startX, startY, startDirection, new []{'f'});
            
            Rover expectedRover = new Rover(expectedX, expectedY, expectedDirection);
            MarsRoverAsserts.AssertRoversEqual(expectedRover, rover);
        }

        [Fact]
        public void ProcessesMultipleCommands(){
            Rover rover = MarsRover.Process(0, 0, Direction.North, new []{'f', 'f'});

            Rover expectedRover = new Rover(0, 2, Direction.North);
            MarsRoverAsserts.AssertRoversEqual(expectedRover, rover);
        }

        [Fact]
        public void MovesRoverBackward(){
            Rover rover = MarsRover.Process(0, 0, Direction.North, new []{'b', 'b'});

            Rover expectedRover = new Rover(0, -2, Direction.North);
            MarsRoverAsserts.AssertRoversEqual(expectedRover, rover);
        }

        [Theory]
        [InlineData(Direction.North, 'l', Direction.West)]
        [InlineData(Direction.West, 'l', Direction.South)]
        [InlineData(Direction.South, 'l', Direction.East)]
        [InlineData(Direction.East, 'l', Direction.North)]
        [InlineData(Direction.North, 'r', Direction.East)]
        [InlineData(Direction.East, 'r', Direction.South)]
        [InlineData(Direction.South, 'r', Direction.West)]
        [InlineData(Direction.West, 'r', Direction.North)]
        public void RotateRover(Direction startDirection, char command, Direction expectedDirection){        
            Rover rover = MarsRover.Process(0, 0, startDirection, new []{command});
            
            Rover expectedRover = new Rover(0, 0, expectedDirection);
            MarsRoverAsserts.AssertRoversEqual(expectedRover, rover);
        }

        [Theory]
        [InlineData(Direction.North, 0, 3, Direction.North, 0, -3)]
        [InlineData(Direction.South, 0, -3, Direction.South, 0, 3)]
        [InlineData(Direction.East, 3, 0, Direction.East, -3, 0)]
        [InlineData(Direction.West, -3, 0, Direction.West, 3, 0)]
        public void WrapRoverMovement(Direction startDirection, int startX, int startY, Direction expectedDirection, int expectedX, int expectedY){        
            Rover rover = MarsRover.Process(startX, startY, startDirection, new []{'f'}, 3);
            
            Rover expectedRover = new Rover(expectedX, expectedY, expectedDirection);
            MarsRoverAsserts.AssertRoversEqual(expectedRover, rover);
        }

        [Fact]
        public void RoverReportsObstacle(){
            Rover rover = MarsRover.Process(0, 0, Direction.North, new []{'f'}, 3, new []{new Obstacle(0,1)});

            Rover expectedRover = new Rover(0, 0, Direction.North, true);
            MarsRoverAsserts.AssertRoversEqual(expectedRover, rover);
        }

        [Fact]
        public void FailsValidationForInvalidCharCommand(){
            Assert.Throws<ArgumentException>(() => MarsRover.Process(0, 0, Direction.North, new []{'q'}));
        }

        [Fact]
        public void ProcessesExtendedCommandSequenceWithWrap(){
            Rover rover = MarsRover.Process(0, 0, Direction.North, new []{'f', 'f', 'f', 'r', 'f', 'f', 'f', 'r', 'b'}, 3);

            Rover expectedRover = new Rover(3, -3, Direction.South, false);
            MarsRoverAsserts.AssertRoversEqual(expectedRover, rover);
        }

        [Fact]
        public void ProcessesExtendedCommandSequenceWithCrash(){
            Rover rover = MarsRover.Process(0, 0, Direction.North, new []{'f', 'f', 'f', 'l', 'f', 'f', 'f', 'r', 'b', 'b', 'b',}, 10, new []{new Obstacle(-3,0), new Obstacle(2,2)});

            Rover expectedRover = new Rover(-3, 1, Direction.North, true);
            MarsRoverAsserts.AssertRoversEqual(expectedRover, rover);
        }
    }
}
