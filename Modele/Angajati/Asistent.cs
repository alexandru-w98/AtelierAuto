using AtelierAuto.Constante;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Modele.Angajati
{
    public class Asistent : Angajat
    {
        public Asistent()
        {
            CoeficientSalariat = ConstanteAngajati.CS_ASISTENT;
        }
    }
}
