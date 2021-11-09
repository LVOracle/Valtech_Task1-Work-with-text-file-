using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Valtech_Task1_Work_with_text_file_
{
    public class WorkWithFile
    {
        /// <summary>
        /// Path to the file
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// General content of the file
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Copy of the general content
        /// </summary>
        public string CopyContent { get; set; }
        /// <summary>
        /// List of the start indexes of each searching words
        /// </summary>
        public List<int> Indexes { get; set; }
        /// <summary>
        /// Dictionary for all words in the text
        /// </summary>
        public Dictionary<int,List<string>> AllWords { get; set; }
        /// <summary>
        /// Initialize path
        /// </summary>
        /// <param name="path"></param>
        public WorkWithFile(string path)
        {
            Path = path ?? throw new ArgumentNullException("Path can't be null.");
            AllWords = new Dictionary<int, List<string>>(1000);
        }
        /// <summary>
        /// Reading from file and initialize content
        /// </summary>
        public void ReadFile()
        {
            try
            {
                using StreamReader sr = new StreamReader(Path);
                Content = sr.ReadToEnd();
                Indexes = new List<int>(Content.Length);
                CopyContent = Content;
                CopyContent = CopyContent.ToLower();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Find the word in the text and remember start position of each word
        /// </summary>
        /// <param name="word"></param>
        public void SearchWord(string word)
            {
            StringBuilder tmp = new StringBuilder(25);
            word = word.ToLower();
            for (int i = 0; i < CopyContent.Length; ++i)
            {
                if (CopyContent[i].Equals(' ') ||
                    CopyContent[i].Equals('\n') ||
                    CopyContent[i].Equals('.') ||
                    CopyContent[i].Equals(',') ||
                    CopyContent[i].Equals('!')||
                    CopyContent[i].Equals('?')||
                    CopyContent[i].Equals('(')||
                    CopyContent[i].Equals(')')||
                    CopyContent[i].Equals('[')||
                    CopyContent[i].Equals(']')||
                    CopyContent[i].Equals('\"')||
                    CopyContent[i].Equals('\''))
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
        /// Display all words with another color if this word we are searching
        /// </summary>
        /// <param name="size"></param>
        private void Show(int size)
        {
            int index = 0;
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
        /// <summary>
        /// Generate a dictionary of all words with printing all statistics about each word in the text file with word's position and line
        /// </summary>
        public void GenerateDictionaryOfAllWords()
        {
            int stringLine = 1;
            int position = 1;
            StringBuilder tmp = new StringBuilder(25);
            for (int i = 0; i < CopyContent.Length; ++i)
            {
                if (CopyContent[i].Equals(' ') ||
                    CopyContent[i].Equals('\n') ||
                    CopyContent[i].Equals('.') ||
                    CopyContent[i].Equals(',') ||
                    CopyContent[i].Equals('!') ||
                    CopyContent[i].Equals('?') ||
                    CopyContent[i].Equals('(') ||
                    CopyContent[i].Equals(')') ||
                    CopyContent[i].Equals('[') ||
                    CopyContent[i].Equals(']') ||
                    CopyContent[i].Equals('\"') ||
                    CopyContent[i].Equals('\r')||
                    CopyContent[i].Equals(':'))
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
        /// <summary>
        /// Printing information about each word
        /// </summary>
        private void PrintFullDictionaryWithStatistic()
        {
            foreach (var dic in AllWords)
            {
                Console.WriteLine($"Position: {dic.Value[2]}\t Index: {dic.Key}\t Line: {dic.Value[1]}\t Word: \'{dic.Value[0]}\'");
            }
        }
        /// <summary>
        /// Print all statistics about each word
        /// </summary>
        public void Statistic()
        {
            var statistics = AllWords.GroupBy(x => x.Value.ElementAt(0));
            var enumerable = statistics as IGrouping<string, KeyValuePair<int, List<string>>>[] ?? statistics.ToArray();
            foreach (var item in enumerable.OrderByDescending(x => x.Count()))
            {
                double percent = Math.Round(Convert.ToDouble(item.Count()) * 100.0 / Convert.ToDouble(statistics.Count()),2);
                Console.WriteLine($"Amount: {item.Count()}\t Percent: {percent}%\t Word: \'{item.Key}\'");
            }
            Console.WriteLine();
        }
    }
}
