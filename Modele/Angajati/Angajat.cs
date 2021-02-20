using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Modele.Angajati
{
    public abstract class Angajat
    {
        public int ID { get; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public DateTime DataNasterii { get; set; }
        public DateTime DataAngajarii { get; set; }
        public double CoeficientSalariat { get; protected set; }

        public virtual double CalculareSalariu()
        {
            return (DateTime.Now.Year - DataAngajarii.Year) * CoeficientSalariat * 1000;
        }
    }
}
