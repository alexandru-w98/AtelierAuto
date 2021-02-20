using AtelierAuto.Modele.Angajati;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Servicii.Angajati
{
    public class ServiciuAngajati : IServiciuAngajati
    {
        public List<Angajat> _angajati { get; set; }

        public ServiciuAngajati()
        {
            _angajati = new List<Angajat>();
        }
    }
}
