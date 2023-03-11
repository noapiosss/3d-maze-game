using System;

namespace maze.Engine
{
    public class Controller
    {
        private readonly Frame _frame;
        public Controller(Frame frame)
        {
            _frame = frame;
        }

        public void Run()
        {
            _frame.Render();

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    _frame.LookUp(0.05000f);
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    _frame.LookUp(-0.05000f);
                }
                if (key.Key == ConsoleKey.RightArrow)
                {
                    _frame.LookSide(0.05000f);
                }
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    _frame.LookSide(-0.05000f);
                }
                if (key.Key == ConsoleKey.W)
                {
                    _frame.MoveForward(10);
                }
                if (key.Key == ConsoleKey.S)
                {
                    _frame.MoveForward(-10);
                }
                if (key.Key == ConsoleKey.D)
                {
                    _frame.MoveSide(10);
                }
                if (key.Key == ConsoleKey.A)
                {
                    _frame.MoveSide(-10);
                }
                if (key.Key == ConsoleKey.Spacebar)
                {
                    _frame.MoveUp(10);
                }
                if (key.Key == ConsoleKey.C)
                {
                    _frame.MoveUp(-10);
                }

                _frame.Render();
            }
        }
    }
}