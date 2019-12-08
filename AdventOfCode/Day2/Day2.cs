using System;
using System.Collections.Generic;

namespace AdventOfCodeDay2
{
    class Day2
    {
        public static void Day2Main(string inputFile)
        {
            string[] opCodeList = System.IO.File.ReadAllText(inputFile).Split(",");
            int currIndexer = 0;

            List <int> opCode = new List<int>();

            foreach (string currOpCode in opCodeList)
            {
                opCode.Add(int.Parse(currOpCode));
            }
            
            List<int> testOpCode = new List<int>(opCode);
            int noun = 0;
            int verb = 0;
            int maxnumber = 100;
            int result = 19690720;
            bool keepTesting = true;

            //List<int> hackyOpCode = new List<int>
            //{
            //    1,
            //    3,
            //    3,
            //    0,
            //    1,
            //    0,
            //    0,
            //    0,
            //    99
            //};

            //testOpCode = hackyOpCode;
            //noun = 0;
            //verb = 0;
            //maxnumber = 5;
            //result = 4;
            //opCode = testOpCode;

            while (noun < maxnumber && keepTesting)
            {
                testOpCode[1] = noun;
                while (verb < maxnumber && keepTesting)
                {
                    testOpCode[1] = noun;
                    testOpCode[2] = verb;
                    currIndexer = 0;
                    while (true)
                    {
                        if (testOpCode[currIndexer] == 99)
                        {
                            break;
                        }
                        else
                        {
                            testOpCode = OpCodeCalculation.OpCodeForward(testOpCode, currIndexer);
                            currIndexer += 4;
                        }
                    }
                    if (testOpCode[0] == result)
                    {
                        keepTesting = false;
                    }
                    else
                    {
                        verb ++;
                        testOpCode = new List<int>(opCode);
                    }

                }
                if (testOpCode[0] == result)
                {
                    keepTesting = false;
                }
                else
                {
                    Console.WriteLine("Noun Failed: " + noun);
                    noun++;
                    verb = 0;
                    testOpCode = new List<int>(opCode);
                }
            }
            

            Console.WriteLine("Final noun is: " + noun);
            Console.WriteLine("Final verb is: " + verb);
            Console.WriteLine("100 * noun + verb: " + ((100*noun) + verb));
            Console.ReadLine();
        }

        static void Challenge1()
        {
            string inputFile = @"C:\Users\Eoghan\source\repos\AdventOfCodeDay2\AdventOfCodeDay2\Day2Input.txt";
            string[] opCodeList = System.IO.File.ReadAllText(inputFile).Split(",");
            int currIndexer = 0;

            List<int> opCode = new List<int>();

            foreach (string currOpCode in opCodeList)
            {
                opCode.Add(int.Parse(currOpCode));
            }

            // Temporary hack clearly... Need to comment.
            opCode[1] = 12;
            opCode[2] = 2;

            while (true)
            {
                if (opCode[currIndexer] == 99)
                {
                    break;
                }
                else
                {
                    opCode = OpCodeCalculation.OpCodeForward(opCode, currIndexer);
                    currIndexer += 4;
                }
            }

            Console.WriteLine("Final Op Code is " + string.Join(",", opCode));
            Console.ReadLine();
        }
    }
}
