using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class IntCode
    {
        public List<decimal> OpCode { get; set; }
        public List<decimal> Input { get; set; }
        public decimal CurrIndex { get; set; }
        public decimal Output { get; set; }
        public decimal RefPoint { get; set; }

        public IntCode(List<decimal> opCode, List<decimal> input)
        {
            OpCode = new List<decimal>(opCode);
            Input = new List<decimal>(input);
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
            List<decimal> opCode = new List<decimal>(currIntCode.OpCode);
            List<decimal> input = new List<decimal>(currIntCode.Input);
            decimal currIndex = currIntCode.CurrIndex;
            decimal output = currIntCode.Output;

            string currOpCode = opCode[(int)currIndex].ToString().PadLeft(5, '0');

            //if opcode doesn't exist

            decimal loc1 = opCode[(int)currIndex + 1];
            decimal loc2 = opCode[(int)currIndex + 2];
            decimal var1 = 0;
            decimal var2 = 0;
            decimal setPoint = 0;
            decimal refPoint = currIntCode.RefPoint;
            int actualTest = Int32.Parse(currOpCode.Substring(3, 2));


            //Console.WriteLine("Curr Op Code " + currOpCode);
            //if (currOpCode.Equals("21107"))
            //{
            //    Console.WriteLine("wow, a 21107, how surprising");
            //}

            currIntCode.OpCode = opCode;

            //switch (actualTest)
            //{
            //    case ((int)OpCodeEnum.lessThan):
            //        break;
            //}

                    // If it's an add or multiply, we need to set two params.
                    switch (actualTest)
            {
                case ((int)OpCodeEnum.add):
                case ((int)OpCodeEnum.mult):
                case ((int)OpCodeEnum.jumpIfNon0):
                case ((int)OpCodeEnum.jumpIf0):
                case ((int)OpCodeEnum.lessThan):
                case ((int)OpCodeEnum.greaterThan):
                    setPoint = opCode[(int)currIndex + 3];
                    switch ((int)Char.GetNumericValue(currOpCode[2]))
                    {
                        case 0:
                            // We need to be sure we're pulling info from a valid space...
                            while ((int)loc1 >= opCode.Count)
                            {
                                opCode.Add(0);
                            }
                            var1 = opCode[(int)loc1];
                            break;
                        case 1:
                            var1 = loc1;
                            break;
                        case 2:
                            while ((int)(loc1 + refPoint) >= opCode.Count)
                            {
                                opCode.Add(0);
                            }
                            var1 = opCode[(int)(loc1 + refPoint)];
                            break;
                    }
                    switch ((int)Char.GetNumericValue(currOpCode[1]))
                    {
                        case 0:
                            // We need to be sure we're pulling info from a valid space...
                            while ((int)loc2 >= opCode.Count)
                            {
                                opCode.Add(0);
                            }
                            var2 = opCode[(int)loc2];
                            break;
                        case 1:
                            var2 = loc2;
                            break;
                        case 2:
                            while ((int)(loc2 + refPoint) >= opCode.Count)
                            {
                                opCode.Add(0);
                            }
                            var2 = opCode[(int)(loc2 + refPoint)];
                            break;
                    }
                    switch ((int)Char.GetNumericValue(currOpCode[0]))
                    {
                        case 0:
                            setPoint = opCode[(int)currIndex + 3];
                            break;
                        case 1:
                            throw new System.InvalidOperationException("Position being written to should never be in immediate mode!");
                        case 2:
                            while ((opCode[(int)(currIndex + 3)] + refPoint) >= opCode.Count)
                            {
                                opCode.Add(0);
                            }
                            setPoint = opCode[(int)(currIndex + 3)] + refPoint;
                            break;
                        default:
                            throw new System.InvalidOperationException("WTF even is this");
                    }
                    break;
                case ((int)OpCodeEnum.input):
                case ((int)OpCodeEnum.output):
                case ((int)OpCodeEnum.refUpdate):
                    switch ((int)Char.GetNumericValue(currOpCode[2]))
                    {
                        case 0:
                            // We need to be sure we're pulling info from a valid space...
                            while ((int)loc1 >= opCode.Count)
                            {
                                opCode.Add(0);
                            }
                            var1 = opCode[(int)loc1];
                            break;
                        case 1:
                            var1 = loc1;
                            break;
                        case 2:
                            while ((int)(loc1 + refPoint) >= opCode.Count)
                            {
                                opCode.Add(0);
                            }
                            var1 = opCode[(int)(loc1+ refPoint)];
                            break;
                    }
                    break;
                default:
                    {
                        throw new System.InvalidOperationException("Lol I fucked up");
                    }
            }

            //if (currOpCode[0] != '0')
            //{
            //    throw new System.InvalidOperationException("Position being written to should never be in immediate mode!");
            //}

            // We need to be sure our set point is a valid memory space. If our setpoint isn't... Let's remedy that!!
            while ((int)setPoint >= opCode.Count)
            {
                opCode.Add(0);
            }

            switch (actualTest)
            {
                case 1:
                    opCode[(int)setPoint] = var1 + var2;
                    currIndex += 4;
                    break;
                case 2:
                    opCode[(int)setPoint] = var1 * var2;
                    currIndex += 4;
                    break;
                case 3:
                    switch ((int)Char.GetNumericValue(currOpCode[2]))
                    {
                        case 0:
                            opCode[(int)loc1] = (int)input[0];
                            input.RemoveAt(0);
                            break;
                        case 1:
                            Console.WriteLine("I think this should be impossible...");
                            opCode[(int)input[0]] = (int)input[0];
                            break;
                        case 2:
                            opCode[(int)(loc1 + refPoint)] = (int)input[0];
                            input.RemoveAt(0);
                            break;
                        default:
                            break;
                    }
                    currIndex += 2;
                    break;
                case 4:
                    //output.Add(var1);
                    output = var1;
                    Console.WriteLine("Output hit. Output is: " + var1);
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
                        opCode[(int)setPoint] = 1;
                    }
                    else
                    {
                        opCode[(int)setPoint] = 0;
                    }
                    currIndex += 4;
                    break;
                case 8:
                    if (var1 == var2)
                    {
                        opCode[(int)setPoint] = 1;
                    }
                    else
                    {
                        opCode[(int)setPoint] = 0;
                    }
                    currIndex += 4;
                    break;
                case 9:
                    currIntCode.RefPoint = currIntCode.RefPoint + var1;
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
