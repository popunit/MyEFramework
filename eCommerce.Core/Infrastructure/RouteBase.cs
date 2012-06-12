using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Common;
using System.IO;
using System.Text.RegularExpressions;

namespace eCommerce.Core.Infrastructure
{
    public abstract class RouteBase : IRoute
    {
        private readonly string assemblySkipLoadingPattern
            = "^System|^mscorlib|^Microsoft|^CppCodeProvider|^VJSharpCodeProvider|^WebDev|^Castle|^Iesi|^log4net|^NHibernate|^nunit|^TestDriven|^MbUnit|^Rhino|^QuickGraph|^TestFu|^Telerik|^ComponentArt|^MvcContrib|^AjaxControlToolkit|^Antlr3|^Remotion|^Recaptcha";

        private readonly string assemblyRestrictToLoadingPattern = ".*";

        protected bool CheckLoadedAppDomainAssemblies
        {
            get;
            set;
        }

        public virtual void Init()
        { 
        }

        public virtual IDictionary<string, Assembly> GetAllAssemblies()
        {
            Init();

            //var addedAssemblyNames = new List<string>();
            //var assemblies = new List<Assembly>();
            var assemblies = new Dictionary<string, Assembly>();

            if (CheckLoadedAppDomainAssemblies)
                AddAssembliesInAppDomain(assemblies);

            return assemblies;
        }

        public virtual IEnumerable<Type> FindType<T>(bool onlyConcreteClasses = true)
        {
            return this.FindType(typeof(T), onlyConcreteClasses);
        }

        public virtual IEnumerable<Type> FindType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            return this.FindType(assignTypeFrom, GetAllAssemblies().Values, onlyConcreteClasses);
        }

        public virtual IEnumerable<Type> FindType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            var types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                foreach (var t in assembly.GetTypes())
                {
                    if (t.IsInherit(assignTypeFrom))
                    {
                        if (!t.IsInterface)
                        {
                            if (onlyConcreteClasses)
                            {
                                if (t.IsClass && !t.IsAbstract)
                                {
                                    types.Add(t);
                                }
                            }
                            else
                            {
                                types.Add(t);
                            }
                        }
                    }
                }
            }

            return types;
        }

        public virtual IEnumerable<Type> FindType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<Type> FindType<T, TAssemblyAttribute>(bool onlyConcreteClasses = true) where TAssemblyAttribute : Attribute
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<System.Reflection.Assembly> FindAssembliesWithAttribute<T>()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<System.Reflection.Assembly> FindAssembliesWithAttribute<T>(IEnumerable<System.Reflection.Assembly> assemblies)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<System.Reflection.Assembly> FindAssembliesWithAttribute<T>(System.IO.DirectoryInfo assemblyPath)
        {
            throw new NotImplementedException();
        }

        protected void UploadAssembliesToAppDomain(string[] directoryPaths)
        {
            if (null == directoryPaths)
                return;

            var availablePaths = new List<string>();
            directoryPaths.ForEach(path =>
            {
                if (Directory.Exists(path))
                    availablePaths.Add(path);
            });

            if (availablePaths.Count() == 0)
                return;

            var loadedAssemblyNames = GetAllAssemblies().Keys; // get all the assemblies which is existed in current AppDomain

            availablePaths.ForEach(path =>
                {
                    Directory.GetFiles(path, "*.dll").ForEach(dll =>
                    {
                        var assemblyName = AssemblyName.GetAssemblyName(dll);
                        if (Matches(assemblyName.FullName) && !loadedAssemblyNames.Contains(assemblyName.FullName)) // if the assembly hasn't been upload, load it
                        {
                            AppDomain.CurrentDomain.Load(assemblyName);
                        }
                    });
                }
            );
        }

        private bool Matches(string assemblyFullName)
        {
            return !Matches(assemblyFullName, assemblySkipLoadingPattern)
                   && Matches(assemblyFullName, assemblyRestrictToLoadingPattern);
        }

        private bool Matches(string assemblyFullName, string pattern)
        {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        //private void AddAssembliesInAppDomain(List<string> addedAssemblyNames, List<Assembly> assemblies)
        private void AddAssembliesInAppDomain(IDictionary<string, Assembly> assemblies)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (Matches(assembly.FullName))
                {
                    if (!assemblies.Keys.Contains(assembly.FullName))
                    {
                        assemblies.Add(assembly.FullName, assembly);
                    }
                }
            }
        }
    }
}
