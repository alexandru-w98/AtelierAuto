using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Constante
{
    // PA = Polita Asigurare
    public class ConstanteMasini
    {
        public enum ModTransmisie { Manual, Automat}
        public enum Tonaj { }

        public const int FACTOR_PA_STANDARD = 100;
        public const int TAXA_DIESEL_STANDARD = 500;
        public const int TAXA_KILOMETRI_STANDARD = 500;
        public const int LIMITA_KILOMETRI_TAXA_STANDARD = 200000;

        public const int FACTOR_PA_AUTOBUZ = 200;
        public const int TAXA_DIESEL_AUTOBUZ = 1000;
        public const int TAXA_KILOMETRI_AUTOBUZ_1 = 1000;
        public const int LIMITA_KILOMETRI_TAXA_AUTOBUZ_1 = 200000;
        public const int TAXA_KILOMETRI_AUTOBUZ_2 = 500;
        public const int LIMITA_KILOMETRI_TAXA_AUTOBUZ_2 = 100000;

        public const int FACTOR_PA_CAMION = 300;
        public const int TAXA_KILOMETRI_CAMION = 700;
        public const int LIMITA_KILOMETRI_TAXA_CAMION = 800000;

        public const double DISCOUNT_STANDARD = 5;
        public const double DISCOUNT_AUTOBUZ = 10;
        public const double DISCOUNT_CAMION = 15;
    }
}
