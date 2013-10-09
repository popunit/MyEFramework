using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace eCommerce.Core.Common
{
    internal class LookUp
    {
        private static readonly IDictionary<Type, object> Dict
            = new ConcurrentDictionary<Type, object>();

        private LookUp()
        {
            // do nothing, avoid to instantiate it
        }

        internal static IDictionary<Type, object> Objects
        {
            get { return Dict; }
        }
    }

    public class Singleton<T>
    {
        private static T _instance; // store the data with type T, avoid to re-look up into dictionary
        public static T Instance
        {
            get 
            { 
                return _instance; 
            }
            set 
            {
                _instance = value;
                LookUp.Objects[typeof(T)] = value;
            }
        }
    }

    public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        static SingletonDictionary()
        {
            Singleton<Dictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
        }

        new public static IDictionary<TKey, TValue> Instance
        {
            get { return Singleton<Dictionary<TKey, TValue>>.Instance; }
        }
    }

    /// <summary>
    /// Thread-safe dictionary
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SingletonConcurrentDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        static SingletonConcurrentDictionary()
        {
            Singleton<ConcurrentDictionary<TKey, TValue>>.Instance = new ConcurrentDictionary<TKey, TValue>();
        }

        new public static IDictionary<TKey, TValue> Instance
        {
            get { return Singleton<ConcurrentDictionary<TKey, TValue>>.Instance; }
        }
    }

}
