using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;

namespace eCommerce.Core.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>[Built-in Component]</remarks>
    internal class AppDomainRoute : RouteBase
    {
        private IEnumerable<string> extraAssemblyNames;

        public AppDomainRoute()
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
            extraAssemblyNames = assemblyNames;
        }

        private void AddConfiguredAssemblies(IDictionary<string, Assembly> assemblies)
        {
            extraAssemblyNames.ForEach(assemblyName => 
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
