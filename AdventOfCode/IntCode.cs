using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class IntCode
    {
        public List<int> OpCode { get; set; }
        public List<int> Input { get; set; }
        public int CurrIndex { get; set; }
        public int Output { get; set; }
        public int RefPoint { get; set; }

        public IntCode(List<int> opCode, List<int> input)
        {
            OpCode = new List<int>(opCode);
            Input = new List<int>(input);
            CurrIndex = 0;
            Output = 0;
            RefPoint = 0;
        }

        public enum OpCodeEnum
        {
            add = 1,
            mult = 2,
            input = 3,
            output = 4,
            jumpIfNon0 = 5,
            jumpIf0 = 6,
            lessThan = 7,
            greaterThan = 8,
            refUpdate = 9
        }

        static public IntCode OpCodeForward(IntCode currIntCode)
        {
            // Initialise Key params
            List<int> opCode = new List<int>(currIntCode.OpCode);
            List<int> input = new List<int>(currIntCode.Input);
            int currIndex = currIntCode.CurrIndex;
            int output = currIntCode.Output;

            string currOpCode = opCode[currIndex].ToString().PadLeft(5, '0');

            //if opcode doesn't exist

            int loc1 = opCode[currIndex + 1];
            int loc2 = opCode[currIndex + 2];
            int var1 = 0;
            int var2 = 0;
            int setPoint = 0;
            int refPoint = currIntCode.RefPoint;
            int actualTest = Int32.Parse(currOpCode.Substring(3, 2));

            while (loc1 > opCode.Count + 1)
            {
                opCode.Add(0);
            }
            while (loc2 > opCode.Count + 1)
            {
                opCode.Add(0);
            }
            currIntCode.OpCode = opCode;

                // If it's an add or multiply, we need to set two params.
                switch (actualTest)
            {
                case ((int)OpCodeEnum.add):
                case ((int)OpCodeEnum.mult):
                case ((int)OpCodeEnum.jumpIfNon0):
                case ((int)OpCodeEnum.jumpIf0):
                case ((int)OpCodeEnum.lessThan):
                case ((int)OpCodeEnum.greaterThan):
                    setPoint = opCode[currIndex + 3];
                    switch ((int)Char.GetNumericValue(currOpCode[2]))
                    {
                        case 0:
                            var1 = opCode[loc1];
                            break;
                        case 1:
                            var1 = loc1;
                            break;
                        case 2:
                            var1 = loc1 + refPoint;
                            break;
                    }
                    switch ((int)Char.GetNumericValue(currOpCode[1]))
                    {
                        case 0:
                            var2 = opCode[loc2];
                            break;
                        case 1:
                            var2 = loc2;
                            break;
                        case 2:
                            var2 = loc2 + refPoint;
                            break;
                    }
                    break;
                case ((int)OpCodeEnum.input):
                case ((int)OpCodeEnum.output):
                case ((int)OpCodeEnum.refUpdate):
                    switch ((int)Char.GetNumericValue(currOpCode[2]))
                    {
                        case 0:
                            var1 = opCode[loc1];
                            break;
                        case 1:
                            var1 = loc1;
                            break;
                        case 2:
                            var1 = loc1+ refPoint;
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
                    opCode[setPoint] = var1 + var2;
                    currIndex += 4;
                    break;
                case 2:
                    opCode[setPoint] = var1 * var2;
                    currIndex += 4;
                    break;
                case 3:
                    switch ((int)Char.GetNumericValue(currOpCode[2]))
                    {
                        case 0:
                            opCode[loc1] = input[0];
                            input.RemoveAt(0);
                            break;
                        case 1:
                            Console.WriteLine("I think this should be impossible...");
                            opCode[input[0]] = input[0];
                            break;
                        default:
                            break;
                    }
                    currIndex += 2;
                    break;
                case 4:
                    //output.Add(var1);
                    output = var1;
                    //Console.WriteLine("Output hit. Output is: " + var1);
                    currIndex += 2;
                    break;
                case 5:
                    if (var1 != 0)
                    {
                        currIndex = var2;
                    }
                    else
                    {
                        currIndex += 3;
                    }
                    break;
                case 6:
                    if (var1 == 0)
                    {
                        currIndex = var2;
                    }
                    else
                    {
                        currIndex += 3;
                    }
                    break;
                case 7:
                    if (var1 < var2)
                    {
                        opCode[setPoint] = 1;
                    }
                    else
                    {
                        opCode[setPoint] = 0;
                    }
                    currIndex += 4;
                    break;
                case 8:
                    if (var1 == var2)
                    {
                        opCode[setPoint] = 1;
                    }
                    else
                    {
                        opCode[setPoint] = 0;
                    }
                    currIndex += 4;
                    break;
                case 9:
                    currIntCode.RefPoint = var1;
                    currIndex += 2;
                    break;
                default:
                    throw new System.InvalidOperationException("Only Valid initial number for Op Code's are 1-9 and 99");
            }

            currIntCode.CurrIndex = currIndex;
            currIntCode.Input = input;
            currIntCode.OpCode = opCode;
            currIntCode.Output = output;
            return currIntCode;
        }



    }
}
