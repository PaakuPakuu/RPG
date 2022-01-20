using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralUtils
{
    public static class GeneralUtils
    {
        public static string[] GetTemplate(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }
    }
}
