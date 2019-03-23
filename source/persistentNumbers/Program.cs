using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace persistantNumbers
{

    class Program
    {
        /// <summary>
        /// Monitors the console to see if the user has requested to quit
        /// </summary>
        private static void UserExitTask()
        {
            bool stopped = false;

            while (!stopped)
            {
                var input = Console.ReadKey(true);

                stopped = input.Key == ConsoleKey.Spacebar;
            }
        }

        static void Main(string[] args)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            Console.WriteLine("Finding peristent numbers. Press SPACE to stop");

            var persistence = new Persistence();

            var task = Task.Factory.StartNew(() => persistence.PersistenceTask(token), source.Token);
            var stopTask = Task.Factory.StartNew(UserExitTask);

            Task.WaitAny(task, stopTask);
           
            source.Cancel();

            task.Wait();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}
