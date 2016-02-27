using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillyAndChocolate
{
    /*
     * Problem statement:
     * 
     * See webpage:
     * https://www.hackerearth.com/druva-sdet-hiring-challenge/algorithm/milly-and-chocolates-4/
     * 
     * https://www.hackerearth.com/druva-sdet-hiring-challenge/
     * 
     Milly loves to eat chocolates. She buys only those food items which contain some amount or 
     * percentage of chocolate in it. She has purchased N such food items and now she is planning 
     * to make a new food item by her own. She will take equal proportions of all of these N food 
     * items and mix them. Now she is confused about the percentage of chocolate that this new 
     * food item will have. Since she is busy in eating the chocolates so you have to help her in this task.

    Input

    First line of the input will contain T (no. of test cases). Every test case will contain two lines. 
     * First line will contain N (no. of food items) and the second line will contain N space separated Pi 
     * values denoting the percentage of chocolate in ith food item.

    Output

    For every test case, print the percentage of chocolate that will be present in the new food item.
    Note : Your answer should be exactly upto 8 decimal places which means that if your answer is 2.357 then 
     * you have to print 2.35700000 or if your answer is 2.66666666 .... then you have to print 2.66666667

    Constraints

    1 <= T <= 5
    1 <= N <= 5*105
    0 <= Pi <= 100

    Sample Input(Plaintext Link)
     1
    3
    80 30 90
     * 
     */

    class Solution 
    {
        static void Main(string[] args)
        {
            string s1 = Console.ReadLine();
            int T = Convert.ToInt16(s1.Trim());

            IList<long> sizelist = new List<long>();
            IList<string[]> list = new List<string[]>(); 

            for(int i=0;i<T;i++)
            {
                string s2 = Console.ReadLine();
                string s3 = Console.ReadLine();
                long N = Convert.ToInt64(s2.Trim());
                sizelist.Add(N);

                string[] A = s3.Split(' ');
                list.Add(A); 
            }

            for(int i=0; i<sizelist.Count;i++)
            {
                long N = sizelist[i];
                string[] A = list[i];

                double d = average2(N, A);
                Console.WriteLine(ToStringWithEightDeciaml(toDecimal(d))); 
            }
        }

        public static double average2(long N, string[] A)
        {
            UInt16[] B = new UInt16[N]; 

            for(long i = 0; i< N; i++)
            {
                B[i] = convertToInt(A[i].Trim()); 
            }

            return average(N, B); 
        }

        private static UInt16 convertToInt(string s)
        {
            int sum = 0; 
            for(int i=0;i<s.Length;i++)
            {
                sum = (s[i] - '0') + sum*10;  
            }

            return Convert.ToUInt16(sum); 
        }
        /*
         *   5 x 10^5  
         *   Feb. 27, 2016
         *   TLE - do not call /N multiple times, only at the end of sum iteration, 
         *   Do one /N only 
         *   double - the sum is not exceeding the max value
         */
        public static double average_bugRunTimeExceedMax(long N, UInt16[] A)
        {
            double val = 0;

            if (N <= 0)
                return 0;
            for (int i = 0; i < A.Length; i++)
            {
                //val += A[i]*1.0/N; 
                val += A[i] * 1.0;  ; 
            }
            val = val / N; 

            return val; 
        }

        /*
         *   5 x 10^5  
         *   Feb. 27, 2016
         *   TLE - do not call /N multiple times, only at the end of sum iteration, 
         *   Do one /N only 
         *   double - the sum is not exceeding the max value
         *   - how to avoid TLE but not exceed runtime error? 
         */
        public static double average(long N, UInt16[] A)
        {
            double val = 0;

            if (N <= 0)
                return 0;
            double afterDiv = 0; 
            for (int i = 0; i < A.Length; i++)
            {
                if (val < double.MaxValue - 200)
                {
                    //val += A[i]*1.0/N; 
                    val += A[i] * 1.0;
                }
                else
                {
                    afterDiv += val / N;
                    val = A[i] * 1.0; 
                }
            }

            val = val / N;

            return afterDiv + val;
        }

        /*
         * https://msdn.microsoft.com/en-us/library/9s0xa85y.aspx
         */
        private static decimal toDecimal(double val)
        {
            decimal d = Convert.ToDecimal(val);
            d = Math.Round(d, 8, MidpointRounding.AwayFromZero);
            return d; 
        }

        /* bug fix if your answer is 2.357 then you have to print 2.35700000
         * 
         * 
         * */
        private static string ToStringWithEightDeciaml(decimal d)
        {
            string s1 = d.ToString();

            //bool havingDot = s1.Contains('.');
            bool havingDot = stringContainsDot(s1); 
            if(havingDot)
            {
                int pos = dotPos(s1);
                int len = s1.Length - pos -1; // missing 0 
                if (len == 8)
                    return s1;
                else if (len < 8)
                    return s1 + zeroString(8 - len);
                else
                    return s1; 
            }
            else
            {
                return s1 +"." +zeroString(8); 
            }
        }

        private static bool stringContainsDot(string s)
        {
            for(int i=0;i<s.Length;i++)
            {
                if (s[i] == '.')
                    return true; 
            }
            return false; 
        }

        private static int dotPos(string s)
        {
            for(int i =0;i<s.Length;i++)
            {
                if (s[i] == '.')
                    return i; 
            }
            return -1; 
        }

        private static string zeroString(int len)
        {
            string s = "";
            for (int i = 0; i < len; i++)
                s += "0";
            return s; 
        }

    }
}
