using AdventOfCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCodeDay7
{
    class Day7
    {
        public static void Day7Main(string inputFile)
        {
            string[] opCodeList = System.IO.File.ReadAllText(inputFile).Split(",");
            string currOpCode = "";
            decimal currIndexer = 0;
            int opCodeCheck = 0;
            
            List<decimal> input_list = new List<decimal>();
            List<decimal> opCode = new List<decimal>();

            List<List<char>> phaseListInput = new List<List<char>>();
            string str = "56789";
            char[] arr = str.ToCharArray();
            phaseListInput = GetPer(phaseListInput, arr);

            foreach (string tempOpCode in opCodeList)
            {
                opCode.Add(int.Parse(tempOpCode));
            }
            
            float maxOutput = 0;
            List<decimal> input = new List<decimal>();
            List<char> maxPhaseInput = new List<char>();

            //List<char> currPhaseList = new List<char> { '4', '3', '2', '1', '0' };
            //List<char> currPhaseList = new List<char> { '0', '1', '2', '3', '4' };
            //List<char> currPhaseList = new List<char> { '1', '0', '4', '3', '2' };
            //List<char> currPhaseList = new List<char> { '9', '8', '7', '6', '5' };
            //List<char> currPhaseList = new List<char> { '9', '7', '8', '5', '6' };

            List<IntCode> ampList = new List<IntCode>();

            foreach (List<char> currPhaseList in phaseListInput)
            {
                // Initialise IntCode setup
                ampList = new List<IntCode>();

                // initialise each input with its phase setting. If it's the first input, tack a 0 on (very first input = 0!)
                foreach (char currPhase in currPhaseList)
                {
                    input = new List<decimal>();
                    input.Add((int)Char.GetNumericValue(currPhase));
                    if (currPhase.Equals(currPhaseList[0]))
                    {
                        input.Add(0);
                    }
                    //Make our ampList of 5x amplifiers.
                    ampList.Add(new IntCode(new List<decimal>(opCode), input));
                }
                while (true)
                {
                    for (int i = 0 ; i < ampList.Count ; i++ ) 
                    {
                        IntCode currIntCode = ampList[i];
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
                        //we have escaped this intcode, save it!
                        ampList[i] = currIntCode;
                        if (i < (ampList.Count-1))
                        {
                            // If it's not our last input, make the output part of our next input :)
                            ampList[i + 1].Input.Add(currIntCode.Output);
                        }
                    }

                    // Will only get here on final amplifier
                    if (opCodeCheck == 99)
                    {
                        // If we're done, let's check if our final output is the best ever
                        if ((int)ampList.Last().Output > maxOutput)
                        {
                            maxOutput = (int)ampList.Last().Output;
                            maxPhaseInput = new List<char>(currPhaseList);
                        }
                        break;
                    }
                    else
                    {
                        ampList[0].Input.Add(ampList.Last().Output);
                    }
                }

            }

            Console.WriteLine("Final Input List is " + string.Join(",", maxPhaseInput));
            Console.WriteLine("Maximum Output is " + maxOutput);
            Console.ReadLine();
        }

        public enum OpCode
        {
            add = 1,
            mult = 2,
            input = 3,
            output = 4,
            jumpIfNon0 = 5,
            jumpIf0 = 6,
            lessThan = 7,
            greaterThan = 8
        }

        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            var temp = a;
            a = b;
            b = temp;
        }

        public static List<List<char>> GetPer(List<List<char>> returnList, char[] list)
        {
            int x = list.Length - 1;
            returnList = GetPer(returnList,list, 0, x);
            return returnList;
        }

        private static List<List<char>> GetPer(List<List<char>> returnList, char[] list, int k, int m)
        {
            if (k == m)
            {
                returnList.Add(list.ToList());
                return returnList;
            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    returnList = GetPer(returnList, list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
            return returnList;
        }

    }
}
