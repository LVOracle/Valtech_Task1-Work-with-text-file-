using System;
using System.Collections.Generic;
using System.Linq;

namespace Valtech_Task1_Work_with_text_file_
{
    class Program
    {
        static void Main(string[] args)
        {
            string word = " ";
            WorkWithFile file = new WorkWithFile(@"C:\Users\vladyslav.levchenko\Desktop\Text.txt"); // <--- change file path here
            file.ReadFile();
            file.GenerateDictionaryOfAllWords();
            Console.WriteLine();
            file.Statistic();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Write some string for searching in the text or enter 'xxx' for exit: ");
                word = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (word == "xxx")
                {
                    Console.WriteLine("Bye-bye");
                    break;
                }

                if (word != null)
                {
                    file.SearchWord(word.Trim());
                    //file.WriteToFile(file.Indexes,word);
                }
                file.Indexes = new List<int>(file.Content.Length);
                file.AllWords = new Dictionary<int, List<string>>(file.Content.Length);
            }
        }
    }
}
