using System;

namespace MarsRover
{
    public class MarsRover
    {
        public static Point Process(Point startPoint, Direction startDirection, char[] commands){
            switch (startDirection){
                case Direction.North:
                    return new Point(startPoint.x, startPoint.y + 1);
                case Direction.South:
                    return new Point(startPoint.x, startPoint.y - 1);
                case Direction.East:
                    return new Point(startPoint.x + 1, startPoint.y);
                case Direction.West:
                    return new Point(startPoint.x - 1, startPoint.y);
                default:
                    throw new ArgumentException($"Unhandled Direction: {startDirection}");
            }
        }
    }
}
