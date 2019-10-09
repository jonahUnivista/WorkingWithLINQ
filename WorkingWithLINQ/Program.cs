using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace WorkingWithLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\windows";
            //ShowLargeFilesWithoutLinq(path);
            Console.WriteLine("*****");
            ShowLargeFilesWithLinq(path);
        }

        private static void ShowLargeFilesWithLinq(string path)
        {
            // Two versions of LINQ 

            var query = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;
            foreach (var file in query.Take(5))
            {
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
            }

            Console.WriteLine("*****");


            var query2 = new DirectoryInfo(path).GetFiles()
                        .OrderByDescending(f => f.Length)
                        .Take(5);

            foreach (var file in query2)
            {
                Console.WriteLine($"{file.Name, -20} : {file.Length, 10:N0}");
            }

        }

        private static void ShowLargeFilesWithoutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            FileInfo[] files = directory.GetFiles();
            Array.Sort(files);
            foreach (FileInfo file in files)
            {
                Console.WriteLine($"{file.Name} : {file.Length}");
            }
        }
    }

    //public class FileInfoComparer : IComparable<FileInfo>
    //{
    //    public int Compare(FileInfo x, FileInfo y)
    //    {
    //        return y.Length.CompareTo(x.Length);
    //    }

    //}

}
