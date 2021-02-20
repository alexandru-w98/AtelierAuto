using AtelierAuto.Constante;
using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Atelier;
using AtelierAuto.Modele.Masini;
using AtelierAuto.Modele.Raspunsuri;
using AtelierAuto.Servicii.Angajati;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Servicii.Atelier
{
    public class ServiciuAtelier : IServiciuAtelier
    {
        public List<ComandaAtelier> _coada { get; set; }
        private IServiciuAngajati _serviciuAngajati;
        private Dictionary<string, int> _capacitate;

        public bool EsteAtelierDeschis
        {
            get => _serviciuAngajati.ExistaAngajati();
        }

        public ServiciuAtelier()
        {
            _coada = new List<ComandaAtelier>();
            _capacitate = new Dictionary<string, int>();
            _serviciuAngajati = ServiciuDependente.Get<IServiciuAngajati>();

            Initializare();
        }

        public void Initializare()
        {
            _capacitate.Add(ConstanteMasini.TipMasina.Autobuz.ToString(), 0);
            _capacitate.Add(ConstanteMasini.TipMasina.Camion.ToString(), 0);
            _capacitate.Add(ConstanteMasini.TipMasina.MasinaStandard.ToString(), 0);
        }

        public RaspunsServiciu<ComandaAtelier> AdaugaLaCoada<T>(T masina, int idAngajat) where T : Masina
        {
            var raspunsCautareAngajat = _serviciuAngajati.CautaAngajatDupaId(idAngajat);

            if (!raspunsCautareAngajat.Succes)
            {
                return new RaspunsServiciu<ComandaAtelier>
                {
                    Continut = null,
                    Succes = false,
                    Mesaj = ConstanteMesaje.ID_INVALID
                };
            }

            if (_capacitate[masina.GetType().Name] >= ConstanteAtelier.CAPACITATE_ATELIER[masina.GetType().Name])
            {
                Console.WriteLine(ConstanteAtelier.CAPACITATE_ATELIER[masina.GetType().Name]);
                Console.WriteLine(ConstanteMesaje.ATELIER_PLIN);
            }

            var comandaAtelier = new ComandaAtelier
            {
                Angajat = raspunsCautareAngajat.Continut,
                Masina = masina,
                DataSosirii = DateTime.Now,
                DataPlecarii = DateTime.Now
            };

            _coada.Add(comandaAtelier);
            _capacitate[masina.GetType().Name]++;

            return new RaspunsServiciu<ComandaAtelier>
            {
                Continut = comandaAtelier,
                Succes = true,
                Mesaj = ConstanteMesaje.COMANDA_ATELIER_ADAUGATA
            };
        }

        public bool EsteLocInAtelier<T>(T masina) where T : Masina
        {
            return true;
        }

        public void AfiseazaCapacitate()
        {
            foreach(var item in _capacitate)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
        }
    }
}
