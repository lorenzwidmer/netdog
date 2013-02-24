using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using Newtonsoft.Json;

namespace NetDog
{
    class Settings
    {
        private Dictionary<string, object> _settings;

        public Settings()
        {
            _settings = new Dictionary<string, object>();
        }

        protected object this[string key]
        {
            get
            {
                return _settings[key];
            }
            set
            {
                if (_settings.ContainsKey(key))
                {
                    _settings[key] = value;
                }
                else
                {
                    _settings.Add(key, value);
                }
            }
        }

        public void Save(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement("config"));

            foreach (KeyValuePair<string, object> setting in _settings)
            {
                XmlElement element = doc.CreateElement("setting");
                XmlAttribute name = doc.CreateAttribute("name");
                XmlAttribute type = doc.CreateAttribute("type");

                name.InnerText = setting.Key;
                type.InnerText = this.GetType().GetProperty(setting.Key, BindingFlags.Public | BindingFlags.Instance).PropertyType.ToString();
                Console.WriteLine(type.InnerText);

                element.InnerText = JsonConvert.SerializeObject(setting.Value);
                element.Attributes.Append(name);
                element.Attributes.Append(type);

                doc["config"].AppendChild(element);
            }

            Validate(doc);
            doc.Save(path);
        }

        public void Load(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            Validate(doc);

            foreach (XmlElement element in doc["config"].ChildNodes)
            {
                Type type = GetType(element.Attributes["type"].InnerText);
                string key = element.Attributes["name"].InnerText;
                object value = JsonConvert.DeserializeObject(element.InnerText, type);

                _settings.Add(key, value);
            }
        }

        public void Validate(XmlDocument doc)
        {
            XmlNode root = doc["config"];
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo bla in properties)
            {
                string xPath = String.Format("//setting[@name='{0}']", bla.Name);
                XmlElement element = (XmlElement)root.SelectSingleNode(xPath);

                if (element == null)
                {
                    throw new Exception(String.Format("Property {0} is missing", bla.Name));
                }
                if (element.Attributes["type"].InnerText != bla.PropertyType.ToString())
                {
                    throw new Exception(String.Format("Property {0} has a wrong Type, {1} instead of {2}",
                        bla.Name,
                        bla.PropertyType,
                        element.Attributes["type"].InnerText));
                }
            }
        }

        private Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (type != null)
                {
                    return type;
                }
                type = a.GetType(typeName);
            }
            return type;
        }
    }
}
