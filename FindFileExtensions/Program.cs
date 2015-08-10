using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindFileExtensions
{
    class Program
    {
        public static string Extension = ".jpg";
        public static string SearchDir = "D:\\Pictures";
        public static string OutputDir = "E:\\test";
        public static DateTime startTime = DateTime.Now;

        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo(SearchDir);
            List<FileInfo> matchingFiles = FindFilesByExt(dir, Extension, true);

            float total = matchingFiles.Count;

            long size = matchingFiles.Sum(f => f.Length)/1000000;
            Console.WriteLine("{0} files found, {1} MB", total, size);
            Console.WriteLine("Copy files to {0} ? (y/n)", OutputDir);
            string proceed = Console.ReadLine().ToUpper();
            /*
            Console.WriteLine("Test sting: asdbfajsiodj");
            Console.ReadLine();
            Console.SetCursorPosition(1,0);
            Console.WriteLine("Done");
            */
            if (proceed == "Y")
            {
                float counter = 1;

                foreach (FileInfo file in matchingFiles)
                {
                    //file.CopyTo(OutputDir + "\\" + file.Name, true);
                    Console.SetCursorPosition(1, 3);
                    Console.WriteLine("Copying... {0}%", ((counter / total) * 100).ToString("F0"));
                    counter++;
                }

                Console.WriteLine(DateTime.Now - startTime);
            }


            Console.ReadLine();
        }




        /// <summary>
        /// Searches directory for files of given extension.
        /// </summary>
        /// <param name="root">The root directory to search</param>
        /// <param name="ext">The extension to search for (include the period) e.g. ".jpg"</param>
        /// <param name="recursive">Searches recursively if true. Default is false.</param>
        /// <returns>List of type System.IO.FileInfo</returns>
        static List<FileInfo> FindFilesByExt(DirectoryInfo root, string ext, bool recursive = false)
        {
            List<FileInfo> files = new List<FileInfo>();

            files.AddRange(from f in root.EnumerateFiles()
                           where f.Extension.ToUpper() == ext.ToUpper()
                           select f);

            if (recursive)
            {
                foreach (var dir in root.GetDirectories())
                {
                    files.AddRange(FindFilesByExt(dir, ext, true));
                }
            }
            return files;
        }

    }
}
