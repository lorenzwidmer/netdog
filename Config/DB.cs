using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace NetDog.Config
{
    static class DB
    {
        private static Path _path = new Path(@".\Config\DB.xml");
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

        public static string User
        {
            get
            {
                return (string)File["User"];
            }
            set
            {
                File["User"] = value;
            }
        }

        public static string Password
        {
            get
            {
                return (string)File["Password"];
            }
            set
            {
                File["Password"] = value;
            }
        }

        public static Path Database
        {
            get
            {
                return (Path)File["Database"];
            }
            set
            {
                File["Database"] = value;
            }
        }

        public static string Charset
        {
            get
            {
                return (string)File["Charset"];
            }
            set
            {
                File["Charset"] = value;
            }
        }

        public static short Dialect
        {
            get
            {
                return (short)File["Dialect"];
            }
            set
            {
                File["Dialect"] = value;
            }
        }
    }
}
