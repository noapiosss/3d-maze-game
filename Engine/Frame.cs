using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Graphic.Extensions;
using maze.Graphic.Primitives;

namespace Maze.Engine
{
    public class Frame
    {
        private static readonly char[] _brightnessGradient = new char[] { '@', '%', '$', '#', '!', '=', ';', ':', '~', '-', ',', '.' };
        public Screen _screen;
        private readonly List<Vector3> _light = new();
        private readonly float _renderDistance;
        private readonly char[,] _pixels;
        private readonly ConsoleColor[,] _colors;
        private readonly float[,] _depthBuffer;
        private readonly List<Point> _points = new();

        public Frame(Screen screen, float renderDistance)
        {
            _screen = screen;
            _renderDistance = renderDistance;
            _pixels = new char[screen.Width, screen.Height];
            _colors = new ConsoleColor[screen.Width, screen.Height];
            _depthBuffer = new float[screen.Width, screen.Height];

            for (int i = 0; i < _screen.Width; ++i)
            {
                for (int j = _screen.Height - 1; j >= 0; --j)
                {
                    _pixels[i, j] = ' ';
                    _colors[i, j] = ConsoleColor.White;
                    _depthBuffer[i, j] = _renderDistance;
                }
            }
        }

        public void AddLight(Vector3 light)
        {
            _light.Add(light);
        }

        public void AddPoints(Point[] points)
        {
            foreach (Point point in points)
            {
                _points.Add(point);
            }
        }

        public void AddPolygon3s(Polygon3[] polygon3s)
        {
            foreach (Polygon3 polygon3 in polygon3s)
            {
                _points.AddRange(polygon3.Points);
            }
        }

        public void AddPolygon4s(Polygon4[] polygon4s)
        {
            foreach (Polygon4 polygon4 in polygon4s)
            {
                _points.AddRange(polygon4.Points);
            }
        }

        private void ProjectPoints()
        {
            foreach (Point point in _points)
            {
                Vector3 p = point.Position.Projection(_screen);
                if (p.Z > _renderDistance || p.Z < _screen.FocalDistance)
                {
                    continue;
                }

                int x = (int)(p.X * _screen.FocalDistance / p.Z) + (_screen.Width / 2);
                int y = (int)(p.Y * _screen.FocalDistance / p.Z) + (_screen.Height / 2);

                float distance = Vector3.Distance(_screen.CameraPosition, point.Position);

                if (x >= 0 && x < _screen.Width && y >= 0 && y < _screen.Height && _depthBuffer[x, y] > distance)
                {
                    _colors[x, y] = point.Color;
                    _pixels[x, y] = CalculateBrightness(point, _light[0]);
                    _depthBuffer[x, y] = distance;
                }
            }
        }

        private void Clear()
        {
            for (int i = 0; i < _screen.Width; ++i)
            {
                for (int j = _screen.Height - 1; j >= 0; --j)
                {
                    _pixels[i, j] = ' ';
                    _depthBuffer[i, j] = _renderDistance;
                }
            }
        }

        private static char CalculateBrightness(Point point, Vector3 light)
        {
            float angle = (point.Position - light).Angle(point.Normal);
            return _brightnessGradient[(int)Math.Round((_brightnessGradient.Length - 1) * angle / Math.PI)]; ;
        }

        public void Render()
        {
            Console.Clear();
            Clear();
            ProjectPoints();

            for (int j = _screen.Height - 1; j >= 0; --j)
            {
                for (int i = 0; i < _screen.Width; ++i)
                {
                    Console.ForegroundColor = _colors[i, j];
                    Console.Write(_pixels[i, j]);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}