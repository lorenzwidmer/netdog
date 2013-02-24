using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NetDog
{
    class test: Settings
    {
        public Size Size
        {
            get
            {
                return (Size)this["Size"];
            }
            set
            {
                this["Size"] = value;
            }
        }
        public string DBUser
        {
            get
            {
                return (string)this["DBUser"];
            }
            set
            {
                this["DBUser"] = value;
            }
        }
        public Color Color
        {
            get
            {
                return (Color)this["Color"];
            }
            set
            {
                this["Color"] = value;
            }
        }
    }
}
