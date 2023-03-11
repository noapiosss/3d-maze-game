using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Engine.Extensions;
using maze.Graphic.Primitives.Base;
using maze.Graphic.Primitives.Extensions;
using maze.Graphic.Primitives.Helpres;

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

        public override ICollection<ProjectedVertice> Project(Camera camera, Vector3 light)
        {
            List<ProjectedVertice> projections = new();

            Pivot rotatedPivot = new(
                camera.View(GlobalVertices[0]),
                camera.LookAt(Pivot.Forward),
                camera.LookAt(Pivot.Up),
                camera.LookAt(Pivot.Right)
            );

            Vector3 W1 = Pivot.ToGlobalCoords(new(-Radius, -Radius, 0));
            Vector3 X1 = Pivot.ToGlobalCoords(new(-Radius, Radius, 0));
            Vector3 Y1 = Pivot.ToGlobalCoords(new(Radius, Radius, 0));
            Vector3 Z1 = Pivot.ToGlobalCoords(new(Radius, -Radius, 0));

            Vector3 W = camera.View(W1);
            Vector3 X = camera.View(X1);
            Vector3 Y = camera.View(Y1);
            Vector3 Z = camera.View(Z1);

            Ellipse ellipse = new(
                W * camera.FocalDistance / W.Z,
                X * camera.FocalDistance / X.Z,
                Y * camera.FocalDistance / Y.Z,
                Z * camera.FocalDistance / Z.Z
            );

            for (float x = -ellipse.RadiusA; x <= ellipse.RadiusA; ++x)
            {
                float y1 = (float)(ellipse.RadiusB * Math.Sqrt(1 - (x * x / (ellipse.RadiusA * ellipse.RadiusA))));
                float y2 = (float)(-ellipse.RadiusB * Math.Sqrt(1 - (x * x / (ellipse.RadiusA * ellipse.RadiusA))));

                Vector3 point1 = new Vector3(x, y1, camera.FocalDistance).RotateZ(ellipse.Angle) + new Vector3(ellipse.CenterX, ellipse.CenterY, camera.FocalDistance);
                Vector3 point2 = new Vector3(x, y2, camera.FocalDistance).RotateZ(ellipse.Angle) + new Vector3(ellipse.CenterX, ellipse.CenterY, camera.FocalDistance);

                if (Intersections.LinePlaneIntersection(Vector3.Zero, point1, rotatedPivot.Forward, rotatedPivot.Center, out Vector3 origin1))
                {
                    if (ProjectedVerticeIsInsideScreen(point1.X, point1.Y, origin1, Normal, camera, light, out ProjectedVertice projection))
                    {
                        projections.Add(projection);
                    }
                }

                if (Intersections.LinePlaneIntersection(Vector3.Zero, point2, rotatedPivot.Forward, rotatedPivot.Center, out Vector3 origin2))
                {
                    if (ProjectedVerticeIsInsideScreen(point2.X, point2.Y, origin2, Normal, camera, light, out ProjectedVertice projection))
                    {
                        projections.Add(projection);
                    }
                }
            }

            for (float y = -ellipse.RadiusB; y <= ellipse.RadiusB; ++y)
            {
                float x1 = (float)(ellipse.RadiusA * Math.Sqrt(1 - (y * y / (ellipse.RadiusB * ellipse.RadiusB))));
                float x2 = (float)(-ellipse.RadiusA * Math.Sqrt(1 - (y * y / (ellipse.RadiusB * ellipse.RadiusB))));

                Vector3 point1 = new Vector3(x1, y, camera.FocalDistance).RotateZ(ellipse.Angle) + new Vector3(ellipse.CenterX, ellipse.CenterY, camera.FocalDistance);
                Vector3 point2 = new Vector3(x2, y, camera.FocalDistance).RotateZ(ellipse.Angle) + new Vector3(ellipse.CenterX, ellipse.CenterY, camera.FocalDistance);

                if (Intersections.LinePlaneIntersection(Vector3.Zero, point1, rotatedPivot.Forward, rotatedPivot.Center, out Vector3 origin1))
                {
                    if (ProjectedVerticeIsInsideScreen(point1.X, point1.Y, origin1, Normal, camera, light, out ProjectedVertice projection))
                    {
                        projections.Add(projection);
                    }
                }

                if (Intersections.LinePlaneIntersection(Vector3.Zero, point2, rotatedPivot.Forward, rotatedPivot.Center, out Vector3 origin2))
                {
                    if (ProjectedVerticeIsInsideScreen(point2.X, point2.Y, origin2, Normal, camera, light, out ProjectedVertice projection))
                    {
                        projections.Add(projection);
                    }
                }
            }


            return projections;
        }
    }
}