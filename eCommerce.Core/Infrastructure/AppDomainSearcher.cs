using eCommerce.Core.Common;
using System.Collections.Generic;
using System.Reflection;

namespace eCommerce.Core.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>[Built-in Component]</remarks>
    public class AppDomainSearcher : SearcherBase
    {
        private IEnumerable<string> _extraAssemblyNames;

        public AppDomainSearcher()
        {
            this.CheckLoadedAppDomainAssemblies = true;
        }

        public override IDictionary<string, Assembly> GetAllAssemblies()
        {
            var assemblies = base.GetAllAssemblies();

            // TO-DO, write another implemented class
            AddConfiguredAssemblies(assemblies);
            return assemblies;
        }

        public void SetAssemblyNamesToUpload(IEnumerable<string> assemblyNames)
        {
            _extraAssemblyNames = assemblyNames;
        }

        private void AddConfiguredAssemblies(IDictionary<string, Assembly> assemblies)
        {
            _extraAssemblyNames.ForEach(assemblyName => 
            {
                Assembly assembly = Assembly.Load(assemblyName);
                if (!assemblies.Keys.Contains(assembly.FullName))
                {
                    assemblies.Add(assembly.FullName, assembly);
                }
            });
        }
    
    }
}
