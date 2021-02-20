using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Constante
{
    // CS = Coeficient Salarial
    public class ConstanteAngajati
    {
        public enum TIP_ANGAJAT { Asistent, Mecanic, Director }
        public const double CS_DIRECTOR = 2;
        public const double CS_MECANIC = 1.5;
        public const double CS_ASISTENT = 1;

        public static readonly Dictionary<TIP_ANGAJAT, double> COEFICIENTI_SALARIALI = new Dictionary<TIP_ANGAJAT, double> 
        { { TIP_ANGAJAT.Asistent, CS_ASISTENT }, { TIP_ANGAJAT.Director, CS_DIRECTOR }, { TIP_ANGAJAT.Mecanic, CS_MECANIC} };

        public const int START_ID = 0;
    }
}
