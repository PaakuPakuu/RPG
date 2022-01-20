namespace GeneralUtils
{
    public static class ResourcesUtils
    {
        public const string MAPS_PATH = "Resources/Maps";
        public const string UI_PATH = "Resources/UI";

        public static string[] GetTemplate(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }
    }
}
