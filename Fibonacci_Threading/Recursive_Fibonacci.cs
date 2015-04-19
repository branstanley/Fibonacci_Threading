using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci_Threading
{
    class Recursive_Fibonacci
    {        /// <summary>
        /// The Nth Fibonacci number to calculate
        /// </summary>
        private long to_calc;
        /// <summary>
        /// The Fibonacci number we've calculated
        /// </summary>
        private long value;
        /// <summary>
        /// Property used to access the Fibonacci number calculated.
        /// </summary>
        public long Value
        {
            get
            {
                return value;
            }
        }

        /// <summary>
        /// Fibonacci number calculation class.  Performs the recursive calculation of the Fibonacci number
        /// </summary>
        /// <param name="n">The nth Fibonacci number to calculate</param>
        public Recursive_Fibonacci(long n)
        {
            to_calc = n;
            value = 0;
        }

        /// <summary>
        /// Used to start calculating the nth Fibonacci number.
        /// </summary>
        public void Run()
        {
            recursive_fibonacci(to_calc);
        }

        /// <summary>
        /// The recusive fibonacci function.  Recursively calculates the Fibonacci value from the value given to it.
        /// Stores the number calculated in value
        /// </summary>
        /// <param name="n">The current value to calculate the Fibonacci value from.</param>
        private void recursive_fibonacci(long n)
        {
            if (n == 0)
            {
                return;
            }
            else if (n == 1)
            {
                value += 1;
                return;
            }
            else
            {
                recursive_fibonacci(n - 1);
                recursive_fibonacci(n - 2);
            }
        }
    }
}
