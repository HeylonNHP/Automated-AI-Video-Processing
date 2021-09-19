using System.Collections.Generic;
using System.IO;

namespace Automated_AI_Video_Processing.BatchFolderActions
{
    public static class RecursiveFolderSearch
    {
        public static IEnumerable<string> Search(string path)
        {
            return Directory.EnumerateFiles(path, "*", new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = true
            });
        }
    }
}