using System;
using System.Collections.Generic;

namespace Valtech_Task1_Work_with_text_file_
{
    class Program
    {
        static void Main(string[] args)
        {
            var word = string.Empty;
            WorkWithFile file = new WorkWithFile();
            while(file.IsCorrectPath == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter path to the file: ");
                file.Path = Console.ReadLine();
                file.ReadFile();
            }
            Console.ForegroundColor = ConsoleColor.White;
            file.GenerateDictionaryOfAllWords();
            Console.WriteLine();
            file.Statistic();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Write some word for searching in the text or enter 'xxx' for exit: ");
                word = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (word == "xxx")
                {
                    Console.WriteLine("Bye-bye");
                    break;
                }
                if (word != null)
                {
                    file.SearchWords(word.Trim());
                }
                file.Indexes = new List<int>(file.Content.Length);
                file.AllWords = new Dictionary<int, List<string>>(file.Content.Length);
            }
        }
    }
}
