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
using System.Threading.Tasks;

namespace AtelierAuto.Servicii.Atelier
{
    public class ServiciuAtelier : IServiciuAtelier
    {
        public List<ComandaAtelier> _comenziAtelier { get; set; }
        private List<ComandaAtelierDTO> _coadaComenzi { get; set; }
        private IServiciuAngajati _serviciuAngajati;
        private Dictionary<string, int> _capacitate;
        private List<StatisticaAngajat> _statisticiAngajati;

        public bool EsteAtelierDeschis
        {
            get => _serviciuAngajati.ObtineTotiAngajatii().Continut.Count() > 0;
        }

        public ServiciuAtelier()
        {
            _comenziAtelier = new List<ComandaAtelier>();
            _coadaComenzi = new List<ComandaAtelierDTO>();
            _capacitate = new Dictionary<string, int>();
            _statisticiAngajati = new List<StatisticaAngajat>();
            _serviciuAngajati = ServiciuDependente.Get<IServiciuAngajati>();

            Initializare();
        }

        public void Initializare()
        {
            _capacitate.Add(ConstanteMasini.TipMasina.Autobuz.ToString(), 0);
            _capacitate.Add(ConstanteMasini.TipMasina.Camion.ToString(), 0);
            _capacitate.Add(ConstanteMasini.TipMasina.MasinaStandard.ToString(), 0);

            var rasp = _serviciuAngajati.ObtineTotiAngajatii();
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

            ModificaStatistica(angajat.Id, ConstanteAtelier.MASINI_REPARATE, 1);
            ModificaStatistica(angajat.Id, ConstanteAtelier.BACSIS, masina.CalculeazaPolitaAsigurare(true) * 1 / 100);
            ModificaStatistica(angajat.Id, ConstanteAtelier.COST_POLITE_MASINI_REPARATE, masina.CalculeazaPolitaAsigurare());

            if (masina.GetType().Name == ConstanteMasini.TipMasina.Autobuz.ToString() &&
                DateTime.Now.Year -  masina.AnulFabricatiei < 5)
            {
                ModificaStatistica(angajat.Id, ConstanteAtelier.AUTOBUZE_NOI_REPARATE, 1);
            }

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
            var angajatInCoada = _coadaComenzi.FirstOrDefault(c => c.Angajat.Id == idAngajat);

            if (comanda != null || angajatInCoada != null)
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

        public string AfiseazaCoada()
        {
            string afisare = "";
            for (int i = 0; i < _coadaComenzi.Count; i++)
            {
                afisare += _coadaComenzi[i].Masina.GetType().Name + " asteapta la angajat cu id " + _coadaComenzi[i].Angajat.Id;
                if (i != _coadaComenzi.Count - 1)
                {
                    afisare += ", ";
                }
            }

            return afisare;
        }

        public bool VerificaCapacitateAtelier(Masina masina)
        {
            if (masina == null)
            {
                return false;
            }

            return _capacitate[masina.GetType().Name] < ConstanteAtelier.CAPACITATE_ATELIER[masina.GetType().Name];
        }

        public string AfiseazaComenziAtelier()
        {
            string afisare = "";
            for (int i = 0; i < _comenziAtelier.Count; i++)
            {
                afisare += "Angajat cu id " + _comenziAtelier[i].Angajat.Id + " lucreaza la " + _comenziAtelier[i].Masina.GetType().Name;
                if (i != _comenziAtelier.Count - 1)
                {
                    afisare += ", ";
                }
            }

            return afisare;
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

            var angajatiOcupati = _comenziAtelier.Select(c => c.Angajat).Concat(_coadaComenzi.Select(c => c.Angajat)).ToList();

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

        public RaspunsServiciu<ComandaAtelierDTO> StergeComandaDinCoada(ComandaAtelierDTO comandaAtelierDTO)
        {
            if (_coadaComenzi.Count() != 0)
            {
                if (_coadaComenzi.Remove(comandaAtelierDTO))
                {
                    return new RaspunsServiciu<ComandaAtelierDTO>
                    {
                        Continut = comandaAtelierDTO,
                        Succes = true,
                        Mesaj = ConstanteMesaje.COMANDA_STEARSA_DIN_COADA
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
            } else
            {
                return new RaspunsServiciu<ComandaAtelierDTO>
                {
                    Continut = null,
                    Succes = false,
                    Mesaj = ConstanteMesaje.COADA_ESTE_GOALA
                };
            }
        }

        public void VerificaReparatiiTerminate()
        {
            foreach (var comanda in _comenziAtelier)
            {
                if (comanda.DataPlecarii <= DateTime.Now)
                {
                    var raspunsStergere = StergeComandaAtelier(comanda);
                    if (raspunsStergere.Succes)
                    {
                        _capacitate[comanda.Masina.GetType().Name]--;
                    }
                    break;
                }
            }

            if (EsteAtelierLiber())
            {
                foreach (var comandaInCoada in _coadaComenzi)
                {
                    if (PoateIntraInAtelier(comandaInCoada))
                    {
                        StergeComandaDinCoada(comandaInCoada);
                        AdaugaComandaAtelier(comandaInCoada.Masina, comandaInCoada.Angajat.Id);
                        break;
                    }
                }
            }
        }

        public RaspunsServiciu<ComandaAtelier> StergeComandaAtelier(ComandaAtelier comanda)
        {
            if (_comenziAtelier.Remove(comanda))
            {
                return new RaspunsServiciu<ComandaAtelier>
                {
                    Continut = comanda,
                    Succes = true,
                    Mesaj = ConstanteMesaje.COMANDA_ATELIER_STEARSA
                };
            } else
            {
                return new RaspunsServiciu<ComandaAtelier>
                {
                    Continut = comanda,
                    Succes = false,
                    Mesaj = ConstanteMesaje.INFORMATII_INVALIDE
                };
            }
        }

        public bool PoateIntraInAtelier(ComandaAtelierDTO comandaAtelierDTO)
        {
            foreach(var comanda in _comenziAtelier)
            {
                if (comanda.Angajat.Id == comandaAtelierDTO.Angajat.Id)
                {
                    return false;
                }
            }

            if (VerificaCapacitateAtelier(comandaAtelierDTO.Masina))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool EsteAtelierLiber()
        {
            if (_comenziAtelier.Count() < (ConstanteAtelier.CAPACITATE_AUTOBUZ + ConstanteAtelier.CAPACITATE_CAMION + ConstanteAtelier.CAPACITATE_STANDARD))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public RaspunsServiciu<StatisticaAngajat> ModificaStatistica(int idAngajat, string numeCamp, double valoare)
        {
            if (_serviciuAngajati.CautaAngajatDupaId(idAngajat).Succes)
            {
                if (_statisticiAngajati.FirstOrDefault(s => s.Id == idAngajat) == null)
                {
                    _statisticiAngajati.Add(new StatisticaAngajat { Id = idAngajat });
                }
            } else
            {
                return new RaspunsServiciu<StatisticaAngajat>
                {
                    Continut = null,
                    Succes = false,
                    Mesaj = ConstanteMesaje.ID_INVALID
                };
            }

            foreach(var statistica in _statisticiAngajati)
            {
                if (statistica.Id == idAngajat)
                {
                    if (statistica.GetType().GetProperties().FirstOrDefault(p => p.Name == numeCamp) != null)
                    {
                        statistica.GetType().GetProperty(numeCamp).SetValue(statistica, valoare);

                        return new RaspunsServiciu<StatisticaAngajat>
                        {
                            Continut = statistica,
                            Succes = true,
                            Mesaj = ConstanteMesaje.STATISTICA_MODIFICATA
                        };
                    } else
                    {
                        return new RaspunsServiciu<StatisticaAngajat>
                        {
                            Continut = null,
                            Succes = false,
                            Mesaj = ConstanteMesaje.PROPRIETATE_INEXISTENTA
                        };
                    }
                }
            }

            return new RaspunsServiciu<StatisticaAngajat>
            {
                Continut = null,
                Succes = false,
                Mesaj = ConstanteMesaje.STATISTICA_INEXISTENTA
            };
        }

        public RaspunsServiciu<StatisticaAngajat> ObtineStatisticaDupaId(int idAngajat)
        {
            var statisticaGasita = _statisticiAngajati.FirstOrDefault(s => s.Id == idAngajat);
            if (statisticaGasita == null)
            {
                return new RaspunsServiciu<StatisticaAngajat>
                {
                    Continut = null,
                    Succes = false,
                    Mesaj = ConstanteMesaje.ID_INVALID
                };
            } else
            {
                return new RaspunsServiciu<StatisticaAngajat>
                {
                    Continut = statisticaGasita,
                    Succes = true,
                    Mesaj = ConstanteMesaje.SUCCES
                };
            }
        }

        public RaspunsServiciu<Angajat> ObtineCelMaiMuncitorAngajat()
        {
            var maxim = -1;
            int idAngajat = -1;

            foreach(var statistica in _statisticiAngajati)
            {
                if (statistica.MasiniReparate > maxim)
                {
                    maxim = (int)statistica.MasiniReparate;
                    idAngajat = statistica.Id;
                }
            }

            if (maxim != -1)
            {
                var raspuns = _serviciuAngajati.CautaAngajatDupaId(idAngajat);
                return new RaspunsServiciu<Angajat>
                {
                    Continut = raspuns.Continut,
                    Succes = raspuns.Succes,
                    Mesaj = raspuns.Mesaj
                };
            } else
            {
                return new RaspunsServiciu<Angajat>
                {
                    Continut = null,
                    Succes = false,
                    Mesaj = ConstanteMesaje.STATISTICA_INEXISTENTA
                };
            }
        }

        public string AfiseazaBacsisulAngajatilor()
        {
            var afisare = "";

            for (int i = 0; i < _statisticiAngajati.Count; i++)
            {
                afisare += "Angajatul cu id " + _statisticiAngajati[i].Id + " a strans " + _statisticiAngajati[i].Bacsis +" lei";
                if (i != _statisticiAngajati.Count - 1)
                {
                    afisare += ", ";
                }
            }

            return afisare;
        }

        public string ObtineCeiMaiSolicitatiAngajati()
        {
            var solicitati = _statisticiAngajati.OrderByDescending(s => s.NrSolicitari).Take(3).ToList();

            var afisare = "";

            for(int i = 0; i < solicitati.Count; i++)
            {
                var angajat = _serviciuAngajati.CautaAngajatDupaId(solicitati[i].Id);
                afisare += angajat.Continut.Nume + " " + angajat.Continut.Prenume + " are " + solicitati[i].NrSolicitari + " solicitari";
                if (i < solicitati.Count - 1 && solicitati[i] != null)
                {
                    afisare += ", ";
                }
            }

            return afisare;
        }

        public string ObtineAngajatiCareAuReparatCeleMaiMulteAutobuzeNoi()
        {
            var solicitati = _statisticiAngajati.OrderByDescending(s => s.AutobuzeNoiReparate).Take(3).ToList();

            var afisare = "";

            for (int i = 0; i < solicitati.Count; i++)
            {
                afisare += "Angajat cu id " + solicitati[i].Id + " a reparat " + solicitati[i].AutobuzeNoiReparate + " autobuze noi";
                if (i < solicitati.Count - 1 && solicitati[i] != null)
                {
                    afisare += ", ";
                }
            }

            return afisare;
        }

        public string ObtineAngajatiCeAuReparatMasiniEgalaCuPolitaMaxima()
        {
            var solicitati = _statisticiAngajati.OrderByDescending(s => s.CostPolitePentruMasiniReparate).Take(3).ToList();

            var afisare = "";

            for (int i = 0; i < solicitati.Count; i++)
            {
                afisare += "Angajat cu id " + solicitati[i].Id + " au strans " + solicitati[i].CostPolitePentruMasiniReparate + " cost polite pentru masinile reparate!";
                if (i < solicitati.Count - 1 && solicitati[i] != null)
                {
                    afisare += ", ";
                }
            }

            return afisare;
        }
    }
}
