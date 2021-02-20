using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Masini;
using AtelierAuto.Servicii;
using AtelierAuto.Servicii.Angajati;
using AtelierAuto.Servicii.Atelier;
using AtelierAuto.Servicii.Masini;
using System;

namespace AtelierAuto
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiciuDependente.Register<IServiciuAngajati>(new ServiciuAngajati());
            ServiciuDependente.Register<IServiciuAtelier>(new ServiciuAtelier());
            ServiciuDependente.Register<IServiciuMasini>(new ServiciuMasini());

            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Mecanic { Nume = "alex1", DataAngajarii = new DateTime(2019, 2, 20) });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Asistent { Nume = "alex2", DataAngajarii = new DateTime(2019, 2, 20) });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex3" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex4" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex5" });

            var _serviciuAtelier = ServiciuDependente.Get<IServiciuAtelier>();
            var _serviciuAngajati = ServiciuDependente.Get<IServiciuAngajati>();

            //ServiciuDependente.Get<IServiciuAtelier>().AfiseazaCoada();
            //ServiciuDependente.Get<IServiciuAtelier>().AdaugaComandaLaCoada(new Autobuz(), 2);
            //ServiciuDependente.Get<IServiciuAtelier>().AfiseazaCoada();

            //Console.WriteLine(ServiciuDependente.Get<IServiciuMasini>().ObtineMasina().Id);
            //Console.WriteLine(ServiciuDependente.Get<IServiciuMasini>().ObtineMasina().Id);
            //Console.WriteLine(ServiciuDependente.Get<IServiciuMasini>().ObtineMasina().Id);

            while (true)
            {
                var masina = ServiciuDependente.Get<IServiciuMasini>().ObtineMasina();
                if (masina == null)
                {
                    break;
                }
                Console.WriteLine("Doriti sa fiti alocat primului angajat liber = 1");
                Console.WriteLine("Doriti un anumit angajat = 2");
                var optiune = Console.ReadKey(true).KeyChar;
                switch (optiune)
                {
                    case '1':
                        Console.WriteLine("salut");
                        break;
                    case '2':
                        _serviciuAngajati.AfiseazaAngajati();
                        Console.WriteLine("Introduceti id-ul pe care il preferati");
                        var optiune2 = Int32.Parse(Console.ReadKey(true).KeyChar.ToString());
                        var raspuns = _serviciuAtelier.EsteAngajatLiber(optiune2);
                        if (raspuns.Succes)
                        {
                            _serviciuAtelier.AdaugaComandaAtelier(masina, optiune2);
                        }
                        else
                        {
                            Console.WriteLine("Angajatul nu este disponibil");
                            Console.WriteLine("Doriti sa plecati = 1 sau asteptati primul angajat liber? = 2");
                            var optiune3 = Int32.Parse(Console.ReadKey(true).KeyChar.ToString());
                            switch(optiune3)
                            {
                                case 1:
                                    break;
                                case 2:
                                    if (_serviciuAtelier.VerificaCapacitateAtelier(masina))
                                    {
                                        _serviciuAtelier.AdaugaComandaAtelier(masina);
                                    }
                                    break;
                                    
                            }
                        }
                        _serviciuAtelier.AfiseazaComenziAtelier();
                        break;
                    default:
                        Console.WriteLine("default");
                        break;
                }
            }
        }
    }
}
