using AtelierAuto.DTO;
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
        RaspunsServiciu<ComandaAtelier> AdaugaComandaAtelier<T>(T masina, int idAngajat = -1) where T : Masina;
        RaspunsServiciu<ComandaAtelierDTO> AdaugaComandaLaCoada(Masina masina, int idAngajat);
        RaspunsServiciu<ComandaAtelier> EsteAngajatLiber(int idAngajat);
        bool VerificaCapacitateAtelier(Masina masina);
        RaspunsServiciu<Angajat> ObtinePrimulAngajatLiber();
        void AfiseazaCapacitate();
        void AfiseazaCoada();
        void AfiseazaComenziAtelier();
    }
}
