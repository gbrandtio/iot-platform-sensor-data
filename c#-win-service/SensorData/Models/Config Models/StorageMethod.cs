using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config_Models
{
    public class StorageMethod : Constants
    {
        private StorageMethod(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }
        public static StorageMethod ENTITY { get { return new StorageMethod(R_ENTITY); } }
        public static StorageMethod API { get { return new StorageMethod(R_API); } }
        public static StorageMethod FILE { get { return new StorageMethod(R_FILE); } }

        public static StorageMethod Convert(string str)
        {
            if (str.Equals(R_ENTITY)) return StorageMethod.ENTITY;
            if (str.Equals(R_API)) return StorageMethod.API;
            if (str.Equals(R_FILE)) return StorageMethod.FILE;
            return null;
        }
    }
}
