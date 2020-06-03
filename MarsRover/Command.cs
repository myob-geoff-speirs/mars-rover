using System;
using System.Linq;
using System.Collections.Generic;

namespace MarsRover
{
    public enum Command
    {
        MoveForward,
        MoveBackward,
        TurnLeft,
        TurnRight

    }
    public static class CommandValidator{
      public static Command[] Validate(char[] commands){
        return commands.Select(command => 
          command switch {
            'f' => Command.MoveForward,
            'b' => Command.MoveBackward,
            'l' => Command.TurnLeft,
            'r' => Command.TurnRight,
            _ => throw new ArgumentException($"Unhandled command: {command}")
          }
        ).ToArray();
      }
    }
}