using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataHandlerFactories
{
    public class DataHandlerFactory
    {
        public IDataHandler GetDataHandler(Type type)
        {
            return Instantiate(type);
        }

        /// <summary>
        /// Instantiates an object that matches an empty constructor.
        /// The method searches for classes that match these costructors only inside Handlers.
        /// </summary>
        /// <param name="type">The type of the objct to create.</param>
        /// <param name="measurementValue">The measurement value.</param>
        /// <param name="location">The location object</param>
        /// <returns>An instance of the object or null.</returns>
        private dynamic Instantiate(Type type)
        {
            List<Type> types = GetIMeasurementTypes();
            foreach (Type assemblyType in types)
            {
                if (assemblyType.Name.ToLower().Equals(type.Name.ToLower()))
                {
                    return Activator.CreateInstance(assemblyType);
                }
            }
            return null;
        }

        /// <summary>
        /// Finds all the types of Handlers.
        /// </summary>
        /// <returns>All the Model types.</returns>
        private static List<Type> GetIMeasurementTypes()
        {
            return Assembly.UnsafeLoadFrom("Handlers.dll").GetTypes().ToList();
        }
    }
}
