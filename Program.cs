using System;
using maze.Graphic.Extensions;
using maze.Engine;
using maze.Graphic.Primitives;

Screen screen = new(new(0, 0, 0), new(0, 0, 1), new(0, 1, 0), new(1, 0, 0), 60, 200, 100, 300);
Frame frame = new(screen);

// Vertice vertice = new(new(0, 0, 150), ConsoleColor.DarkGreen);
// frame.AddPrimitive(vertice);

Line lineX = new(new(-40, 0, 110), new(60, 0, 110), ConsoleColor.Green);
frame.AddPrimitive(lineX);
Line lineY = new(new(-40, 0, 110), new(-40, 100, 110), ConsoleColor.Red);
frame.AddPrimitive(lineY);
Line lineZ = new(new(-40, 0, 110), new(-40, 0, 210), ConsoleColor.Blue);
frame.AddPrimitive(lineZ);

// CubeEdges cubeEdges1 = new(new(0, 0, 150), 21, ConsoleColor.White);
// frame.AddPrimitive(cubeEdges1);

// Cube cube1 = new(new(0, 0, 150), 20, ConsoleColor.Red);
// frame.AddPrimitive(cube1);

// CubeEdges cubeEdges2 = new(new(-30, 0, 150), 21, ConsoleColor.White);
// frame.AddPrimitive(cubeEdges2);

// Cube cube2 = new(new(-30, 0, 150), 20, ConsoleColor.Green);
// frame.AddPrimitive(cube2);

CubeEdges cubeEdges3 = new(new(30, 0, 150), 22, ConsoleColor.White);
frame.AddPrimitive(cubeEdges3);

Cube cube3 = new(new(30, 0, 150), 20, ConsoleColor.Blue);
frame.AddPrimitive(cube3);

// Sphere sphere = new(new(0, 0, 150), 10, ConsoleColor.DarkYellow);
// frame.AddPrimitive(sphere);

Circle circle1 = new(new(0, 0, 150), 11, ConsoleColor.White);
frame.AddPrimitive(circle1);

// Circle circle2 = new(new(0, 0, 150), Vector3.UnitY, -Vector3.UnitZ, Vector3.UnitX, 11, ConsoleColor.White);
// frame.AddPrimitive(circle2);

// Circle circle3 = new(new(0, 0, 150), Vector3.UnitX, Vector3.UnitY, -Vector3.UnitZ, 11, ConsoleColor.White);
// frame.AddPrimitive(circle3);

// Polygon floor1 = new(new(-100, -11, 50), new(-100, -11, 250), new(100, -11, 250), ConsoleColor.DarkYellow);
// Polygon floor2 = new(new(-100, -11, 50), new(100, -11, 250), new(100, -11, 50), ConsoleColor.DarkYellow);
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