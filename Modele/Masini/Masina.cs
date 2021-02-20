using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Modele.Masini
{
    public abstract class Masina
    {
        public int Id { get; set; }
        public int NrKilometri { get; set; }
        public int AnulFabricatiei { get; set; }
        public bool EsteMotorDiesel { get; set; }

        public abstract double CalculeazaPolitaAsigurare(bool discount = false);
    }
}
