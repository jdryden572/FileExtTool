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
        public static string Extension = ".pdf";
        public static string SearchDir = "C:\\Users\\Jdryden\\Downloads";
        public static string OutputDir = "C:\\Users\\Jdryden\\Documents\\TestCopy";

        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo(SearchDir);
            List<FileInfo> matchingFiles = FindFilesByExt(dir, Extension, true);

            int total = matchingFiles.Count;
            /*
            Console.WriteLine("Test sting: asdbfajsiodj");
            Console.ReadLine();
            Console.SetCursorPosition(1,0);
            Console.WriteLine("Done");
            */
            int counter = 1;
            
            foreach (FileInfo file in matchingFiles)
            {
                file.CopyTo(OutputDir + "\\" + file.Name, true);
                Console.SetCursorPosition(1,0);
                Console.WriteLine("Copying... {0,3}%", (counter / total) * 100);
                counter++;
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
