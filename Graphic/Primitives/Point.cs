using System;
using System.Numerics;

namespace maze.Graphic.Primitives
{
    public class Point
    {
        public Vector3 Position { get; set; }
        public Vector3 Normal { get; set; }
        public ConsoleColor Color { get; set; }
        public float Brightness { get; set; }

        public Point(Vector3 position, Vector3 normal, ConsoleColor color = ConsoleColor.White)
        {
            Position = position;
            Normal = normal;
            Color = color;
        }
    }
}