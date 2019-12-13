using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCodeDay7
{
    class Day7
    {
        public static void Day7Main(string inputFile)
        {
            string[] opCodeList = System.IO.File.ReadAllText(inputFile).Split(",");
            string currOpCode = "";
            int currIndexer = 0;
            int opCodeCheck = 0;
            int input = 0;

            List<int> opCodeOrig = new List<int>();
            List<int> outputs = new List<int>();
            List<int> input_list = new List<int>();
            List<int> opCode = new List<int>();

            foreach (string tempOpCode in opCodeList)
            {
                opCodeOrig.Add(int.Parse(tempOpCode));
            }
            List<Tuple<int, int, int>> output2 = new List<Tuple<int, int, int>>();

            opCode = new List<int>(opCodeOrig);
            currIndexer = 0;
            //input = j;
            //while (true)
            //{
            //    currOpCode = opCode[currIndexer].ToString().PadLeft(5, '0');
            //    opCodeCheck = Int32.Parse(currOpCode.Substring(3, 2));
            //    //Console.WriteLine(currIndexer);
            //    //Console.WriteLine("Current Op Code is " + currOpCode);
            //    if (opCodeCheck == 99)
            //    {
            //        output2.Add(Tuple.Create(i, j, outputs[i * 5 + j]));
            //        break;
            //    }
            //    else
            //    {
            //        (opCode, input, outputs, currIndexer) = OpCodeForward(opCode, currIndexer, input, outputs, currIndexer);
            //    }
            //}

            List<int> phaseList = new List<int>{1, 2, 3};
            // 1 2 3
            // 1 3 2
            // 2 1 3
            // 2 3 1
            // 3 1 2
            // 3 2 1

            //1 2 3 4
            //1 2 4 3
            //1 3 2 4
            //1 3 4 2
            //1 4 2 3
            //1 4 3 2
            



            Console.WriteLine("Final Input List is " + string.Join(",", input_list));
            Console.WriteLine("Final Diagnostic Code is " + string.Join(",", outputs));
            Console.WriteLine("Final Op Code is " + string.Join(",", opCode));
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
        
        static List<int> GenerateListOfInputs(List<int> phaseList)
        {
            int listSize = Factorial(phaseList.Count);
            
            for (int i = 0; i < listSize; i++)
            {

            }

            return phaseList;
        }

        static public int Factorial(int number)
        {
            if (number == 1)
                return 1;
            else
                return number * Factorial(number - 1);
        }

        static public Tuple<List<int>, int, List<int>, int> OpCodeForward(List<int> currList, int startPoint, int input, List<int> outputs, int currIndexer)
        {
            string currOpCode = currList[startPoint].ToString().PadLeft(5, '0');
            //3,50
            int loc1 = currList[startPoint + 1];
            int loc2 = currList[startPoint + 2];
            int var1 = 0;
            int var2 = 0;
            int setPoint = currList[startPoint + 3];
            int actualTest = Int32.Parse(currOpCode.Substring(3, 2));
            


            // If it's an add or multiply, we need to set two params.
            switch (actualTest)
            {
                case ((int)OpCode.add):
                case ((int)OpCode.mult):
                case ((int)OpCode.jumpIfNon0):
                case ((int)OpCode.jumpIf0):
                case ((int)OpCode.lessThan):
                case ((int)OpCode.greaterThan):
                    switch ((int)Char.GetNumericValue(currOpCode[2]))
                    {
                        case 0:
                            var1 = currList[loc1];
                            break;
                        case 1:
                            var1 = loc1;
                            break;
                    }
                    switch ((int)Char.GetNumericValue(currOpCode[1]))
                    {
                        case 0:
                            var2 = currList[loc2];
                            break;
                        case 1:
                            var2 = loc2;
                            break;
                    }
                    break;
                case ((int)OpCode.input):
                case ((int)OpCode.output):
                    switch ((int)Char.GetNumericValue(currOpCode[2]))
                    {
                        case 0:
                            var1 = currList[loc1];
                            break;
                        case 1:
                            var1 = loc1;
                            break;
                    }
                    break;
                default:
                    {
                        throw new System.InvalidOperationException("Lol I fucked up");
                    }
            }

            if (currOpCode[0] != '0')
            {
                throw new System.InvalidOperationException("Position being written to should never be in immediate mode!");
            }
            switch (actualTest)
            {
                case 1:
                    currList[setPoint] = var1 + var2;
                    currIndexer += 4;
                    break;
                case 2:
                    currList[setPoint] = var1 * var2;
                    currIndexer += 4;
                    break;
                case 3:
                    switch ((int)Char.GetNumericValue(currOpCode[2]))
                    {
                        case 0:
                            currList[loc1] = input;
                            break;
                        case 1:
                            Console.WriteLine("I think this should be impossible...");
                            currList[input] = input;
                            break;
                        default:
                            break;
                    }
                    currIndexer += 2;
                    break;
                case 4:
                    outputs.Add(var1);
                    Console.WriteLine("Output hit. Output is: " + var1);
                    currIndexer += 2;
                    break;
                case 5:
                    if (var1 != 0)
                    {
                        currIndexer = var2;
                    }
                    else
                    {
                        currIndexer += 3;
                    }
                    break;
                case 6:
                    if (var1 == 0)
                    {
                        currIndexer = var2;
                    }
                    else
                    {
                        currIndexer += 3;
                    }
                    break;
                case 7:
                    if (var1 < var2)
                    {
                        currList[setPoint] = 1;
                    }
                    else
                    {
                        currList[setPoint] = 0;
                    }
                    currIndexer += 4;
                    break;
                case 8:
                    if (var1 == var2)
                    {
                        currList[setPoint] = 1;
                    }
                    else
                    {
                        currList[setPoint] = 0;
                    }
                    currIndexer += 4;
                    break;
                default:
                    throw new System.InvalidOperationException("Only Valid initial number for Op Code's are 1, 2, 3, 4, and 99");
            }
            // angels, the chain
            return Tuple.Create(currList, input, outputs, currIndexer);
        }
    }
}
