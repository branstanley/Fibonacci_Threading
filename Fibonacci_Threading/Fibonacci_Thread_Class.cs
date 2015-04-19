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
        /// This method is the method called to calculate the Fibonacci number. 
        /// It chooses the threaded fibonacci if 2^(n-3) >= max_thread_count.  This determines that there will be max_thread_count values to be calculated (ie that many recursive thread paths even if they just return a number).
        /// Event then, it won't make a difference in calculation speed unless the value of n is larger.
        /// </summary>
        /// <param name="n">The nth Fibonacci number to be calculated</param>
        /// <returns>The nth Fibonacci number</returns>
        public static long fibonacci(long n)
        {
            // The Fibonacci number to calculate goes longo enough recursive depth to start threading
            if (Math.Pow(2, n - 3) >= Threading_Fibonacci.Max_Thread_Count)
                return Threading_Fibonacci.thread_fibonacci(n);
            // The Fibonacci number to calculate does not go longo enough depth to start threading
            else
            {
                Recursive_Fibonacci fib = new Recursive_Fibonacci(n);
                fib.Run();
                return fib.Value;
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
