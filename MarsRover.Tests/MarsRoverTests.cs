using System;
using Xunit;
using static MarsRover.MarsRover;

namespace MarsRover.Tests
{
    public class MarsRoverTests
    {

        [Theory]
        [InlineData(Direction.North, 0, 1, 0, 2)]
        [InlineData(Direction.South, 0, -1, 0, -2)]
        [InlineData(Direction.East, 1, 0, 2, 0)]
        [InlineData(Direction.West, -1, 0, -2, 0)]
        public void MovesRoverForward(Direction startDirection, int startX, int startY, int expectedX, int expectedY)
        {
            var startPoint = new Point(startX, startY);
            var expectedEndPoint = new Point(expectedX, expectedY);
            char[] command = {'f'};
            Point endPoint = MarsRover.Process(startPoint, startDirection, command);

            Assert.Equal(expectedEndPoint.x, endPoint.x);
            Assert.Equal(expectedEndPoint.y, endPoint.y);
        }
    }
}
