using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Constante
{
    // Am considerat timpul de reparare ca fiind unul constant si masurat in secunde (pentru testare usoara)
    public class ConstanteAtelier
    {
        public const int TIMP_REPARARE_STANDARD = 20;
        public const int TIMP_REPARARE_AUTOBUZ = 30;
        public const int TIMP_REPARARE_CAMION = 40;

        public static readonly Dictionary<string, int> TIMP_REPARARE = new Dictionary<string, int>
        {
            { ConstanteMasini.TipMasina.Autobuz.ToString(), TIMP_REPARARE_AUTOBUZ },
            { ConstanteMasini.TipMasina.MasinaStandard.ToString(), TIMP_REPARARE_STANDARD },
            { ConstanteMasini.TipMasina.Camion.ToString(), TIMP_REPARARE_CAMION },
        };

        public const int CAPACITATE_STANDARD = 3;
        public const int CAPACITATE_AUTOBUZ = 1;
        public const int CAPACITATE_CAMION = 1;

        public static readonly Dictionary<string, int> CAPACITATE_ATELIER = new Dictionary<string, int>
        {
            { ConstanteMasini.TipMasina.Autobuz.ToString(), CAPACITATE_AUTOBUZ },
            { ConstanteMasini.TipMasina.MasinaStandard.ToString(), CAPACITATE_STANDARD },
            { ConstanteMasini.TipMasina.Camion.ToString(), CAPACITATE_CAMION },
        };

        public const string MASINI_REPARATE = "MasiniReparate";
        public const string AUTOBUZE_NOI_REPARATE = "AutobuzeNoiReparate";
    }
}
