using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Modele.Raspunsuri
{
    public class RaspunsServiciu<T>
    {
        public T Continut { get; set; }
        public bool Succes { get; set; }
        public string Mesaj { get; set; }
    }
}
