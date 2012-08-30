using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using eCommerce.Core.Infrastructure.IoC.Web;

namespace eCommerce.Core.Infrastructure.IoC
{
    public class AutofacContainerManager : ContainerManagerBase<ContainerBuilder>
    {
        private readonly IContainer container;

        /// <summary>
        /// Container with life time
        /// </summary>
        public ILifetimeScope GetContainer()
        {
            if (AutofacLifetimeScopeHttpModule.IsValid())
                return AutofacLifetimeScopeHttpModule.GetLifetimeScope(container);
            return container;
        }

        public AutofacContainerManager(IContainer container)
        {
            //this.builder = builder;
            this.container = container;
        }

        public override void AddComponent<T>(string key, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            AddComponent<T, T>(key, lifeStyle);
        }

        public override void AddComponent(Type service, string key, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            AddComponent(service, service, key, lifeStyle);
        }

        public override void AddComponent<TFrom, TTo>(string key, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            AddComponent(typeof(TFrom), typeof(TTo), key, lifeStyle);
        }

        public override void AddComponent(Type tFrom, Type tTo, string key, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            if (String.IsNullOrEmpty(key))
                return;

            UpdateContainer_T(build =>
            {
                var currentBuild = build as ContainerBuilder;
                var types = new List<Type> { tFrom };

                // check if the type is generic type
                if (tFrom.IsGenericType && tTo.IsGenericType)
                {
                    var reg = currentBuild.RegisterGeneric(tTo).As(tFrom).PerLifeStyle(lifeStyle);
                    reg.Keyed(key, tFrom); // set key and type resolved
                }
                else
                {
                    var reg = currentBuild.RegisterType(tTo).As(tFrom).PerLifeStyle(lifeStyle);
                    reg.Keyed(key, tFrom); // set key and type resolved
                }
            });
        }

        public override void AddComponentInstance<T>(object instance, string key, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            AddComponentInstance(typeof(T), instance, key, lifeStyle);
        }

        public override void AddComponentInstance(Type service, object instance, string key, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            UpdateContainer(build => 
            {
                var currentBuild = build as ContainerBuilder;
                currentBuild.RegisterInstance(instance).Keyed(key, service).As(service).PerLifeStyle(lifeStyle);
            });
        }

        public override void AddComponentInstance(object instance, string key, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            AddComponentInstance(instance.GetType(), instance, key, lifeStyle);
        }

        public override void AddComponentWithParameters<TFrom, TTo>(string key, IDictionary<string, string> parameters, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            AddComponentWithParameters(typeof(TFrom), typeof(TTo), key, parameters, lifeStyle);
        }

        public override void AddComponentWithParameters(Type tFrom, Type tTo, string key, IDictionary<string, string> parameters, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            if (String.IsNullOrEmpty(key))
                return;
            UpdateContainer_T(build =>
            {
                var currentBuild = build as ContainerBuilder;
                var temp = currentBuild.RegisterType(tTo).As(tFrom).
                    WithParameters(parameters.Select(y => new NamedParameter(y.Key, y.Value)));
                if (!string.IsNullOrEmpty(key))
                {
                    temp.Keyed(key, tFrom);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">
        /// if key is null, will get the default one from container even though
        /// there are more than one configuration item for the type
        /// </param>
        /// <returns></returns>
        public override T Resolve<T>(string key = "")
        {
            var con = GetContainer();
            if (String.IsNullOrEmpty(key))
                return con.Resolve<T>();
            return con.ResolveKeyed<T>(key);
        }

        public override object Resolve(Type type)
        {
            return GetContainer().Resolve(type);
        }

        public override T[] ResolveAll<T>()
        {
            return GetContainer().Resolve<IEnumerable<T>>().ToArray();
            //return GetContainer().ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        public override T ResolveUnregistered<T>()
        {
            throw new NotImplementedException();
        }

        public override object ResolveUnregistered(Type type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update current container
        /// </summary>
        /// <param name="actions"></param>
        public override void UpdateContainer_T(Action<ContainerBuilder> actions)
        {
            var builder = new ContainerBuilder();
            actions(builder);
            builder.Update(container);
        }
    }
}
