using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Xml;

namespace NetDog.Config
{
    class ConfigXML
    {
        private Type _parent;

        public ConfigXML(Type validator, Path path)
        {
            _parent = validator;
            Load(path);
        }

        public void Save(Path path)
        {
            XmlDocument doc = new XmlDocument();
            PropertyInfo[] properties = _parent.GetProperties(BindingFlags.Public | BindingFlags.Static);
            doc.AppendChild(doc.CreateElement("config"));

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(null);
                XmlElement element = doc.CreateElement("setting");
                XmlAttribute name = doc.CreateAttribute("name");
                XmlAttribute type = doc.CreateAttribute("type");

                name.InnerText = property.Name;
                type.InnerText = property.PropertyType.ToString();

                element.InnerText = JsonConvert.SerializeObject(value);
                element.Attributes.Append(name);
                element.Attributes.Append(type);

                doc["config"].AppendChild(element);
            }

            Validate(doc);
            doc.Save(path);
        }

        public void Load(Path path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            Validate(doc);

            foreach (XmlElement element in doc["config"].ChildNodes)
            {
                Type type = GetType(element.Attributes["type"].InnerText);
                string key = element.Attributes["name"].InnerText;
                object value = JsonConvert.DeserializeObject(element.InnerText, type);

                _parent.InvokeMember(key, BindingFlags.SetProperty, null, null, new object[]{value});
            }
        }

        public void Validate(XmlDocument doc)
        {
            XmlNode root = doc["config"];
            PropertyInfo[] properties = _parent.GetProperties(BindingFlags.Public | BindingFlags.Static);

            foreach (PropertyInfo property in properties)
            {
                string xPath = String.Format("//setting[@name='{0}']", property.Name);
                XmlElement element = (XmlElement)root.SelectSingleNode(xPath);

                if (element == null)
                {
                    throw new Exception(String.Format("Property {0} is missing", property.Name));
                }
                if (element.Attributes["type"].InnerText != property.PropertyType.ToString())
                {
                    throw new Exception(String.Format("Property {0} has a wrong Type, {1} instead of {2}",
                        property.Name,
                        property.PropertyType,
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
