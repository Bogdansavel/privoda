using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Utils
{
    public static class FileUtil
    {

        public static IEnumerable<string> GetLibraryFileNames(string dirName)
        {
            var directory = new DirectoryInfo(@"wwwroot/documents/" + dirName);
            var Files = directory.GetFiles();
            var fileNames = new List<string>();
            foreach (FileInfo file in Files)
            {
                fileNames.Add(file.Name);
            }
            return fileNames;
        }

    }
}
