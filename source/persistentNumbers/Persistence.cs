using System;
using System.Diagnostics;
using System.Threading;

namespace persistantNumbers
{
    public class Persistence
    {
        /// <summary>
        /// Calculates the persistence of numbers up to uint.MaxValue
        /// </summary>
        /// <param name="token">Cancellation token that will stop this task</param>
        public void PersistenceTask(CancellationToken token)
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

        private static readonly char[] EVEN_DIGITS = new char[] { '2', '4', '6', '8' };

        /// <summary>
        /// Recursively calculates the persistence of a number
        /// </summary>
        /// <param name="number">Persistance will be calculated for this number</param>
        /// <param name="level">Level into this recursive call. This value indicates the current peristence</param>
        /// <returns>The persistence of <paramref name="number"/></returns>
        public int CalculatePersistence(ulong number, int level = 0)
        {
            if (number <= 9)
            {
                return level;
            }

            string digits = number.ToString();

            // Short circuit
            if (digits.Contains("0"))
            {
                // The next level will be n x 0 ending the persistence check
                return level + 1;
            }
            // Short-circuit approximation.
            if (digits.Contains("5"))
            {
                // If it contains an even number
                if (digits.IndexOfAny(EVEN_DIGITS) > 0)
                {
                    // 5 x EVEN * n = m x 10, so the next level will satisfy the ContainsZero short circuit
                    return level + 2;
                }

                // If it contains no even numbers
                // Then the next level will contain an even number, satisfying the above condition
                return level + 3;
            }

            ulong newNumber = 1;

            foreach (var i in digits)
            {
                newNumber *= (ulong)Char.GetNumericValue(i);
            }

            return CalculatePersistence(newNumber, ++level);
        }
    }
}
