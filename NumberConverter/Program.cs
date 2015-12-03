using System;
using System.Collections.Generic;
using System.Linq;
using Converter;
using System.IO;

namespace NumberConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            // constant string for input error
            const string inputError = "The value entered was not an int.";

            // container to hold conversion keys
            Dictionary<int, string> conversions = new Dictionary<int, string>();

            // defaultly we are going to run with the two keys
            if (args.Count() == 0)
            {
                conversions.Add(3, "Fizz");
                conversions.Add(5, "Buzz");
            }
            // going to read in the file listing all the user supplied conversion key/value pairs
            else
            {
                try
                {
                    string filePath = Path.Combine(Environment.CurrentDirectory, "AppData", args[0]);

                    StreamReader file = new StreamReader(filePath);

                    while (!file.EndOfStream)
                    {
                        string conversionPair = file.ReadLine();

                        int occurance = conversionPair.IndexOf(':');

                        if (occurance < 0)
                        {
                            throw new ArgumentException("File not in the agreed upon syntax.");
                        }

                        int key = Int32.Parse(conversionPair.Substring(0, occurance));
                        string value = conversionPair.Substring(occurance + 1);

                        conversions.Add(key, value);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            int lowerBound = ObtainLowerBound(inputError);

            int upperBound = ObtainUpperBound(lowerBound, inputError);

            foreach (string result in IntegerConverter.convert(lowerBound, upperBound, conversions))
            {
                Console.WriteLine(result);
            }

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Please press the enter key to exit.");
            Console.ReadLine();
        }

        static int ObtainLowerBound(string errorMessage)
        {
            string lowerInput = "Please insert an lower bound integer value";

            Console.WriteLine(lowerInput);

            int bound = 0;

            while (true)
            {
                try
                {
                    bound = Int32.Parse(Console.ReadLine());
                    return bound;
                }
                catch (Exception e)
                {
                    Console.WriteLine(errorMessage + " " + lowerInput);
                }
            }
        }

        static int ObtainUpperBound(int lowerBound, string errorMessage)
        {
            string upperInput = "Please insert an upper bound integer value";
            string lowerBoundGreaterThanUpperBoundErrorMessage = "Upper bound must be greater than or equal to: " + lowerBound.ToString();

            Console.WriteLine(upperInput);

            int bound = 0;

            while (true)
            {
                try
                {
                    bound = Int32.Parse(Console.ReadLine());

                    if (bound >= lowerBound)
                    {
                        return bound;
                    }
                    else
                    {
                        Console.WriteLine(lowerBoundGreaterThanUpperBoundErrorMessage + " " + upperInput);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(errorMessage + " " + upperInput);
                }
            }
        }
    }
}
