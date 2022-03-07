using Interfaces;
using Services.FileService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ParserService
{
    public class XMLParser : IParser
    {
        #region IParser
        /// <summary>
        /// Parses an xml file that is structured similarly to app.config files.
        /// 
        /// </summary>
        /// <param name="args">
        /// args[0]: The configuration section to retrieve the child values of (e.g. appSettings).
        /// args[1]: The nodes to select of the descendant nodes of arg[0].
        /// args[2]: The attribute to select from the resulting nodes (after args[1] is applied).
        /// args[3]: The path and filename of the file to parse.
        /// </param>
        /// <returns>A list of all the values of the mentioned nodes.</returns>
        public List<string> ExtractData(params object[] args)
        {
            List<string> extractedAttributeValues = new List<string>();
            XDocument doc = XDocument.Load(args[3].ToString());
            var result = doc.Descendants(args[0].ToString()).Descendants(args[1].ToString()).Attributes(args[2].ToString());

            // Add all the extracted attributes to the returning list.
            foreach (var item in result)
            {
                    extractedAttributeValues.Add(item.Value);
            }
            return extractedAttributeValues;
        }

        /// <summary>
        /// Validates that the passed data are indeed xml valid data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>true if the string passed is valid xml, false otherwise.</returns>
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
