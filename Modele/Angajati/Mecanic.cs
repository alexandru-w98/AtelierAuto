using AtelierAuto.Constante;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Modele.Angajati
{
    public class Mecanic: Angajat
    {
        public Mecanic()
        {
            CoeficientSalarial = ConstanteAngajati.CS_MECANIC;
        }
    }
}
