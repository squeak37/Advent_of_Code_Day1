﻿using System;
using AdventOfCodeDay1;
using AdventOfCodeDay2;
using AdventOfCodeDay3;
using AdventOfCodeDay4;
using AdventOfCodeDay5;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {

            string input_file = @"C:\Users\Eoghan\source\repos\AdventOfCode\AdventOfCode\Inputs";

            input_file = input_file + @"\Day5Input.txt";
            //input_file = input_file + @"\Day3InputDEBUG2.txt";

            //AdventOfCodeDay1.Day1.Day1Main(input_file);
            //AdventOfCodeDay2.Day2.Day2Main(input_file);
            //AdventOfCodeDay3.Day3.Day3Main(input_file);
            //AdventOfCodeDay3.Day3.Day3Secondary(input_file);
            //AdventOfCodeDay4.Day4.Day4Main(input_file);
            AdventOfCodeDay5.Day5.Day5Main(input_file);
        }
    }
}