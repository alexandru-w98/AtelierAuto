using AtelierAuto.Constante;
using AtelierAuto.DTO;
using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Atelier;
using AtelierAuto.Modele.Masini;
using AtelierAuto.Modele.Raspunsuri;
using AtelierAuto.Servicii.Angajati;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtelierAuto.Servicii.Atelier
{
    public class ServiciuAtelier : IServiciuAtelier
    {
        public List<ComandaAtelier> _comenziAtelier { get; set; }
        public List<ComandaAtelierDTO> _coadaComenzi { get; set; }
        private IServiciuAngajati _serviciuAngajati;
        private Dictionary<string, int> _capacitate;

        public bool EsteAtelierDeschis
        {
            get => _serviciuAngajati.ExistaAngajati();
        }

        public ServiciuAtelier()
        {
            _comenziAtelier = new List<ComandaAtelier>();
            _coadaComenzi = new List<ComandaAtelierDTO>();
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

        public RaspunsServiciu<ComandaAtelier> AdaugaComandaAtelier<T>(T masina, int idAngajat) where T : Masina
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
                return new RaspunsServiciu<ComandaAtelier>
                {
                    Continut = null,
                    Succes = false,
                    Mesaj = ConstanteMesaje.ATELIER_PLIN
                };
            }

            var comandaAtelier = new ComandaAtelier
            {
                Angajat = raspunsCautareAngajat.Continut,
                Masina = masina,
                DataSosirii = DateTime.Now,
                DataPlecarii = DateTime.Now
            };

            _comenziAtelier.Add(comandaAtelier);
            _capacitate[masina.GetType().Name]++;

            return new RaspunsServiciu<ComandaAtelier>
            {
                Continut = comandaAtelier,
                Succes = true,
                Mesaj = ConstanteMesaje.COMANDA_ATELIER_ADAUGATA
            };
        }

        public void AfiseazaCapacitate()
        {
            foreach(var item in _capacitate)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
        }

        public RaspunsServiciu<ComandaAtelier> EsteAngajatLiber(int idAngajat)
        {
            var comanda = _comenziAtelier.FirstOrDefault(comanda => comanda.Angajat.Id == idAngajat);

            if (comanda != null)
            {
                return new RaspunsServiciu<ComandaAtelier>
                {
                    Continut = comanda,
                    Succes = false,
                    Mesaj = ConstanteMesaje.ANGAJAT_OCUPAT
                };
            }

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

            return new RaspunsServiciu<ComandaAtelier>
            {
                Continut = null,
                Succes = true,
                Mesaj = ConstanteMesaje.ANGAJAT_DISPONIBIL
            };
        }

        public RaspunsServiciu<ComandaAtelierDTO> AdaugaComandaLaCoada(Masina masina, int idAngajat)
        {
            var raspunsCautareAngajat = _serviciuAngajati.CautaAngajatDupaId(idAngajat);

            if (!raspunsCautareAngajat.Succes)
            {
                return new RaspunsServiciu<ComandaAtelierDTO>
                {
                    Continut = null,
                    Succes = false,
                    Mesaj = ConstanteMesaje.ID_INVALID
                };
            }

            if (masina != null && raspunsCautareAngajat.Continut != null)
            {
                var comandaDeAdaugat = new ComandaAtelierDTO
                {
                    Angajat = raspunsCautareAngajat.Continut,
                    Masina = masina
                };

                _coadaComenzi.Add(comandaDeAdaugat);

                return new RaspunsServiciu<ComandaAtelierDTO>
                {
                    Continut = comandaDeAdaugat,
                    Succes = true,
                    Mesaj = ConstanteMesaje.COMANDA_ADAUGATA_LA_COADA
                };
            } else
            {
                return new RaspunsServiciu<ComandaAtelierDTO>
                {
                    Continut = null,
                    Succes = false,
                    Mesaj = ConstanteMesaje.INFORMATII_INVALIDE
                };
            }
        }
    }
}
