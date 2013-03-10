using System.Reflection;

namespace NetDog.Config
{
    static class DB
    {
        private static Path _path = new Path(@".\Config\DB.xml");
        private static ConfigXML _file = new ConfigXML(MethodBase.GetCurrentMethod().DeclaringType, _path);

        public static void Save()
        {
            _file.Save(_path);
        }

        public static string User { get; set; }

        public static string Password { get; set; }

        public static Path Database { get; set; }

        public static string Charset { get; set; }

        public static short Dialect { get; set; }
    }
}
