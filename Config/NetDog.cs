using System.Reflection;

namespace NetDog.Config
{
    static class NetDog
    {
        private static Path _path = new Path(@".\Config\NetDog.xml");
        private static SettingFile _file = new SettingFile(MethodBase.GetCurrentMethod().DeclaringType, _path);

        public static void Save()
        {
            _file.Save(_path);
        }

        public static Path FirebirdClient { get; set; }
    }
}
