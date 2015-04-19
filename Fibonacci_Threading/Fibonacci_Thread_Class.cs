using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fibonacci_Threading
{
    /// <summary>
    /// Fibonacci Recursive Calculation worker class.  Objects are created of this class to perform the Fibonacci calculations.
    /// This is done for threading purposes.
    /// </summary>
    public class Fibonacci_Thread_Class
    {
        /// <summary>
        /// Member that keeps track of the maximum number of threads that can be spawned.
        /// MUST BE A NUMBER SUCH THAT: 2^n = max_thread_count
        /// </summary>
        private static long max_thread_count;

        /// <summary>
        /// The static constructor.  Currently just sets thread_count to 0
        /// </summary>
        static Fibonacci_Thread_Class(){
            max_thread_count = 8;
        }

        /// <summary>
        /// This method is the method called to calculate the Fibonacci number. 
        /// It chooses the threaded fibonacci if 2^(n-3) >= max_thread_count.  This determines that there will be max_thread_count values to be calculated (ie that many recursive thread paths even if they just return a number).
        /// Event then, it won't make a difference in calculation speed unless the value of n is larger.
        /// </summary>
        /// <param name="n">The nth Fibonacci number to be calculated</param>
        /// <returns>The nth Fibonacci number</returns>
        public static long fibonacci(long n)
        {
            // The Fibonacci number to calculate goes longo enough recursive depth to start threading
            if (Math.Pow(2, n - 3) >= max_thread_count)
                return thread_fibonacci(n);
            // The Fibonacci number to calculate does not go longo enough depth to start threading
            else
            {
                Recursive_Fibonacci fib = new Recursive_Fibonacci(n);
                fib.Run();
                return fib.Value;
            }
        }

        /// <summary>
        /// Method that is recusrive until the proper depth is found, and then spawns the threads to do threaded recursion.
        /// </summary>
        /// <param name="n"> nth Fibonacci number to calculate</param>
        /// <param name="depth">The current depth level.  When 2^(n+1) == max_thread_count we start spawning threads</param>
        /// <returns>The sum of the two sub-Fibonacci numbers it has calculated at this point (n-1 and n-2)</returns>
        private static long thread_fibonacci(long n, long depth = 0)
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

        /// <summary>
        /// Main driver method
        /// </summary>
        public static void Main()
        {
            long returnedValue = 0;
            long numberToCalculate = 0;

            Console.WriteLine("What Fibonacci number do you wish to calculate?");
            try
            {
                numberToCalculate = long.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.ReadKey();
                return;
            }

            returnedValue = fibonacci(numberToCalculate);

            Console.WriteLine("The " + numberToCalculate + " Fibonacci number is: " + returnedValue);
            Console.ReadKey();
        }
    }

}
