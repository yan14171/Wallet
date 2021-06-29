using System;

namespace Projects.QueriesUI
{
    internal class UI
    {
        private readonly int taskAmount;
        private readonly string[] tasks;
        private readonly string[] taskNames;

        public UI(string[] tasks, string[] taskNames)
        {
            if (tasks.Length != taskNames.Length) throw new Exception("taskNames length != tasks length");

            this.taskAmount = taskNames.Length;
            this.tasks = tasks;
            this.taskNames = taskNames;
        }

        internal (string Method, int Parameter) GetTaskNumber()
        {
            int initialX = Console.CursorLeft;
            int initialY = Console.CursorTop;

            Console.SetCursorPosition(40, 0);
            Console.WriteLine("Use arrows to navigate menu. Enter to choose the task");

            Console.SetCursorPosition(initialX, initialY);

            var taskRes = KeyLoop();

            return (tasks[taskRes.Method-1],taskRes.Parameter);
        }

        private (int Method, int Parameter) KeyLoop()
        {
            Console.CursorVisible = false;

            int loopY = 0;

            while (true)
            {
                Console.SetCursorPosition(0, 0);

                for (int i = 0; i < taskAmount; i++)
                {
                    if(Console.CursorTop == loopY)
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Task " + taskNames[i]);

                    Console.ForegroundColor = ConsoleColor.White;
                }

                switch (Console.ReadKey(intercept: true).Key)
                {
                    case ConsoleKey.DownArrow:
                        if (loopY < taskAmount-1) loopY++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (loopY > 0) loopY--;
                        break;
                    case ConsoleKey.Enter:
                        {
                            int param = 0;

                            return (loopY + 1, param);
                        }
                }
            }
        }

       
    }
}