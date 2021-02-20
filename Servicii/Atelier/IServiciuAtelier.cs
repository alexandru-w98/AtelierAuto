using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Atelier;
using AtelierAuto.Modele.Masini;
using AtelierAuto.Modele.Raspunsuri;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Servicii.Atelier
{
    public interface IServiciuAtelier
    {
        RaspunsServiciu<ComandaAtelier> AdaugaLaCoada<T>(T masina, int idAngajat) where T : Masina;
        bool EsteLocInAtelier<T>(T masina) where T : Masina;
        void AfiseazaCapacitate();
    }
}
