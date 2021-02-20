using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Modele.Angajati
{
    public abstract class Angajat
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime DataNasterii { get; set; }
        public DateTime DataAngajarii { get; set; }
        public double CoeficientSalarial { get; set; }
    }
}
