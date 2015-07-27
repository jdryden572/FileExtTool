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
        public static string Extension = ".dll";
        public static string SearchDir = "C:\\Program Files";
        public static string OutputDir = "C:\\Users\\Jdryden\\Documents\\TestCopy";

        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo(SearchDir);
            List<FileInfo> matchingFiles = FindFilesByExt(dir, Extension, true);

            foreach (FileInfo file in matchingFiles)
            {
                Console.WriteLine(file.FullName);
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
                           where f.Extension == ext
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
