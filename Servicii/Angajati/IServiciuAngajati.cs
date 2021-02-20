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
        void AfiseazaAngajati();
        RaspunsServiciu<Angajat> ValideazaAngajat(Angajat angajat);
    }
}
