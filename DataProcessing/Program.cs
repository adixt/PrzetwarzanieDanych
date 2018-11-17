using System;
using System.Collections.Generic;

namespace DataProcessing
{
    class Program
    {
        private static readonly Dictionary<string, string> FileNames = new Dictionary<string, string>{
            { "Oct", "2016.10.csv" },
            { "Dec", "2016.12.csv" },
            { "Jan", "2017.01.csv" },
        };

        static void Main(string[] args)
        {
            AutoMapperConfig.Initialize();

            Console.WriteLine("Hello World!");
            var processor = new CSVProcessor();
            processor.DifferenceOfTwoFiles(FileNames["Oct"], FileNames["Dec"]);
            processor.DifferenceOfTwoFiles(FileNames["Dec"], FileNames["Jan"]);
            Console.WriteLine("########## PRESS ANY KEY TO EXIT ###########");
            Console.ReadKey();
        }
    }
}
