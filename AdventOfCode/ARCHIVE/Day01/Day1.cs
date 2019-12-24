using System;
using System.IO;

namespace AdventOfCodeDay1
{
    class Day1
    {
        public static void Day1Main(string inputFile)
        {
            Console.WriteLine("Hello World!");

            string[] massList = File.ReadAllLines(inputFile);
            double totalFuel = 0;

            foreach (string module in massList)
            {
                totalFuel = totalFuel + MassCalculator.calculateMass(Double.Parse(module));
            }

            Console.WriteLine("Fuel required is" + totalFuel);
            Console.ReadLine();
        }
        
    }
}
