using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fibonacci_Threading
{
    public class Threading_Fibonacci
    {
        /// <summary>
        /// Member that keeps track of the maximum number of threads that can be spawned.
        /// Default value is 8, but can be set using the Max_Thread_Count Property
        /// </summary>
        private static long max_thread_count;

        /// <summary>
        /// When using this property to set the max_thread_count, set the depth at which threading will occur, not the number of threads you wish to have.
        /// ex/ Max_Thread_Count = 2;  This sets max_thread_count = 2^(2) = 4 threads to spawn
        /// 
        /// When this property is used to access the max_thread_count it returns the number of threads to spawn.
        /// </summary>
        public static long Max_Thread_Count
        {
            get
            {
                return max_thread_count;
            }
            set
            {
                max_thread_count = (long)Math.Pow(2, value);
            }
        }

        static Threading_Fibonacci()
        {
            max_thread_count = 8;
        }

        /// <summary>
        /// Method that is recusrive until the proper depth is found, and then spawns the threads to do threaded recursion.
        /// </summary>
        /// <param name="n"> nth Fibonacci number to calculate</param>
        /// <param name="depth">The current depth level.  When 2^(n+1) == max_thread_count we start spawning threads</param>
        /// <returns>The sum of the two sub-Fibonacci numbers it has calculated at this point (n-1 and n-2)</returns>
        public static long thread_fibonacci(long n, long depth = 0)
        {
            // depth + 1 gives us how many paths will be spawned from our current depth, which is where we want to start threading.
            if (Math.Pow(2, depth + 1) == max_thread_count)
            {
                // Create Fibonacci calculation objects
                Recursive_Fibonacci fib1 = new Recursive_Fibonacci(n - 1);
                Recursive_Fibonacci fib2 = new Recursive_Fibonacci(n - 2);

                // Threading starts
                Thread fib_thread_1 = new Thread(new ThreadStart(fib1.Run));
                Thread fib_thread_2 = new Thread(new ThreadStart(fib2.Run));

                // Start the threads
                fib_thread_1.Start();
                fib_thread_2.Start();

                // Wait for the threads to finish
                fib_thread_1.Join();
                fib_thread_2.Join();

                // Add and return the Fibonacci values calculated by the threads.
                return fib1.Value + fib2.Value;
            }
            // We're not deep enough to spawn threads.
            else
            {
                return thread_fibonacci(n - 1, depth + 1) + thread_fibonacci(n - 2, depth + 1);
            }
        }
    }
}
