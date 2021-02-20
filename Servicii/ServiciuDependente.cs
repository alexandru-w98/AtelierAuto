using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Servicii
{
    public static class ServiciuDependente
    {
        private static Dictionary<Type, object> _servicii = new Dictionary<Type, object>();

        public static void Register<T>(object Timpl)
        {
            _servicii.Add(typeof(T), Timpl);
        }

        public static T Get<T>() where T : class
        {
            return (T)_servicii[typeof(T)];
        }
    }
}
