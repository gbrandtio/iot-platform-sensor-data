using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ParserService
{
    public class XMLParser : IParser
    {
        #region IParser
        public List<string> ExtractData(params object[] args)
        {
            return null;
        }

        public bool ValidateData(string data)
        {
            bool isValidXML = true;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(data);
            }
            catch (Exception e)
            {
                isValidXML = false;
            }
            return isValidXML;
        }
        #endregion
    }
}
