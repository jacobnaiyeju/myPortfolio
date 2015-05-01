using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//To be able to use Arraylist.
using System.Collections;
/*
 * This is one solution to Problem D- Everyone out of the pool in the 
 * ACM International Collegiate Programming Contest 2011.
 * The highlights of this solution are:
 * 1.looping through the specified range once
 * 2.while looping, once the first perfect square is hit,
 *      loop starts counting in the exact sequence to find perfect squares.
 * Created on December 1, 2013
 * Created by:
 *              Jacob Naiyeju - Presenter
 *              
 */
namespace Everyone_out_of_the_pool
{
    class PoolLogic
    {
        public Int64 lLimit;
        public Int64 uLimit;

        /*Validates input for each test case and sets lLimit, 
         *lower-limit and ulimit, upper-limit*/
        public bool validateInput(string input1, string input2)
        {
            bool isValid = false;
            bool input1Test;
            bool input2Test;
            input1Test = Int64.TryParse(input1, out lLimit);
            input2Test = Int64.TryParse(input2, out uLimit);
            if (input1Test && input2Test)
                isValid = true;
            return isValid;
        }
        /*
         * Checks if all balls including cue ball fit a square
         */
        public bool checkBallsInSquare(Int64 value)
        {
            bool itFits = false;
            double result1;
            double result2;
            result1 = Math.Sqrt(value);
            //extracts the whole number part of the result.
            result2 = Math.Truncate(result1);
            //result1 and result2 will only match when value is a perfect square
            if (result1 == result2)
                itFits = true;
            return itFits;
        }
        /*
         * This method checks if pool balls excluding the cue ball fit a triangle.
         * Check was done using the Sum of an arithmetic progression to find the nth term.
         * Sum referring to the total amount of balls to be checked and nth term referring
         * to the number of balls in the last-row/base of triangle.
         * Using the Almighty formula- 2*a*x = -b '+/-' Math.Sqrt(b^2 +(4*a*c)) ,
         * where
         * a=1, first term
         * b=1, each row added to the base of the triangle at any point 
         *      has a increment of 1 compared to the previous
         * c=Sum, number of balls to be tested
         * x(the positive root) = nth term, number of balls on the last-row/base of triangle
         * For Sums that fit the triangle;
         * the difference between the absolute values of the roots(x1 and x2) is 1
         * the roots are always integers
         */
        public bool checkBallsInTriangle(Int64 value)
        {
            bool itFits = false;
            double x1;
            double x2;
            value--;

            // Using almighty formula: a=1,b=1,c=value; to find roots
            x1 = ((-1) + (Math.Sqrt(1 + (4 * (2 * value))))) / (2 * 1);
            x2 = ((-1) - (Math.Sqrt(1 + (4 * (2 * value))))) / (2 * 1);

            //Tests on roots to see if it meets established criteria for a fit.
            if (Math.Abs(Math.Abs(x1) - Math.Abs(x2)) == 1)
            {
                if (x1 > 0 && (x1 == Math.Truncate(x1)))
                {
                    itFits = true;
                }
                else if (x2 > 0 && (x2 == Math.Truncate(x2)))
                {
                    itFits = true;
                }
                else
                    itFits = false;
            }
            return itFits;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Everyone out of the pool");
            Console.WriteLine("=================================================");
            Console.WriteLine("Enter each Lower limit and upper limit separated by a space");
            Console.WriteLine("i.e 'Lower-limit Upper-limit'");
            Console.WriteLine("Enter Test case '0 0' to indicate last test case");
            Console.WriteLine("");
            int counter = 0;
            ArrayList testdatas = new ArrayList();
            bool doRepeat = true;
            do
            {
                Console.WriteLine("Enter Test case "+ ++counter);
                string input = Console.ReadLine();
                string[] token = input.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                testdatas.Add(token);
                if (token[0] == "0" && token[1] == "0")
                {
                    doRepeat = false;
                }
            }
            while (doRepeat);
            counter = 0;
            PoolLogic pool = new PoolLogic();
            Console.WriteLine("");
            Console.WriteLine("Results");
            Console.WriteLine("=================================================");
            foreach(string[] testdata in testdatas)
            {
                int totalFits = 0;
                Console.Write("Case " + (++counter).ToString() + " : ");
                if (!(testdata.Length == 2)) 
                {
                    Console.WriteLine("Your test values for case: " + counter.ToString() + " was not valid");
                    Console.WriteLine("=================================================");
                    // ignores the remaining code after and continues looping
                    continue;
                }
                if (pool.validateInput(testdata[0], testdata[1]))
                {
                    for (Int64 count = pool.lLimit; count < pool.uLimit + 1; count++)
                    {
                        if (pool.checkBallsInSquare(count))
                        {
                            if (pool.checkBallsInTriangle(count))
                            {
                                totalFits++;
                                //Console.WriteLine(count);
                            }
                            /*
                             * once a perfect square is hit for the first time. 
                             * counter is made to key into the sequence of perfect squares
                             * to always find the next value
                             */
                            count = (Int64)Math.Pow((Math.Sqrt(count) + 1), 2);
                            count--;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Your test values for case: " + counter.ToString() + " was not valid");
                    Console.WriteLine("=================================================");
                    continue;
                }
                Console.WriteLine("You have " + totalFits + " Total Fits");
                Console.WriteLine("=================================================");
            }
            Console.Read();
        }
    }
}
