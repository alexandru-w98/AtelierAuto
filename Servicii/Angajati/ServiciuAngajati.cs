using AtelierAuto.Constante;
using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Atelier;
using AtelierAuto.Modele.Raspunsuri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtelierAuto.Servicii.Angajati
{
    public class ServiciuAngajati : IServiciuAngajati
    {
        public List<Angajat> Angajati { get; private set; }
        private int _id;

        public ServiciuAngajati()
        {
            Angajati = new List<Angajat>();

            Initializare();
        }

        private void Initializare()
        {
            _id = ConstanteAngajati.START_ID;
        }

        public RaspunsServiciu<Angajat> AdaugaAngajat(Angajat angajat)
        {
            var raspunsValidare = ValideazaInformatiiAngajat(angajat);

            angajat.Id = _id++;

            if (raspunsValidare.Succes)
            {
                Angajati.Add(angajat);

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

        public RaspunsServiciu<Angajat> ValideazaInformatiiAngajat(Angajat angajat)
        {
            if (string.IsNullOrEmpty(angajat.Nume) || angajat.Nume.Length > 30)
            {
                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajat,
                    Succes = false,
                    Mesaj = ConstanteMesaje.NUME_INVALID
                };
            }

            if (string.IsNullOrEmpty(angajat.Prenume) || angajat.Prenume.Length > 30)
            {
                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajat,
                    Succes = false,
                    Mesaj = ConstanteMesaje.PRENUME_INVALID
                };
            }

            if ((DateTime.Now.Year - angajat.DataNasterii.Year) < 18)
            {
                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajat,
                    Succes = false,
                    Mesaj = ConstanteMesaje.VARSTA_INVALIDA
                };
            }

            if (DateTime.Now < angajat.DataAngajarii || angajat.DataAngajarii == null)
            {
                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajat,
                    Succes = false,
                    Mesaj = ConstanteMesaje.DATA_ANGAJARII_INVALIDA
                };
            }

            return new RaspunsServiciu<Angajat>
            {
                Continut = angajat,
                Succes = true,
                Mesaj = ConstanteMesaje.ANGAJAT_VALID
            };
        }

        public void AfiseazaAngajati()
        {
            foreach(var angajat in Angajati)
            {
                Console.Write(angajat.Id + " ");
            }
            Console.WriteLine();
        }

        public RaspunsServiciu<Angajat> StergereAngajat(int id)
        {
            var angajatDeSters = Angajati.FirstOrDefault(a => a.Id == id);

            if (angajatDeSters != null)
            {
                Angajati.Remove(angajatDeSters);

                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajatDeSters,
                    Succes = true,
                    Mesaj = ConstanteMesaje.ANGAJAT_STERS
                };
            } else
            {
                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajatDeSters,
                    Succes = false,
                    Mesaj = ConstanteMesaje.ID_INVALID
                };
            }
        }

        public RaspunsServiciu<double> CalculeazaSalariu(int id)
        {
            var angajatCautat = Angajati.FirstOrDefault(a => a.Id == id);
            
            if (angajatCautat == null)
            {
                return new RaspunsServiciu<double>
                {
                    Continut = 0,
                    Succes = false,
                    Mesaj = ConstanteMesaje.ID_INVALID
                };
            }

            var salariu = (DateTime.Now.Year - angajatCautat.DataAngajarii.Year) * angajatCautat.CoeficientSalarial * ConstanteAngajati.FACTOR_SALARIU;

            if (salariu == 0)
            {
                salariu = ConstanteAngajati.SALARIU_MINIM;
            }

            return new RaspunsServiciu<double>
            {
                Continut = salariu,
                Succes = true,
                Mesaj = ConstanteMesaje.SUCCES
            };
        }

        public RaspunsServiciu<Angajat> CautaAngajatDupaId(int id)
        {
            var angajatCautat = Angajati.FirstOrDefault(a => a.Id == id);

            if (angajatCautat == null)
            {
                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajatCautat,
                    Succes = false,
                    Mesaj = ConstanteMesaje.ID_INVALID
                };
            } else
            {
                return new RaspunsServiciu<Angajat>
                {
                    Continut = angajatCautat,
                    Succes = true,
                    Mesaj = ConstanteMesaje.ANGAJAT_GASIT
                };
            }
        }

        public RaspunsServiciu<IEnumerable<Angajat>> ObtineTotiAngajatii()
        {
            if (Angajati.Count == 0)
            {
                return new RaspunsServiciu<IEnumerable<Angajat>>
                {
                    Continut = null,
                    Succes = false,
                    Mesaj = ConstanteMesaje.ANGAJATI_INEXISTENTI
                };
            }

            return new RaspunsServiciu<IEnumerable<Angajat>>
            {
                Continut = Angajati,
                Succes = true,
                Mesaj = ConstanteMesaje.SUCCES
            };
        }
    }
}
