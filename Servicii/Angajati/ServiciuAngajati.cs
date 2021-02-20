using AtelierAuto.Constante;
using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Raspunsuri;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Servicii.Angajati
{
    public class ServiciuAngajati : IServiciuAngajati
    {
        public List<Angajat> _angajati { get; set; }
        private int _id;

        public ServiciuAngajati()
        {
            _angajati = new List<Angajat>();

            Initializare();
        }

        private void Initializare()
        {
            _id = ConstanteAngajati.START_ID;
        }

        public RaspunsServiciu<Angajat> AdaugaAngajat(Angajat angajat)
        {
            var raspunsValidare = ValideazaAngajat(angajat);

            angajat.ID = _id++;

            if (raspunsValidare.Succes)
            {
                _angajati.Add(angajat);

                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajat,
                    Succes = true,
                    Mesaj = ConstanteMesaje.ANGAJAT_ADAUGAT
                };
            } else
            {
                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajat,
                    Succes = false,
                    Mesaj = raspunsValidare.Mesaj
                };
            }
        }

        public RaspunsServiciu<Angajat> ValideazaAngajat(Angajat angajat)
        {
            var validare = true;

            if (validare)
            {
                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajat,
                    Succes = true,
                    Mesaj = ConstanteMesaje.ANGAJAT_VALID
                };
            } else
            {
                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajat,
                    Succes = false,
                    Mesaj = ConstanteMesaje.ANGAJAT_INVALID
                };
            }
        }

        public void AfiseazaAngajati()
        {
            foreach(var angajat in _angajati)
            {
                Console.WriteLine(angajat.Nume);
            }
        }
    }
}
