using System;
using System.Numerics;

namespace maze.Engine
{
    public struct ProjectedVertice
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vector3 Origin { get; set; }
        public float Brightness { get; set; }
        public ConsoleColor Color { get; set; }
    }
}