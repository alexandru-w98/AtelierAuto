using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Masini;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.DTO
{
    public class ComandaAtelierDTO
    {
        public Angajat Angajat { get; set; }
        public Masina Masina { get; set; }
    }
}
