using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Constante
{
    // CS = Coeficient Salarial
    public class ConstanteAngajati
    {
        public enum TipAngajat { Asistent, Mecanic, Director }
        public const double CS_DIRECTOR = 2;
        public const double CS_MECANIC = 1.5;
        public const double CS_ASISTENT = 1;

        public static readonly Dictionary<TipAngajat, double> COEFICIENTI_SALARIALI = new Dictionary<TipAngajat, double> 
        { { TipAngajat.Asistent, CS_ASISTENT }, { TipAngajat.Director, CS_DIRECTOR }, { TipAngajat.Mecanic, CS_MECANIC} };

        public const int START_ID = 0;
        public const int FACTOR_SALARIU = 1000;
    }
}
