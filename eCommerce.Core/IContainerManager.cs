using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Core.Infrastructure.IoC;

namespace eCommerce.Core
{
    /// <summary>
    /// Container Manager Interface
    /// </summary>
    /// <remarks>Support multiple IoC structure</remarks>
    public interface IContainerManager
    {
        void AddComponent<TService>(string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        void AddComponent(Type service, string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        void AddComponent<TFrom, TTo>(string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        void AddComponent(Type tFrom, Type tTo, string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        void AddComponentInstance<TService>(object instance, string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        void AddComponentInstance(Type service, object instance, string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        void AddComponentInstance(object instance, string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        void AddComponentWithParameters<TFrom, TTo>(string key, IDictionary<string, string> options, LifeStyle lifeStyle = LifeStyle.Singleton);
        void AddComponentWithParameters(Type tFrom, Type tTo, string key, IDictionary<string, string> options, LifeStyle lifeStyle = LifeStyle.Singleton);
        TService Resolve<TService>(string key = "") where TService : class; // allow to get object without key
        object Resolve(Type type);
        TService[] ResolveAll<TService>();
        TService ResolveUnregistered<TService>() where TService : class;
        object ResolveUnregistered(Type type);
        void UpdateContainer(Action<dynamic> actions);
    }

    public abstract class ContainerManagerBase<T> : IContainerManager
    {
        public abstract void AddComponent<TService>(string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        public abstract void AddComponent(Type service, string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        public abstract void AddComponent<TFrom, TTo>(string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        public abstract void AddComponent(Type tFrom, Type tTo, string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        public abstract void AddComponentInstance<TService>(object instance, string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        public abstract void AddComponentInstance(Type service, object instance, string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        public abstract void AddComponentInstance(object instance, string key, LifeStyle lifeStyle = LifeStyle.Singleton);
        public abstract void AddComponentWithParameters<TFrom, TTo>(string key, IDictionary<string, string> options, LifeStyle lifeStyle = LifeStyle.Singleton);
        public abstract void AddComponentWithParameters(Type tFrom, Type tTo, string key, IDictionary<string, string> options, LifeStyle lifeStyle = LifeStyle.Singleton);
        public abstract TService Resolve<TService>(string key = "") where TService : class;
        public abstract object Resolve(Type type);
        public abstract TService[] ResolveAll<TService>();
        public abstract TService ResolveUnregistered<TService>() where TService : class;
        public abstract object ResolveUnregistered(Type type);
        public abstract void UpdateContainer_T(Action<T> actions);
        public void UpdateContainer(Action<dynamic> actions)
        {
            if (null == actions)
                return;
            Action<T> newAct = new Action<T>(t => actions((T)t));
            UpdateContainer_T(newAct);
        }

    }

}
