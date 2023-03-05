using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Graphic.Primitives.Interfaces;

namespace maze.Graphic.Primitives
{
    public abstract class Primitive : IProjectible
    {
        public Vector3[] Vertices { get; protected set; }
        public (int, int, int)[] Indexes { get; protected set; }
        public (int, int)[] Lines { get; protected set; }
        public ConsoleColor Color { get; protected set; }

        public abstract ICollection<ProjectedVertice> Project(Screen screen);

        protected bool ProjectedVerticeIsInsideScreen(int x, int y, float distance, Screen screen, out ProjectedVertice projection)
        {
            projection = new()
            {
                X = x + (screen.Width / 2),
                Y = y + (screen.Height / 2),
                Distance = distance,
                Color = Color
            };

            return projection.X >= 0 &&
                projection.X < screen.Width &&
                projection.Y >= 0 &&
                projection.Y < screen.Height &&
                projection.Distance > 0 &&
                projection.Distance < screen.RenderDistance;
        }
    }
}