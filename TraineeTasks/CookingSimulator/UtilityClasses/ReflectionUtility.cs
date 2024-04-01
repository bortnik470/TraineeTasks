using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.CookingSimulator.UtilityClasses
{
    internal class ReflectionUtility
    {
        private Assembly _assembly;

        public ReflectionUtility(string pathToAssembly)
        {
            _assembly = Assembly.LoadFrom(pathToAssembly);
        }

        public Type[] GetTypes()
        {
            return _assembly.GetTypes();
        }

        public object CreateClass(Type classType)
        {
            return Activator.CreateInstance(classType);
        }
    }
}
