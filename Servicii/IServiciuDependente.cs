using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Servicii
{
    public interface IServiciuDependente
    {
        void Register<T>(object Timpl);
        T Get<T>() where T : class;
    }
}
