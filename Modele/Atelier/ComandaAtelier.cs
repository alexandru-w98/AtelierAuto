using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Masini;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Modele.Atelier
{
    public class ComandaAtelier
    {
        public Angajat Angajat { get; set; }
        public Masina Masina { get; set; }
        public DateTime DataSosirii { get; set; }
        public DateTime DataPlecarii { get; set; }
    }
}
