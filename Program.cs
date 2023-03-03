using System;
using System.Numerics;
using maze.Graphic.Extensions;
using maze.Graphic.Figures;
using maze.Graphic.Primitives;
using Maze.Engine;

Screen screen = new(new(0, 20, 0), new(0, 0, -1), new(0, 1, 0), new(1, 0, 0), 60, 100, 100, 1000, (float)(100 * Math.PI / 180), (float)(90 * Math.PI / 180));
Frame frame = new(screen, 2000);

Vector3 torusCenter = new(0, 0, 200);
Torus torus = new(torusCenter, 25, 15, ConsoleColor.Yellow);

Polygon4 floor1 = new(new(-150, 0, -150), new(-150, 0, 250), new(250, 0, 250), new(250, 0, -150), ConsoleColor.Gray);
// Polygon4 floor2 = new(new(0, -150, -150), new(0, -150, 250), new(0, 250, 250), new(0, 250, -150), ConsoleColor.Green);

// Cube cube = new(new(-10, 10, 120), new(10, -10, 240), ConsoleColor.Red);

frame.AddLight(new(torusCenter.Z - 500, torusCenter.Y - 500, torusCenter.Z));

frame.AddPoints(torus.Points);
// frame.AddPoints(cube.Points);
frame.AddPoints(floor1.Points);
// frame.AddPoints(floor2.Points);
frame.Render();

while (true)
{
    ConsoleKeyInfo key = Console.ReadKey(true);

    if (key.Key == ConsoleKey.UpArrow)
    {
        frame._screen.RotateX(0.1f);
    }
    if (key.Key == ConsoleKey.DownArrow)
    {
        frame._screen.RotateX(-0.1f);
    }
    if (key.Key == ConsoleKey.RightArrow)
    {
        frame._screen.RotateY(0.1f);
    }
    if (key.Key == ConsoleKey.LeftArrow)
    {
        frame._screen.RotateY(-0.1f);
    }
    if (key.Key == ConsoleKey.W)
    {
        frame._screen.MoveForward(10);
    }
    if (key.Key == ConsoleKey.S)
    {
        frame._screen.MoveForward(-10);
    }
    if (key.Key == ConsoleKey.D)
    {
        frame._screen.MoveSide(10);
    }
    if (key.Key == ConsoleKey.A)
    {
        frame._screen.MoveSide(-10);
    }

    frame.Render();
    // Console.WriteLine($"torus center: {torus.Center}");
    Console.WriteLine($"camera position: {frame._screen.CameraPosition}");
    Console.WriteLine($"camera forward: {frame._screen.CameraForward.Length()}");
}