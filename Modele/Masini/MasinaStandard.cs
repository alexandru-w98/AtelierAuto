using AtelierAuto.Constante;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Modele.Masini
{
    public class MasinaStandard : Masina
    {
        public ConstanteMasini.ModTransmisie ModTransmisie { get; set; }

        public override double CalculeazaPolitaAsigurare(bool discount = false)
        {
            int taxaDiesel = EsteMotorDiesel ? ConstanteMasini.TAXA_DIESEL_STANDARD : 0;
            int taxaKilometri = NrKilometri > ConstanteMasini.LIMITA_KILOMETRI_TAXA_STANDARD ? ConstanteMasini.TAXA_KILOMETRI_STANDARD : 0;

            if (!discount)
            {
                return (DateTime.Now.Year - AnulFabricatiei) * ConstanteMasini.FACTOR_PA_STANDARD + taxaDiesel + taxaKilometri;
            } else
            {
                return ((DateTime.Now.Year - AnulFabricatiei) * ConstanteMasini.FACTOR_PA_STANDARD + taxaDiesel + taxaKilometri)
                    * ConstanteMasini.DISCOUNT_STANDARD / 100;
            }
        }
    }
}
