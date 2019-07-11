using System.Collections.Generic;
using System.Xml;
using PerfectoTest.PerfectoAPI;

namespace PerfectoTest.Utils
{
    internal class XmlHandler
    {
        private XmlElement xmlDocument;

        public XmlHandler(string url)
        {
            xmlDocument = GetXmlResponse(url);
        }

        private XmlElement GetXmlResponse(string url)
        {
            XmlDocument doc1 = new XmlDocument();
            doc1.Load(url);
            return doc1.DocumentElement;
        }

        public bool IsDeviceAvailable()
        {
            XmlNodeList nodes = this.xmlDocument.SelectNodes("/handset");
            foreach (XmlNode node in nodes)
            {
                string available = node["available"].InnerText;
                string inUse = node["inUse"].InnerText;

                if (available.Equals("true") && inUse.Equals("false"))
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> AvailableDeviceList()
        {
            // https://developers.perfectomobile.com/display/PD/Get+Devices+List
            XmlNodeList nodes = this.xmlDocument.SelectNodes("/handsets/handset");
            var availableDeviceList = new List<string>();

            foreach (XmlNode node in nodes)
            {
                string deviceName = node["deviceId"].InnerText;
                string available = node["available"].InnerText;
                string inUse = node["inUse"]?.InnerText;
                string reserved = node["reserved"]?.InnerText;
                string reservedTo = node["reservedTo"]?.InnerText;
                
                if (available.Equals("true") && inUse.Equals("false") && reserved.Equals("true") && reservedTo.Equals(Access.Email))
                {
                    availableDeviceList.Add(deviceName);
                }
            }

            return availableDeviceList;
        }
    }
}
