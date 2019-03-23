using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace persistantNumbers
{
    
    class Program
    {
        /// <summary>
        /// Calculates the persistence of numbers up to uint.MaxValue
        /// </summary>
        /// <param name="token">Cancellation token that will stop this task</param>
        private static void PersistenceTask(CancellationToken token)
        {
            var stopwatch = new Stopwatch();
            int longestPersistence = 0;
            ulong mostPersistent = 0;

            stopwatch.Start();

            ulong i;
            for (i = 0; i < ulong.MaxValue; ++i)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                var p = CalculatePersistence(i);

                if (p > longestPersistence)
                {
                    longestPersistence = p;
                    mostPersistent = i;
                    Console.WriteLine($"Found {i} with persistence {p}");
                }
            }

            stopwatch.Stop();


            Console.WriteLine($"Completed through {i} in {stopwatch.Elapsed}.");
            Console.WriteLine($"Found {mostPersistent} with persistence of {longestPersistence}");
        }

        /// <summary>
        /// Recursively calculates the persistence of a number
        /// </summary>
        /// <param name="number">Persistance will be calculated for this number</param>
        /// <param name="level">Level into this recursive call. This value indicates the current peristence</param>
        /// <returns>The persistence of <paramref name="number"/></returns>
        private static int CalculatePersistence(ulong number, int level = 0)
        {
            if (number <= 9)
            {
                return level;
            }

            string digits = number.ToString();

            // Short circuit
            if (digits.Contains("0")) {
                return level + 1;
            }
            // Short-circuit approximation.
            if(digits.Contains("5"))
            {
                return level + 2;
            }

            ulong newNumber = 1;

            foreach (var i in digits)
            {
                newNumber *= (ulong)Char.GetNumericValue(i);
            }

            return CalculatePersistence(newNumber, ++level);   
        }

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

            var task = Task.Factory.StartNew(() => PersistenceTask(token), source.Token);
            var stopTask = Task.Factory.StartNew(UserExitTask);

            Task.WaitAny(task, stopTask);
           
            source.Cancel();

            task.Wait();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}
