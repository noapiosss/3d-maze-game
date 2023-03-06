using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Graphic.Extensions;
using maze.Graphic.Primitives.Interfaces;

namespace maze.Graphic.Primitives
{
    public abstract class Primitive : IProjectible, IBrightable
    {
        public Vector3 Normal { get; protected set; }
        public Vector3[] Vertices { get; protected set; }
        public (int, int, int)[] Indexes { get; protected set; }
        public (int, int)[] Lines { get; protected set; }
        public ConsoleColor Color { get; protected set; }

        public abstract ICollection<ProjectedVertice> Project(Screen screen, Vector3 light);

        protected bool ProjectedVerticeIsInsideScreen(int x, int y, Vector3 origin, Screen screen, Vector3 light, out ProjectedVertice projection)
        {
            projection = new()
            {
                X = x + (screen.Width / 2),
                Y = y + (screen.Height / 2),
                Origin = origin,
                Brightness = GetBrightness(origin, Normal, light),
                Color = Color
            };

            return projection.X >= 0 &&
                projection.X < screen.Width &&
                projection.Y >= 0 &&
                projection.Y < screen.Height &&
                projection.Origin.Z > 0 &&
                Vector3.Distance(projection.Origin, Vector3.Zero) < screen.RenderDistance;
        }

        public float GetBrightness(Vector3 pointPosition, Vector3 pointNormal, Vector3 light)
        {
            if (pointNormal == Vector3.Zero)
            {
                return 1;
            }

            float angle = (pointPosition - light).Angle(pointNormal);
            return (float)(angle / Math.PI);
        }
    }
}