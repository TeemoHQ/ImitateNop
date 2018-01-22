using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Core.Infrastructure.Singleton
{
    public class Singleton
    {
        static Singleton()
        {
            allSingletons = new Dictionary<Type, object>();
        }

        static readonly IDictionary<Type, object> allSingletons;

        public static IDictionary<Type, object> AllSingletons
        {
            get { return allSingletons; }
        }
    }

    public class Singleton<T>: Singleton
    {
        static T instance;

        public static T Instance
        {
            get { return instance; }
            set
            {
                instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }
}
