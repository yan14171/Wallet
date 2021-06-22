using System;

namespace QueriesUI
{
    internal class UI
    {
        public UI()
        {
        }

        internal (int Method, int Parameter) GetTaskNumber()
        {
            int initialX = Console.CursorLeft;
            int initialY = Console.CursorTop;

            Console.SetCursorPosition(40, 0);
            Console.WriteLine("Use arrows to navigate menu. Enter to choose the task");

            Console.SetCursorPosition(initialX, initialY);

            return KeyLoop();
        }

        private (int Method, int Parameter) KeyLoop()
        {
            Console.CursorVisible = false;

            int loopY = 0;

            while (true)
            {
                Console.SetCursorPosition(0, 0);

                for (int i = 1; i < 8; i++)
                {
                    if(Console.CursorTop == loopY)
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Task " + i);

                    Console.ForegroundColor = ConsoleColor.White;
                }

                switch (Console.ReadKey(intercept: true).Key)
                {
                    case ConsoleKey.DownArrow:
                        if (loopY < 6) loopY++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (loopY > 0) loopY--;
                        break;
                    case ConsoleKey.Enter:
                        {
                            int param = 0;

                            if (loopY== 0 || loopY == 1 || loopY == 2 || loopY == 5)
                             param = GetParameter();

                            return (loopY + 1, param);
                        }
                }
            }
        }

        private int GetParameter()
        {
            Console.Clear();
            Console.WriteLine("This method accepts additionl Id parameter :");

            int param;
            while (true)
            {
                try
                {
                    param = Convert.ToInt32(Console.ReadLine());

                    break;
                }
                catch { }
            }

            return param;
        }
    }
}