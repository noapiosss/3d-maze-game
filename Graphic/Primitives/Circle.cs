using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Graphic.Extensions;

namespace maze.Graphic.Primitives
{
    public class Circle : Primitive
    {
        public float Radius { get; protected set; }

        public Circle(Vector3 pivot, float radius, ConsoleColor color)
        {
            Pivot = new(pivot, Vector3.UnitZ, Vector3.UnitY, Vector3.UnitX);
            LocalVertices = new Vector3[] { Vector3.Zero };
            GlobalVertices = ToGlobalVertices();
            Radius = radius;
            Color = color;
            Normal = Vector3.Zero;
        }
        public Circle(Vector3 pivot, Vector3 pivotForward, Vector3 pivotUp, Vector3 pivotRight, float radius, ConsoleColor color)
        {
            Pivot = new(pivot, pivotForward, pivotUp, pivotRight);
            LocalVertices = new Vector3[] { Vector3.Zero };
            GlobalVertices = ToGlobalVertices();
            Radius = radius;
            Color = color;
            Normal = Vector3.Zero;
        }

        public override ICollection<ProjectedVertice> Project(Screen screen, Vector3 light)
        {
            List<ProjectedVertice> projections = new();

            Pivot rotatedPivot = new(
                GlobalVertices[0].RotationInOZ(screen),
                Pivot.Forward.NormalRotationInOZ(screen),
                Pivot.Up.NormalRotationInOZ(screen),
                Pivot.Right.NormalRotationInOZ(screen)
            );

            Pivot projectedPivot = new(
                rotatedPivot.Center * screen.FocalDistance / rotatedPivot.Center.Z,
                rotatedPivot.Forward,
                rotatedPivot.Up,
                rotatedPivot.Right
            );

            float projectedRadius = Radius * screen.FocalDistance / rotatedPivot.Center.Z;

            for (float x = projectedPivot.Center.X - projectedRadius; x <= projectedPivot.Center.X + projectedRadius; ++x)
            {
                float xLocal = rotatedPivot.ToLocalCoords(new(x, 0, screen.FocalDistance)).X;
                float yLocal = (float)Math.Sqrt(Math.Pow(Radius, 2) - Math.Pow(xLocal, 2));

                Vector3 point1 = rotatedPivot.ToGlobalCoords(new(xLocal, yLocal, 0));
                Vector3 point2 = rotatedPivot.ToGlobalCoords(new(xLocal, -yLocal, 0));

                float y1 = point1.Y * screen.FocalDistance / point1.Z;
                float y2 = point2.Y * screen.FocalDistance / point2.Z;

                if (ProjectedVerticeIsInsideScreen(x, y1, point1, Normal, screen, light, out ProjectedVertice projection))
                {
                    projections.Add(projection);
                }

                if (ProjectedVerticeIsInsideScreen(x, y2, point2, Normal, screen, light, out projection))
                {
                    projections.Add(projection);
                }
            }

            for (float y = projectedPivot.Center.Y - projectedRadius; y <= projectedPivot.Center.Y + projectedRadius; ++y)
            {
                float yLocal = rotatedPivot.ToLocalCoords(new(0, y, screen.FocalDistance)).Y;
                float xLocal = (float)Math.Sqrt(Math.Pow(Radius, 2) - Math.Pow(yLocal, 2));

                Vector3 point1 = rotatedPivot.ToGlobalCoords(new(xLocal, yLocal, 0));
                Vector3 point2 = rotatedPivot.ToGlobalCoords(new(-xLocal, yLocal, 0));

                float x1 = point1.X * screen.FocalDistance / point1.Z;
                float x2 = point2.X * screen.FocalDistance / point2.Z;

                if (ProjectedVerticeIsInsideScreen(x1, y, point1, Normal, screen, light, out ProjectedVertice projection))
                {
                    projections.Add(projection);
                }

                if (ProjectedVerticeIsInsideScreen(x2, y, point2, Normal, screen, light, out projection))
                {
                    projections.Add(projection);
                }
            }

            return projections;
        }
    }
}