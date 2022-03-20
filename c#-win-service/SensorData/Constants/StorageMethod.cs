using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    /// <summary>
    /// Provides a representation of the storage methods/mode configuration.
    /// </summary>
    public class StorageMethod : Constants
    {
        private StorageMethod(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// The internal value of the configuration.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Returns the configured Entity storage method.
        /// </summary>
        public static StorageMethod ENTITY { get { return new StorageMethod(R_ENTITY); } }
        /// <summary>
        /// Returns the configured API storage method.
        /// </summary>
        public static StorageMethod API { get { return new StorageMethod(R_API); } }
        /// <summary>
        /// Returns the configured File storage method.
        /// </summary>
        public static StorageMethod FILE { get { return new StorageMethod(R_FILE); } }

        /// <summary>
        /// Converts the passed string into the matching StorageMethod object.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static StorageMethod Convert(string str)
        {
            if (str.Equals(R_ENTITY)) return StorageMethod.ENTITY;
            if (str.Equals(R_API)) return StorageMethod.API;
            if (str.Equals(R_FILE)) return StorageMethod.FILE;
            return null;
        }
    }
}
