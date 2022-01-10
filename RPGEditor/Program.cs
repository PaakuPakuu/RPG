using System;
using System.IO;
using System.Text;

namespace RPGEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool res = GenerateEmptyFileToCustom(10, 10, "test", "txt");
            //Console.WriteLine(res);
            Console.ReadKey();
        }

        public static bool GenerateEmptyFileToCustom(int width, int height, string filename, string extension)
        {
            if (File.Exists($"../../../../RPG/Resources/UI/{filename}.{extension}"))
            {
                return false;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(' ', width);
            for (int i = 0; i < height; i++)
            {
                sb.AppendLine();
                sb.Append(' ', width);
            }

            File.WriteAllText($"../../../../RPG/Resources/UI/{filename}.{extension}", sb.ToString());

            return true;
        }
    }
}
