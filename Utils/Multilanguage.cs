using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Fwk.Utils
{
    public class Multilanguage
    {
        public static string ResourcesFilePath { get; set; }
        public static string DefaultCode { get; set; }
        public static string GetValue(string key, string location)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(Multilanguage.ResourcesFilePath);
            XmlNode node = xml.DocumentElement.SelectSingleNode(string.Format("Key[@name='{0}']", key));
            string rv = key;
            if (node != null)
            {
                XmlNode locationNode = node.SelectSingleNode(string.Format("Location[@name='{0}']",location));
                if (locationNode != null)
                {
                    rv = locationNode.Attributes["value"].Value;
                }
            }
            return rv;
        }
    }
}
