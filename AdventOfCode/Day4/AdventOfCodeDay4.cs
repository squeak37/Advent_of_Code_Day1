using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeDay4
{
    class Day4
    {
        public static void Day4Main(string inputFile)
        {
            string[] input = File.ReadAllLines(inputFile);
            string[] range = input[0].Split(",");
            int lowerLimit = Int32.Parse(range[0]);
            int upperLimit = Int32.Parse(range[1]);
            List<int> validPassword = new List<int>();
            string currPassword;
            bool repeatCheck = false;
            bool incrementCheck = false;
            int noDoubles;

            for (int i = lowerLimit; i<upperLimit; i++)
            {
                repeatCheck = false;
                incrementCheck = true;
                noDoubles = 0;
                currPassword = i.ToString();

                for (int j = 0; j < currPassword.Length-1; j++)
                {
                    // Rule check 1, are there in doubles? Will 
                    if (currPassword[j] == currPassword[j + 1])
                    {
                        //repeatCheck = true;
                        if (currPassword.ToCharArray().Count(c => c == currPassword[j]) == 2)
                        {
                            repeatCheck = true;
                        }
                        noDoubles++;
                    }
                    // Rule check 2, do all numbers increase?
                    if (currPassword[j] > currPassword[j + 1])
                    {
                        incrementCheck = false;
                    }
                }
                
                if (noDoubles > 1)
                {

                }

                if (repeatCheck && incrementCheck)
                {
                    validPassword.Add(i);
                }
            }
            Console.WriteLine("Number of valid passwords are: " + validPassword.Count);
            Console.ReadLine();
        }
    }
}
