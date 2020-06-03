using System;
using Xunit;
using static MarsRover.MarsRover;

namespace MarsRover.Tests
{
    public class CommandValidatorTests
    {

        [Fact]
        public void ValidatesCharCommands(){
            var commands = CommandValidator.Validate(new[]{'f', 'b', 'l', 'r'});
            Assert.Equal(Command.MoveForward, commands[0]);
            Assert.Equal(Command.MoveBackward, commands[1]);
            Assert.Equal(Command.TurnLeft, commands[2]);
            Assert.Equal(Command.TurnRight, commands[3]);
        }

        [Fact]
        public void FailsValidationForInvalidCharCommand(){
            Assert.Throws<ArgumentException>(() => CommandValidator.Validate(new[]{'q'}));
        }
    }
}
