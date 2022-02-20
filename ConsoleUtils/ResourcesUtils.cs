namespace GeneralUtils
{
    public static class ResourcesUtils
    {
        public const string MAPS_PATH = "Resources/Maps";
        public const string UI_PATH = "Resources/UI";
        public const string DIALOGS_PATH = "Resources/Dialogs";

        public static string[] GetFileLines(string path)
        {
            return System.IO.File.ReadAllLines(path);
        }
    }
}
