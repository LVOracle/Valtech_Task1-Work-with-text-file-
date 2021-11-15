using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;

namespace Valtech_Task1_Work_with_text_file_
{
    public class WorkWithFile
    {
        public string Path { get; set; }
        public string Content { get; set; }
        public string CopyContent { get; set; }
        public List<int> Indexes { get; set; }
        public Dictionary<int, List<string>> AllWords { get; set; }
        public bool IsCorrectPath { get; set; }
        public WorkWithFile()
        {
            AllWords = new Dictionary<int, List<string>>(1000);
            IsCorrectPath = false;
        }
        public void ReadFile()
        {
            try
            {
                using StreamReader sr = new StreamReader(Path);
                Content = sr.ReadToEnd();
                IsCorrectPath = true;
                Indexes = new List<int>(Content.Length);
                CopyContent = Content;
                CopyContent = CopyContent.ToLower();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                IsCorrectPath = false;
            }
        }
        /// <summary>
        /// Find the word in the text and remember start position of each word
        /// </summary>
        /// <param name="word"></param>
        public void SearchWords(string word)
            {
                StringBuilder tmp = new StringBuilder(25);
                word = word.ToLower();
                for (int i = 0; i < CopyContent.Length; ++i)
                {
                    if ((Char.IsControl(CopyContent[i]) || Char.IsPunctuation(CopyContent[i]) || Char.IsWhiteSpace(CopyContent[i]) || Char.IsDigit(CopyContent[i])))
                    {
                        if (tmp.Equals(word))
                        { 
                            int start = i - tmp.Length;
                            Indexes.Add(start);
                        }
                        tmp.Clear();
                    }
                    else
                    {
                        tmp.Append(CopyContent[i]);
                    }
                }

                if (Indexes.Count != 0)
                {
                    Show(word.Length);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine();
                    Console.Write($"All indexes of \'{word}\' ({Indexes.Count}) word: ");
                    foreach (var ind in Indexes)
                        Console.Write($"{ind} ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"In this text file word \'{word}\' is not found!");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
               
                Console.WriteLine();
        }
        /// <summary>
        /// Generate a dictionary of all words with printing all statistics about each word in the text file with word's position and line
        /// </summary>
        public void GenerateDictionaryOfAllWords()
        {
            var stringLine = 1;
            var position = 1;
            StringBuilder tmp = new StringBuilder(25);
            for (int i = 0; i < CopyContent?.Length; ++i)
            {
                if (Char.IsControl(CopyContent[i]) || Char.IsPunctuation(CopyContent[i]) || Char.IsWhiteSpace(CopyContent[i]) || Char.IsDigit(CopyContent[i]))
                {
                    if (CopyContent[i].Equals('\n'))
                    {
                        ++stringLine;
                        position = 1;
                    }
                    var startIndex = i - tmp.Length;
                    if(tmp.ToString() != "")
                        AllWords.Add(startIndex,new List<string>(){tmp.ToString(),stringLine.ToString(),(position - tmp.Length).ToString()});
                    tmp.Clear();
                }
                else
                {
                    tmp.Append(CopyContent[i]);
                }
                ++position;
            }
            PrintFullDictionaryWithStatistic();
        }
        public void Statistic()
        {
            var statistics = AllWords.GroupBy(x => x.Value.ElementAt(0));
            var enumerable = statistics as IGrouping<string, KeyValuePair<int, List<string>>>[] ?? statistics.ToArray();
            foreach (var item in enumerable.OrderByDescending(x => x.Count()))
            {
                double percent = Math.Round(Convert.ToDouble(item.Count()) * 100.0 / Convert.ToDouble(statistics.Count()), 2);
                Console.WriteLine($"Amount: {item.Count()}\t Percent: {percent}%\t Word: \'{item.Key}\'");
            }
            Console.WriteLine();
        }
        private void PrintFullDictionaryWithStatistic()
        {
            foreach (var dic in AllWords)
            {
                Console.WriteLine($"Position: {dic.Value[2]}\t Index: {dic.Key}\t Line: {dic.Value[1]}\t Word: \'{dic.Value[0]}\'");
            }
        }
        private void Show(int size)
        {
            var index = 0;
            for (int i = 0; i < Content.Length;)
            {
                if (i == Indexes[index])
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    for (int j = i; j < Indexes[index] + size; ++j)
                    {
                        Console.Write(Content[i]);
                        ++i;
                    }
                    if (index < Indexes.Count - 1)
                    {
                        ++index;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(Content[i]);
                    ++i;
                }
            }
        }
    }
}
