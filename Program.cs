using System;
using maze.Graphic.Extensions;
using maze.Engine;
using maze.Graphic.Primitives;
using System.Numerics;

Screen screen = new(new(0, 0, 0), new(0, 0, 1), new(0, 1, 0), new(1, 0, 0), 60, 200, 100, 300);
Frame frame = new(screen);

// Vertice vertice = new(new(0, 0, 150), ConsoleColor.DarkGreen);
// frame.AddPrimitive(vertice);

// Line line1 = new(new(-20, 0, 150), new(20, 0, 150), ConsoleColor.Green);
// frame.AddPrimitive(line1);

// Line line2 = new(new(0, -20, 150), new(0, 20, 150), ConsoleColor.Green);
// frame.AddPrimitive(line2);

// CubeEdges cubeEdges1 = new(new(0, 10, 100), 21, ConsoleColor.White);
// frame.AddPrimitive(cubeEdges1);

// Cube cube1 = new(new(0, 10, 100), 20, ConsoleColor.Red);
// frame.AddPrimitive(cube1);

// CubeEdges cubeEdges2 = new(new(-30, 10, 100), 21, ConsoleColor.White);
// frame.AddPrimitive(cubeEdges2);

// Cube cube2 = new(new(-30, 10, 100), 20, ConsoleColor.Green);
// frame.AddPrimitive(cube2);

CubeEdges cubeEdges3 = new(new(40, 0, 150), 21, ConsoleColor.White);
frame.AddPrimitive(cubeEdges3);

Cube cube3 = new(new(40, 0, 150), 20, ConsoleColor.Blue);
frame.AddPrimitive(cube3);

// Sphere sphere = new(new(0, 0, 150), 10, ConsoleColor.DarkYellow);
// frame.AddPrimitive(sphere);

Circle circle1 = new(new(0, 0, 150), Vector3.UnitZ, Vector3.UnitY, Vector3.UnitX, 11, ConsoleColor.White);
frame.AddPrimitive(circle1);

// Circle circle2 = new(new(0, 0, 150), Vector3.UnitY, -Vector3.UnitZ, Vector3.UnitX, 11, ConsoleColor.White);
// frame.AddPrimitive(circle2);

// Circle circle3 = new(new(0, 0, 150), Vector3.UnitX, Vector3.UnitY, -Vector3.UnitZ, 11, ConsoleColor.White);
// frame.AddPrimitive(circle3);

// Polygon floor1 = new(new(-100, 0, 50), new(-100, 0, 250), new(100, 0, 250), ConsoleColor.Gray);
// Polygon floor2 = new(new(-100, 0, 50), new(100, 0, 250), new(100, 0, 50), ConsoleColor.Gray);
// frame.AddPrimitive(floor1);
// frame.AddPrimitive(floor2);


frame.AddLight(new(200, 200, 50));
frame.Render();

while (true)
{
    ConsoleKeyInfo key = Console.ReadKey(true);

    if (key.Key == ConsoleKey.UpArrow)
    {
        frame._screen.LookUp(0.05000f);
    }
    if (key.Key == ConsoleKey.DownArrow)
    {
        frame._screen.LookUp(-0.05000f);
    }
    if (key.Key == ConsoleKey.RightArrow)
    {
        frame._screen.LookSide(0.05000f);
    }
    if (key.Key == ConsoleKey.LeftArrow)
    {
        frame._screen.LookSide(-0.05000f);
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
    if (key.Key == ConsoleKey.Spacebar)
    {
        frame._screen.MoveUp(10);
    }
    if (key.Key == ConsoleKey.C)
    {
        frame._screen.MoveUp(-10);
    }

    frame.Render();
    Console.SetCursorPosition(0, 0);
    Console.WriteLine($"camera position: {frame._screen.CameraPosition}");
    // Console.WriteLine($"camera forward: {frame._screen.CameraForward}");
    // Console.WriteLine($"camera up: {frame._screen.CameraUp}");
    // Console.WriteLine($"camera right: {frame._screen.CameraRight}");
}