using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebLab1
{
    public static class Calculations
    {
        public static string Factorial(int number)
        {
            if (number < 0 || number > 20)
            {
                return "Please enter a number between 0 and 20 for factorial.";
            }

            long result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }
            return result + "";
        }

        public static string Fibonacci(int number)
        {
            if (number < 1 || number > 100)
            {
                return "Please enter a number between 1 and 100 for Fibonacci.";
            }

            long a = 0, b = 1, temp;
            for (int i = 2; i <= number; i++)
            {
                temp = a + b;
                a = b;
                b = temp;
            }
            return b + "";
        }
    }
}
