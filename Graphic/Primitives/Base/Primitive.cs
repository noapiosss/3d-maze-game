using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine;
using maze.Engine.Extensions;
using maze.Graphic.Primitives.Base.Interfaces;
using maze.Graphic.Primitives.Extensions;

namespace maze.Graphic.Primitives.Base
{
    public abstract class Primitive : IProjectible
    {
        public Vector3 Normal { get; protected set; }
        public Pivot Pivot { get; protected set; }
        public Vector3[] LocalVertices { get; protected set; }
        public Vector3[] GlobalVertices { get; protected set; }
        public (int, int, int)[] Polygons { get; protected set; }
        public (int, int)[] Lines { get; protected set; }
        public ConsoleColor Color { get; protected set; }

        public void Move(Vector3 v)
        {
            Pivot.Move(v);

            for (int i = 0; i < LocalVertices.Length; i++)
            {
                GlobalVertices[i] += v;
            }
        }

        public void Rotate(Vector3 axis, float angle)
        {
            Pivot.Rotate(axis, angle);

            for (int i = 0; i < LocalVertices.Length; i++)
            {
                GlobalVertices[i] = Pivot.ToGlobalCoords(LocalVertices[i]);
            }
        }

        public abstract ICollection<ProjectedVertice> Project(Camera camera, Vector3 light);

        protected Vector3[] ToGlobalVertices()
        {
            List<Vector3> globalVertices = new();

            foreach (Vector3 localVertices in LocalVertices)
            {
                globalVertices.Add(Pivot.ToGlobalCoords(localVertices));
            }

            return globalVertices.ToArray();
        }

        protected bool ProjectedVerticeIsInsideScreen(float x, float y, Vector3 origin, Vector3 originNormal, Camera camera, Vector3 light, out ProjectedVertice projection)
        {
            projection = new()
            {
                X = (int)Math.Round(x) + (camera.Width / 2),
                Y = (int)Math.Round(y) + (camera.Height / 2),
                Origin = origin,
                Brightness = GetBrightness(origin, originNormal, camera.View(light)),
                Color = Color
            };

            return projection.X >= 0 &&
                projection.X < camera.Width &&
                projection.Y >= 0 &&
                projection.Y < camera.Height &&
                projection.Origin.Z > camera.FocalDistance &&
                Vector3.Distance(projection.Origin, Vector3.Zero) < camera.RenderDistance;
        }

        public static float GetBrightness(Vector3 origin, Vector3 originNormal, Vector3 light)
        {
            if (originNormal == Vector3.Zero)
            {
                return 1;
            }

            float angle = (origin - light).Angle(originNormal);
            return (float)(angle / Math.PI);
        }
    }
}