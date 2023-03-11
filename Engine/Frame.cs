using System;
using System.Collections.Generic;
using System.Numerics;
using maze.Engine.Helpers.cs;
using maze.Graphic.Primitives.Base;

namespace maze.Engine
{
    public class Frame
    {
        private static readonly char[] _brightnessGradient = " .:-~=+xX#%@".ToCharArray();
        //private static readonly char[] _brightnessGradient = " .`:,;'_^\"\\></-!~=)(|j?}{][ti+l7v1%yrfcJ32uIC$zwo96sngaT5qpkYVOL40&mG8*xhedbZUSAQPFDXWK#RNEHBM@".ToCharArray();
        private readonly Camera _camera;
        private readonly List<Vector3> _light = new();
        private readonly List<Primitive> _primitives = new();
        private readonly float[,] _depthBuffer;
        private readonly char[,] _pixels;
        private readonly ConsoleColor[,] _colors;
        private readonly ConsoleHelper _consoleHelper;


        public Frame(Camera camera)
        {
            _camera = camera;
            _pixels = new char[camera.Width, camera.Height];
            _colors = new ConsoleColor[camera.Width, camera.Height];
            _depthBuffer = new float[camera.Width, camera.Height];
            _consoleHelper = new(camera.Height, camera.Width);

            for (int i = 0; i < camera.Width; ++i)
            {
                for (int j = camera.Height - 1; j >= 0; --j)
                {
                    _pixels[i, j] = ' ';
                    _colors[i, j] = ConsoleColor.White;
                    _depthBuffer[i, j] = camera.RenderDistance;
                }
            }
        }

        public void AddLight(Vector3 light)
        {
            _light.Add(light);
        }

        public void AddPrimitive(Primitive primitive)
        {
            _primitives.Add(primitive);
        }

        private void Project()
        {
            foreach (Primitive primitive in _primitives)
            {
                foreach (ProjectedVertice projection in primitive.Project(_camera, _light[0]))
                {
                    FillPixel(projection);
                }
            }
        }

        private void FillPixel(ProjectedVertice projection)
        {
            float distance = Vector3.Distance(projection.Origin, Vector3.Zero);

            if (_depthBuffer[projection.X, projection.Y] > distance)
            {
                _depthBuffer[projection.X, projection.Y] = distance;
                _colors[projection.X, projection.Y] = projection.Color;
                _pixels[projection.X, projection.Y] = _brightnessGradient[(int)Math.Round((_brightnessGradient.Length - 1) * projection.Brightness)];
            }
        }

        private void Clear()
        {
            for (int i = 0; i < _camera.Width; ++i)
            {
                for (int j = _camera.Height - 1; j >= 0; --j)
                {
                    _pixels[i, j] = ' ';
                    _depthBuffer[i, j] = _camera.RenderDistance;
                }
            }
        }

        public void Render()
        {
            Clear();
            Project();
            _consoleHelper.Draw(_pixels, _colors);

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LookUp(float angle)
        {
            _camera.LookUp(angle);
        }

        public void LookSide(float angle)
        {
            _camera.LookSide(angle);
        }

        public void MoveForward(float distance)
        {
            _camera.MoveForward(distance);
        }

        public void MoveSide(float distance)
        {
            _camera.MoveSide(distance);
        }

        public void MoveUp(float distance)
        {
            _camera.MoveUp(distance);
        }
    }
}