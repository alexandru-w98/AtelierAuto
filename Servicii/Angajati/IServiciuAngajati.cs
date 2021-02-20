using AtelierAuto.Constante;
using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Raspunsuri;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Servicii.Angajati
{
    public interface IServiciuAngajati
    {
        RaspunsServiciu<Angajat> AdaugaAngajat(Angajat angajat);
        RaspunsServiciu<Angajat> StergereAngajat(int id);
        RaspunsServiciu<double> CalculeazaSalariu(int id);
        RaspunsServiciu<Angajat> CautaAngajatDupaId(int id);
        RaspunsServiciu<IEnumerable<Angajat>> ObtineTotiAngajatii();
        void AfiseazaAngajati();
        bool ExistaAngajati();
        RaspunsServiciu<Angajat> ValideazaInformatiiAngajat(Angajat angajat);
    }
}
