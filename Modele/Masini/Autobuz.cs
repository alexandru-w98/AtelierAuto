using AtelierAuto.Constante;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Modele.Masini
{
    public class Autobuz : Masina
    {
        public int NumarLocuri { get; set; }

        public override double CalculeazaPolitaAsigurare(bool discount = false)
        {
            int taxaDiesel = EsteMotorDiesel ? ConstanteMasini.TAXA_DIESEL_AUTOBUZ : 0;
            int taxaKilometri1 = NrKilometri > ConstanteMasini.LIMITA_KILOMETRI_TAXA_AUTOBUZ_1 ? ConstanteMasini.TAXA_KILOMETRI_AUTOBUZ_1 : 0;
            int taxaKilometri2 = NrKilometri > ConstanteMasini.LIMITA_KILOMETRI_TAXA_AUTOBUZ_2 ? ConstanteMasini.TAXA_KILOMETRI_AUTOBUZ_2 : 0;

            if (!discount)
            {
                return (DateTime.Now.Year - AnulFabricatiei) * ConstanteMasini.FACTOR_PA_AUTOBUZ + taxaDiesel + taxaKilometri1 + taxaKilometri2;
            } else
            {
                return ((DateTime.Now.Year - AnulFabricatiei) * ConstanteMasini.FACTOR_PA_AUTOBUZ + taxaDiesel + taxaKilometri1 + taxaKilometri2)
                    * ConstanteMasini.DISCOUNT_AUTOBUZ / 100;
            }
        }
    }
}
