using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Servicii
{
    public class ServiciuDependente: IServiciuDependente
    {
        private Dictionary<Type, object> _servicii = new Dictionary<Type, object>();

        public void Register<T>(object Timpl)
        {
            _servicii.Add(typeof(T), Timpl);
        }

        public T Get<T>() where T : class
        {
            return (T)_servicii[typeof(T)];
        }
    }
}
