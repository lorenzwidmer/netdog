using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace NetDog.Config
{
    static class NetDog
    {
        private static Path _path = new Path(@".\Config\NetDog.xml");

        private static SettingFile _file = null;
        private static SettingFile File
        {
            get
            {
                if (_file == null)
                {
                    _file = new SettingFile(MethodBase.GetCurrentMethod().DeclaringType);
                    _file.Load(_path);
                }
                return _file;
            }
        }

        public static void Save()
        {
            _file.Save(_path);
        }

        public static Path FirebirdClient
        {
            get
            {
                return (Path)File["FirebirdClient"];
            }
            set
            {
                File["FirebirdClient"] = value;
            }
        }
    }
}
