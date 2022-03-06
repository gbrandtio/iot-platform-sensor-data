using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjService
{
    public class Instantiator
    {
        /// <summary>
        /// Dynamically creates an object of the passed type by using it's class empty constructor.
        /// </summary>
        /// <param name="type">The type of the object to create.</param>
        /// <returns>An object of the specified type.</returns>
        public static dynamic GetObject(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Instantiates an object that matches an empty constructor.
        /// The method searches for classes that match these costructors only inside the passed assembly.
        /// </summary>
        /// <param name="type">The type of the object to create.</param>
        /// <param name="assembly">The assembly to load the type from.</param>
        /// <returns>An instance of the object or null.</returns>
        public static dynamic GetObject(Type type, String assembly)
        {
            List<Type> types = GetTypes(assembly);
            object dynamicObject = null;
            foreach (Type assemblyType in types)
            {
                dynamicObject = Activator.CreateInstance(assemblyType);
            }
            return dynamicObject;
        }

        /// <summary>
        /// Instantiates an object that matches a constructor that takes as arguments (double ,ILocation).
        /// The method searches for classes that match these costructors only inside the passed assembly.
        /// </summary>
        /// <param name="type">The type of the object to create.</param>
        /// <param name="assembly">The assembly to load the type from.</param>
        /// <returns>An instance of the object or null.</returns>
        public static dynamic GetObject(string type, string assembly, double measurementValue, ILocation location)
        {
            List<Type> types = GetTypes(assembly);
            foreach (Type assemblyType in types)
            {
                if (assemblyType.Name.ToLower().Equals(type.ToLower()))
                {
                    return Activator.CreateInstance(assemblyType, measurementValue, location);
                }
            }
            return null;
        }

        /// <summary>
        /// Finds all the types of the passed assembly.
        /// </summary>
        /// <returns>All types inside the passed assembly.</returns>
        public static List<Type> GetTypes(string assembly)
        {
            return Assembly.UnsafeLoadFrom(assembly).GetTypes().ToList();
        }
    }
}
