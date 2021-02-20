using AtelierAuto.Constante;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Modele.Masini
{
    public class Camion : Masina
    {
        public ConstanteMasini.Tonaj Tonaj { get; set; }

        public override double CalculeazaPolitaAsigurare(bool discount = false)
        {
            int taxaKilometri = NrKilometri > ConstanteMasini.LIMITA_KILOMETRI_TAXA_CAMION ? ConstanteMasini.TAXA_KILOMETRI_CAMION : 0;

            if (!discount)
            {
                return (DateTime.Now.Year - AnulFabricatiei) * ConstanteMasini.FACTOR_PA_CAMION + taxaKilometri;
            } else
            {
                return ((DateTime.Now.Year - AnulFabricatiei) * ConstanteMasini.FACTOR_PA_CAMION + taxaKilometri)
                    * ConstanteMasini.DISCOUNT_CAMION / 100;
            }
        }
    }
}
