using DbService;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RPGEditor
{
    public static class EditorFeatures
    {
        private const string UI_PATH = "../../../../RPG/Resources/Maps";

        public static bool GenerateEmptyFileToCustom(int width, int height, string filename, string extension = "txt")
        {
            string path = $"{UI_PATH}/{filename}.{extension}";

            if (File.Exists(path))
            {
                return false;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append('█', width);
            for (int i = 0; i < height; i++)
            {
                sb.AppendLine();
                sb.Append('█', width);
            }

            File.WriteAllText(path, sb.ToString());

            return true;
        }

        public static List<Origine> GetOrigins()
        {
            using RpgContext rpgContext = new RpgContext();
            return rpgContext.Origine.ToList();
        }
    }
}
