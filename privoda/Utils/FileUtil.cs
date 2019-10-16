using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace privoda.Utils
{
    public class FileUtil
    {

        public List<String> getLibraryFileNames(string dirName)
        {
            DirectoryInfo d = new DirectoryInfo(@"wwwroot/documents/" + dirName);
            FileInfo[] Files = d.GetFiles();
            List<String> fileNames = new List<String>();
            foreach (FileInfo file in Files)
            {
                fileNames.Add(file.Name);
            }
            return fileNames;
        }

    }
}
