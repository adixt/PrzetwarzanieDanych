using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataProcessing
{
    public class CSVProcessor
    {
        private readonly DirectoryInfo inputFolder = new DirectoryInfo(@"InputData\");
        private readonly DirectoryInfo outputFolder = new DirectoryInfo(@"..\..\..\OutputData");
        private readonly Configuration csvConfiguration;

        public CSVProcessor()
        {
            csvConfiguration = new Configuration()
            {
                MissingFieldFound = (headerNames, index, context) => Console.WriteLine($"Field with names ['{string.Join("', '", headerNames)}'] at index '{index}' was not found. "),
                ReadingExceptionOccurred = exception => Console.WriteLine($"Reading exception: {exception.Message}"),
                HeaderValidated = (isValid, headerNames, headerNameIndex, context) =>
                {
                    if (!isValid)
                    {
                        Console.WriteLine($"Header matching ['{string.Join("', '", headerNames)}'] names at index {headerNameIndex} was not found.");
                    }
                },
                CultureInfo = CultureInfo.InvariantCulture,
                HasHeaderRecord = true
            };
        }

        private List<T> ReadCSVFromFile<T>(string filePath)
        {
            List<T> records;
            using (var reader = File.OpenText(filePath))
            {
                var csvReader = new CsvReader(reader, csvConfiguration);
                records = csvReader.GetRecords<T>().ToList();
            }
            return records;
        }

        private void WriteCVSToFile<T>(IEnumerable<T> fileToWrite, string fileName)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(outputFolder.FullName, fileName)))
            {
                var csvWriter = new CsvWriter(writer, csvConfiguration);
                csvWriter.WriteRecords(fileToWrite);
            }
        }

        public void DifferenceOfTwoFiles(string fileNameOne, string fileNameTwo)
        {
            var fileOneRecordsAll = ReadCSVFromFile<InputClass>(Path.Combine(inputFolder.FullName, fileNameOne));
            var fileOneRecords = fileOneRecordsAll.Distinct(new InputClassEqualityComparer()).ToList();

            var fileTwoRecordsAll = ReadCSVFromFile<InputClass>(Path.Combine(inputFolder.FullName, fileNameTwo));
            var fileTwoRecords = fileTwoRecordsAll.Distinct(new InputClassEqualityComparer()).ToList();

            var fileOneMapped = Mapper.Map<List<OutputClass>>(fileOneRecords);
            var fileTwoMapped = Mapper.Map<List<OutputClass>>(fileTwoRecords);

            var commonRecords = fileTwoMapped
                .Join(
                    fileOneMapped,
                    outKey => outKey,
                    innerKey => innerKey,
                    (input, inner) =>
                    {
                        return new OutputClass()
                        {
                            Name = input.Name,
                            Platform = input.Platform,
                            Year_of_Release = input.Year_of_Release,
                            Genre = input.Genre,
                            NA_Sales = Math.Abs(input.NA_Sales - inner.NA_Sales),
                            EU_Sales = Math.Abs(input.EU_Sales - inner.EU_Sales),
                            JP_Sales = Math.Abs(input.JP_Sales - inner.JP_Sales),
                            Other_Sales = Math.Abs(input.Other_Sales - inner.Other_Sales),
                            Global_Sales = Math.Abs(input.Global_Sales - inner.Global_Sales),
                            Publisher = input.Publisher,
                            Critic_Score = input.Critic_Score,
                            Critic_Count = input.Critic_Count,
                            User_Score = input.User_Score,
                            User_Count = input.User_Count,
                            Rating = input.Rating ?? inner.Rating,
                        };
                    },
                    new OutputClassEqualityComparer()
                )
                .OrderBy(x=>x.Name).ToList();

            Console.WriteLine($"Comparisson of {fileNameOne} and {fileNameTwo}:");
            Console.WriteLine($"Common games found:\t{commonRecords.Count}");
            Console.WriteLine($"List of 10 first games:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"\t- {commonRecords[i].Name}");
            }

            string outputFileName = $"{Path.GetFileNameWithoutExtension(fileNameOne)}-{fileNameTwo}";

            WriteCVSToFile(commonRecords, outputFileName);
        }

    }
}
