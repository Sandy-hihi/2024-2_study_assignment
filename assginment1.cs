using System;

namespace calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter an expression (ex. 2 + 3): ");
            string input = Console.ReadLine();

            try
            {
                Parser parser = new Parser();
                (double num1, string op, double num2) = parser.Parse(input);

                Calculator calculator = new Calculator();
                double result = calculator.Calculate(num1, op, num2);

                Console.WriteLine($"Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    // Parser class to parse the input
    public class Parser
    {
        public (double, string, double) Parse(string input)
        {
            string[] parts = input.Split(' ');

            if (parts.Length != 3)
            {
                throw new FormatException("Input must be in the format: number operator number");
            }

            double num1 = Convert.ToDouble(parts[0]);
            string op = parts[1];
            double num2 = Convert.ToDouble(parts[2]);

            return (num1, op, num2);
        }
    }

    // Calculator class to perform operations
    public class Calculator
    {
        // ---------- TODO ----------
        public double Calculate(double num1, string op, double num2)
        {
            switch(op)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "*":
                    return num1 * num2;
                case "/":
                    if (num2 == 0) throw new DivideByZeroException("Division by zero is not allowed");
                    return num1 / num2;
                case "**":
                    return Math.Pow(num1, num2);
                case "%":
                    return num1 % num2;
                case "G":
                    return common_divisor(num1 , num2);
                case "L":
                    return (num1 * num2) / common_divisor(num1,num2);
                default:
                throw new InvalidOperationException("Invalid operator");
            }
        }
        
        public double common_divisor(double a, double b) 
        {
            if (a % b==0) return b;
            else return common_divisor(b , a % b);
        }
        // --------------------
    }
}