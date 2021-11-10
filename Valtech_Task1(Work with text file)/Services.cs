using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Valtech_Task1_Work_with_text_file_
{
    public static class Services
    {
        public static  void WriteToFileList(this List<int> list,string word)
        {
            string path = @"C:\Users\vladyslav.levchenko\Desktop\Result.txt"; // <--- change file path here
            foreach (var item in list)
            {
                try
                {
                    using StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                    sw.WriteLineAsync(item.ToString() + "\t" + word);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
