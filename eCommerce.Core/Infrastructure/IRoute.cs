using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Infrastructure
{
    /// <summary>
    /// Implement it for setting how to find target
    /// </summary>
    public interface IRoute
    {
        IDictionary<string, Assembly> GetAllAssemblies();
        IEnumerable<Type> FindType<T>(bool onlyConcreteClasses = true);
        IEnumerable<Type> FindType(Type assignTypeFrom, bool onlyConcreteClasses = true);
        IEnumerable<Type> FindType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
        IEnumerable<Type> FindType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
        IEnumerable<Type> FindType<T, TAssemblyAttribute>(bool onlyConcreteClasses = true) where TAssemblyAttribute : Attribute;
        IEnumerable<Assembly> FindAssembliesWithAttribute<T>();
        IEnumerable<Assembly> FindAssembliesWithAttribute<T>(IEnumerable<Assembly> assemblies);
        IEnumerable<Assembly> FindAssembliesWithAttribute<T>(DirectoryInfo assemblyPath);
    }
}
