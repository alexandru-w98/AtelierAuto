using AtelierAuto.DTO;
using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Atelier;
using AtelierAuto.Modele.Masini;
using AtelierAuto.Modele.Raspunsuri;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtelierAuto.Servicii.Atelier
{
    public interface IServiciuAtelier
    {
        RaspunsServiciu<ComandaAtelier> AdaugaComandaAtelier(Masina masina, int idAngajat = -1);
        RaspunsServiciu<ComandaAtelierDTO> AdaugaComandaLaCoada(Masina masina, int idAngajat);
        RaspunsServiciu<ComandaAtelier> StergeComandaAtelier(ComandaAtelier comanda);
        bool PoateIntraInAtelier(ComandaAtelierDTO comandaAtelierDTO);
        RaspunsServiciu<ComandaAtelierDTO> StergeComandaDinCoada(ComandaAtelierDTO comandaAtelierDTO);
        void VerificaReparatiiTerminate();
        RaspunsServiciu<ComandaAtelier> EsteAngajatLiber(int idAngajat);
        bool EsteAtelierLiber();
        IEnumerable<Angajat> ObtineAngajatiDisponibili();
        string AfiseazaAngajatiDisponibili();
        bool VerificaCapacitateAtelier(Masina masina);
        RaspunsServiciu<Angajat> ObtinePrimulAngajatLiber();
        void AfiseazaCapacitate();
        string AfiseazaCoada();
        string AfiseazaComenziAtelier();
        RaspunsServiciu<StatisticaAngajat> ModificaStatistica(int idAngajat, string numeCamp, double valoare);
        RaspunsServiciu<StatisticaAngajat> ObtineStatisticaDupaId(int idAngajat);
        RaspunsServiciu<Angajat> ObtineCelMaiMuncitorAngajat();
        string AfiseazaBacsisulAngajatilor();
        string ObtineCeiMaiSolicitatiAngajati();
        string ObtineAngajatiCareAuReparatCeleMaiMulteAutobuzeNoi();
        string ObtineAngajatiCeAuReparatMasiniEgalaCuPolitaMaxima();
    }
}
