using System;
using System.Collections.Generic;
using System.Text;
using AdventOfCode;

namespace AdventOfCodeDay9
{
    class Day9
    {
        public static void Day9Main(string inputFile)
        {
            string[] opCodeList = System.IO.File.ReadAllText(inputFile).Split(",");

            List<decimal> opCode = new List<decimal>();
            List<decimal> input = new List<decimal>();
            decimal currIndexer = 0;
            string currOpCode;
            int opCodeCheck;

            foreach (string tempOpCode in opCodeList)
            {
                opCode.Add(decimal.Parse(tempOpCode));
            }

            //Challenge 1
            //input.Add(1);
            //Challenge 2
            input.Add(2);
            IntCode currIntCode = new IntCode(new List<decimal>(opCode), input);


            while (true)
            {
                currIndexer = currIntCode.CurrIndex;
                currOpCode = currIntCode.OpCode[(int)currIndexer].ToString().PadLeft(5, '0');
                opCodeCheck = Int32.Parse(currOpCode.Substring(3, 2));
                if (opCodeCheck == 99)
                {
                    break;
                }
                else if (opCodeCheck == 3 && currIntCode.Input.Count < 1)
                {
                    //Add output to next IntCode's input list
                    //Console.WriteLine("Input 3 and no valid input. Better keep going or I might die inside.");
                    break;
                }
                else
                {
                    currIntCode = IntCode.OpCodeForward(currIntCode);
                }
            }


            Console.WriteLine("Final Output is " + currIntCode.Output);
            //Console.WriteLine("Maximum Output is " + maxOutput);
            Console.ReadLine();
        }
    }
}
