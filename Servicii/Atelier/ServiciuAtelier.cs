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
            get => _serviciuAngajati.ObtineTotiAngajatii().Continut.Count() > 0;
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

        public RaspunsServiciu<ComandaAtelier> AdaugaComandaAtelier(Masina masina, int idAngajat = -1)
        {
            Angajat angajat;
            if (idAngajat == -1)
            {
                var raspuns = ObtinePrimulAngajatLiber();
                angajat = raspuns.Continut;

                if (angajat == null)
                {
                    return new RaspunsServiciu<ComandaAtelier>
                    {
                        Continut = null,
                        Succes = false,
                        Mesaj = raspuns.Mesaj
                    };
                }
            } else
            {
                var raspunsAngajat = _serviciuAngajati.CautaAngajatDupaId(idAngajat);

                if (!raspunsAngajat.Succes)
                {
                    return new RaspunsServiciu<ComandaAtelier>
                    {
                        Continut = null,
                        Succes = false,
                        Mesaj = ConstanteMesaje.ID_INVALID
                    };
                }

                angajat = raspunsAngajat.Continut;
            }

            if (!VerificaCapacitateAtelier(masina))
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
                Angajat = angajat,
                Masina = masina,
                DataSosirii = DateTime.Now,
                DataPlecarii = DateTime.Now.AddSeconds(ConstanteAtelier.TIMP_REPARARE[masina.GetType().Name])
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

            if (raspunsCautareAngajat.Continut.GetType().Name == ConstanteAngajati.TipAngajat.Director.ToString())
            {
                return new RaspunsServiciu<ComandaAtelier>
                {
                    Continut = null,
                    Succes = false,
                    Mesaj = ConstanteMesaje.DIRECTOR_OCUPAT
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

            if (masina != null)
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
                    Mesaj = ConstanteMesaje.MASINA_INVALIDA
                };
            }
        }

        public void AfiseazaCoada()
        {
            foreach (var item in _coadaComenzi)
            {
                Console.WriteLine(item.Angajat.Id + " " + item.Masina.GetType().Name);
            }
        }

        public bool VerificaCapacitateAtelier(Masina masina)
        {
            if (masina == null)
            {
                return false;
            }

            return _capacitate[masina.GetType().Name] < ConstanteAtelier.CAPACITATE_ATELIER[masina.GetType().Name];
        }

        public void AfiseazaComenziAtelier()
        {
            foreach (var item in _comenziAtelier)
            {
                Console.WriteLine(item.Angajat.Id + " " + item.Masina.GetType().Name);
            }
        }

        public RaspunsServiciu<Angajat> ObtinePrimulAngajatLiber()
        {
            var angajatiOcupati = _comenziAtelier.Select(c => c.Angajat);

            Angajat primulLiber = null;

            foreach (var angajat in _serviciuAngajati.ObtineTotiAngajatii().Continut)
            {
                if (!angajatiOcupati.Contains(angajat) && angajat.GetType().Name != ConstanteAngajati.TipAngajat.Director.ToString())
                {
                    primulLiber = angajat;
                    break;
                }
            }

            if (primulLiber == null)
            {
                if (angajatiOcupati == null)
                {
                    return new RaspunsServiciu<Angajat>
                    {
                        Continut = null,
                        Succes = false,
                        Mesaj = ConstanteMesaje.ANGAJATI_INEXISTENTI
                    };
                } else
                {
                    DateTime celMaiDevreme = _comenziAtelier[0].DataPlecarii;
                    for (int i = 1; i < _comenziAtelier.Count; i++)
                    {
                        if (_comenziAtelier[i].DataPlecarii < celMaiDevreme)
                        {
                            celMaiDevreme = _comenziAtelier[i].DataPlecarii;
                            primulLiber = _comenziAtelier[i].Angajat;
                        }
                    }
                }
            }

            return new RaspunsServiciu<Angajat>
            {
                Continut = primulLiber,
                Succes = true,
                Mesaj = ConstanteMesaje.ANGAJAT_GASIT
            };
        }

        public IEnumerable<Angajat> ObtineAngajatiDisponibili()
        {
            var angajatiDisponibili = new List<Angajat>();

            var angajatiOcupati = _comenziAtelier.Select(c => c.Angajat);

            foreach (var angajat in _serviciuAngajati.ObtineTotiAngajatii().Continut)
            {
                if (!angajatiOcupati.Contains(angajat) && angajat.GetType().Name != ConstanteAngajati.TipAngajat.Director.ToString())
                {
                    angajatiDisponibili.Add(angajat);
                }
            }

            return angajatiDisponibili;
        }

        public string AfiseazaAngajatiDisponibili()
        {
            var angajati = ObtineAngajatiDisponibili();

            string afisare = "";

            for (int i = 0; i < angajati.Count(); i++)
            {
                afisare += angajati.ElementAt(i).Id + " " + angajati.ElementAt(i).Nume;
                if (i != angajati.Count() - 1)
                {
                    afisare += ", ";
                }
            }

            return afisare;
        }
    }
}
