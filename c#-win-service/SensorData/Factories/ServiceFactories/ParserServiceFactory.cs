using Interfaces;
using ParserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceFactories
{
    public class ParserServiceFactory
    {
        #region Members
        IParser parser;
        JSONParser jsonParser;
        STRParser strParser;
        XMLParser xmlParser;
        #endregion

        #region Constructor
        public ParserServiceFactory()
        {
            jsonParser = new JSONParser();
            strParser = new STRParser();
            xmlParser = new XMLParser();
        }
        #endregion

        #region Factory
        public IParser GetInstance(string data)
        {
            if (jsonParser.ValidateData(data)) return new JSONParser();
            if (xmlParser.ValidateData(data)) return new XMLParser();

            return new STRParser();
        }
        #endregion
    }
}
