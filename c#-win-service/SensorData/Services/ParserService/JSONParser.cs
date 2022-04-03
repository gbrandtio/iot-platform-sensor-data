using Constants;
using Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserService
{
    /// <summary>
    /// Provides functionality related with JSON parsing and data extraction.
    /// </summary>
    public class JSONParser : IParser
    {
        #region IParser
        /// <summary>
        /// Constructs a list that contains a string of the json value to be retrieved.
        /// Assumes that the data to be parsed is a json array with a name and is used to
        /// select the field value only from a specific json object of the json array.
        /// </summary>
        /// <param name="args">
        /// args[0]: The data to be parsed.
        /// args[1]: The name of the json array to select.
        /// args[2]: The array position to select data from.
        /// args[3]: The field to select.
        /// </param>
        /// <returns></returns>
        public List<string> ExtractData(params object[] args)
        {
            List<string> extractedData = new List<string>();
            try
            {
                JObject jResponse = JObject.Parse(args[0].ToString());
                JArray jResultsArray = (JArray)jResponse[args[1].ToString()];
                extractedData.Add(jResultsArray[2][args[3].ToString()].ToString());
            }
            catch (Exception e)
            {

            }
            return extractedData;
        }

        /// <summary>
        /// Validates that a string is a valid, parseable JSON.
        /// </summary>
        /// <param name="json">The JSON to parse.</param>
        /// <returns>true if a valid json string was passed, otherwise false.</returns>
        public bool ValidateData(string json)
        {
            bool isValidJson = true;
            if ((json.StartsWith("{") && json.EndsWith("}")) || (json.StartsWith("[") && json.EndsWith("]") && !String.IsNullOrEmpty(json)))
            {
                try
                {
                    JObject jObject = JObject.Parse(json);
                }
                catch (Exception e)
                {
                    isValidJson = false;
                }
            }
            else isValidJson = false;

            return isValidJson;
        }
        #endregion

        #region JSON Parsing Methods
        /// <summary>
        /// Loops through a JToken in order to find the specified property and 
        /// return it's value.
        /// </summary>
        /// <param name="jToken">The JObject.</param>
        /// <param name="propertyToExtract">The property to extract.</param>
        /// <returns>The value of the specified property if found, otherwise null.</returns>
        public string ExtractJPropertyFromJObject(JToken jToken, string propertyToExtract)
        {
            foreach (JProperty property in jToken)
            {
                if (property.Name.Equals(propertyToExtract)) return property.Value.ToString();
            }
            return null;
        }

        /// <summary>
        /// Loops through a JArray and constructs a dictionary with the values of the JSON fields
        /// specified in args.
        /// </summary>
        /// <param name="jArray">The JArray to loop through</param>
        /// <param name="field1">The value from this field will be the key of the dictionary.</param>
        /// <param name="field2">The value from this field will be the value of the dictionary.</param>
        /// <returns>A dictionary containing the extracted values.
        /// The dictionary can be empty.</returns>
        public Dictionary<string, string> ExtractJMeasurementsFromEachJObject(JArray jArray, string field1, string field2)
        {
            Dictionary<string, string> jPropertyValuePairs = new Dictionary<string, string>();
            foreach (JObject jObject in jArray)
            {
                string key = jObject.GetValue(field1).ToString();
                string value = jObject.GetValue(field2).ToString();
                jPropertyValuePairs.Add(key, value);
            }
            return jPropertyValuePairs;
        }
        #endregion
    }
}
