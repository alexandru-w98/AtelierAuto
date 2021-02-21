using AtelierAuto.Constante;
using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Masini;
using AtelierAuto.Servicii;
using AtelierAuto.Servicii.Angajati;
using AtelierAuto.Servicii.Atelier;
using AtelierAuto.Servicii.Masini;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Asistent { Nume = "alex3" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Asistent { Nume = "alex4" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Asistent { Nume = "alex5" });

            var _serviciuAtelier = ServiciuDependente.Get<IServiciuAtelier>();
            var _serviciuAngajati = ServiciuDependente.Get<IServiciuAngajati>();
            var _serviciuMasini = ServiciuDependente.Get<IServiciuMasini>();

            //_serviciuAtelier.AdaugaComandaLaCoada(new Autobuz(), 3);
            //_serviciuAtelier.AdaugaComandaAtelier(new Autobuz(), 1);
            //_serviciuAtelier.AdaugaComandaAtelier(new Autobuz(), 2);
            //_serviciuAtelier.AdaugaComandaAtelier(new Autobuz(), 3);
            //_serviciuAtelier.AdaugaComandaAtelier(new Autobuz(), 4);
            //_serviciuAtelier.AdaugaComandaAtelier(new Autobuz(), 5);

            Thread VerificareAtelier = new Thread(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    _serviciuAtelier.VerificaReparatiiTerminate();

                    //Console.WriteLine();
                    //Console.WriteLine("atelier");
                    //Console.WriteLine(_serviciuAtelier.AfiseazaComenziAtelier());
                    //Console.WriteLine("coada");
                    //Console.WriteLine(_serviciuAtelier.AfiseazaCoada());
                }
            });

            VerificareAtelier.Start();

            while (true)
            {

            }

            Console.SetWindowSize(ConstanteGenerale.LatimeConsola, ConstanteGenerale.InaltimeConsola);

            //while (true)
            //{
            //    Console.WriteLine("*Rapoarte angajati = Tasta 1*");
            //    Console.WriteLine("*Deschide Atelierul = Tasta 2*");
            //    var optiuneMeniu = Console.ReadKey(true).KeyChar;
            //    switch (optiuneMeniu) 
            //    {
            //        case '1':
            //            Console.Clear();
            //            Console.WriteLine("*Angajatul cu cea mai mare incarcare in data curenta = Tasta 1*");
            //            Console.WriteLine("*Top 3 angajati care au reparat masini ce insumeaza valoarea politei de asigurare maxima = Tasta 2*");
            //            Console.WriteLine("*Top 3 angajati care au reparat cele mai multe autobuze noi = Tasta 3*");
            //            Console.WriteLine("*Numele si prenumele celor mai solicitati angajati = Tasta 4*");
            //            Console.WriteLine("*Bacsisul fiecărui angajat = Tasta 5*");
            //            var optiune10 = Console.ReadKey(true).KeyChar;
            //            switch(optiune10)
            //            {
            //                case '1':
                                
            //                    break;
            //            }
            //            break;
            //        case '2':
            //            var masina = _serviciuMasini.ObtineMasina();
            //            if (masina == null)
            //            {
            //                Console.WriteLine("S-au terminat masinile ^_^");
            //                return;
            //            }
            //            Console.Clear();
            //            Console.WriteLine("Lista comenzilor din atelier: ");
            //            Console.WriteLine(_serviciuAtelier.AfiseazaComenziAtelier());
            //            Console.WriteLine("La coada sunt: ");
            //            Console.WriteLine(_serviciuAtelier.AfiseazaCoada());
            //            Console.WriteLine();
            //            Console.WriteLine("*A venit " + masina.GetType().Name + " cu id " + masina.Id + " *");
            //            Console.WriteLine("Doriti sa fiti alocat primului angajat liber = Tasta 1");
            //            Console.WriteLine("Doriti un anumit angajat = Tasta 2");
            //            var optiune = Console.ReadKey(true).KeyChar;
            //            switch (optiune)
            //            {
            //                case '1':
            //                    if (_serviciuAtelier.EsteAtelierLiber())
            //                    {
            //                        if (_serviciuAtelier.VerificaCapacitateAtelier(masina))
            //                        {
            //                            _serviciuAtelier.AdaugaComandaAtelier(masina, _serviciuAtelier.ObtinePrimulAngajatLiber().Continut.Id);
            //                            Console.WriteLine("Ati fost repartizat!");
            //                            Thread.Sleep(2000);
            //                        }
            //                        else
            //                        {
            //                            Console.WriteLine("Nu mai este loc in atelier pentru masini de acest tip!");
            //                            Console.WriteLine("Doriti sa asteptati pana se elibereaza un loc? = Tasta 1");
            //                            Console.WriteLine("Doriti sa plecati? = Tasta 2");
            //                            var optiune4 = Console.ReadKey(true).KeyChar;
            //                            switch (optiune4)
            //                            {
            //                                case '1':
            //                                    _serviciuAtelier.AdaugaComandaLaCoada(masina, _serviciuAtelier.ObtinePrimulAngajatLiber().Continut.Id);
            //                                    Console.WriteLine("Ati fost adaugat la coada!");
            //                                    Thread.Sleep(2000);
            //                                    break;
            //                                case '2':
            //                                    Console.WriteLine("Va dorim o zi buna!");
            //                                    Thread.Sleep(2000);
            //                                    break;
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        Console.WriteLine("Momentan atelierul este plin!");
            //                        Console.WriteLine("Doriti sa asteptati pana se elibereaza un loc? = Tasta 1");
            //                        Console.WriteLine("Doriti sa plecati? = Tasta 2");
            //                        var optiune6 = Console.ReadKey(true).KeyChar;
            //                        switch (optiune6)
            //                        {
            //                            case '1':
            //                                _serviciuAtelier.AdaugaComandaLaCoada(masina, _serviciuAtelier.ObtinePrimulAngajatLiber().Continut.Id);
            //                                Console.WriteLine("Ati fost adaugat la coada!");
            //                                Thread.Sleep(2000);
            //                                break;
            //                            case '2':
            //                                Console.WriteLine("Va dorim o zi buna!");
            //                                Thread.Sleep(2000);
            //                                break;
            //                        }
            //                    }
            //                    break;
            //                case '2':
            //                    Console.WriteLine("Lista angajatilor disponibili: ");
            //                    Console.WriteLine(_serviciuAtelier.AfiseazaAngajatiDisponibili());
            //                    Console.WriteLine("Introduceti id-ul angajatului pe care il preferati: ");
            //                    var optiune2 = Int32.Parse(Console.ReadKey(true).KeyChar.ToString());
            //                    var raspuns = _serviciuAtelier.EsteAngajatLiber(optiune2);
            //                    if (raspuns.Succes)
            //                    {
            //                        if (_serviciuAtelier.VerificaCapacitateAtelier(masina))
            //                        {
            //                            _serviciuAtelier.AdaugaComandaAtelier(masina, optiune2);
            //                            Console.WriteLine("Ati fost repartizat!");
            //                            Thread.Sleep(2000);
            //                        }
            //                        else
            //                        {
            //                            Console.WriteLine("Nu mai este loc in atelier pentru masini de acest tip!");
            //                            Console.WriteLine("Doriti sa asteptati pana se elibereaza un loc? = Tasta 1");
            //                            Console.WriteLine("Doriti sa plecati? = Tasta 2");
            //                            var optiune4 = Console.ReadKey(true).KeyChar;
            //                            switch (optiune4)
            //                            {
            //                                case '1':
            //                                    _serviciuAtelier.AdaugaComandaLaCoada(masina, _serviciuAtelier.ObtinePrimulAngajatLiber().Continut.Id);
            //                                    Console.WriteLine("Ati fost adaugat la coada!");
            //                                    Thread.Sleep(2000);
            //                                    break;
            //                                case '2':
            //                                    Console.WriteLine("Va dorim o zi buna!");
            //                                    Thread.Sleep(2000);
            //                                    break;
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        Console.Clear();
            //                        Console.WriteLine("Lista comenzilor din atelier: ");
            //                        Console.WriteLine(_serviciuAtelier.AfiseazaComenziAtelier());
            //                        Console.WriteLine("La coada sunt: ");
            //                        Console.WriteLine(_serviciuAtelier.AfiseazaCoada());
            //                        Console.WriteLine();
            //                        Console.WriteLine("Angajatul cu id " + optiune2 + " nu este disponibil");
            //                        Console.WriteLine("Doriti sa plecati? = Tasta 1");
            //                        Console.WriteLine("Doriti sa asteptati la acest angajat? = Tasta 2");
            //                        Console.WriteLine("Doriti sa asteptati primul angajat liber? = Tasta 3");
            //                        var optiune3 = Int32.Parse(Console.ReadKey(true).KeyChar.ToString());
            //                        switch (optiune3)
            //                        {
            //                            case 1:
            //                                Console.WriteLine("Va dorim o zi buna!");
            //                                Thread.Sleep(2000);
            //                                break;
            //                            case 2:
            //                                _serviciuAtelier.AdaugaComandaLaCoada(masina, optiune2);
            //                                Console.WriteLine("Ati fost adaugat la coada!");
            //                                Thread.Sleep(2000);
            //                                break;
            //                            case 3:
            //                                if (_serviciuAtelier.VerificaCapacitateAtelier(masina))
            //                                {
            //                                    _serviciuAtelier.AdaugaComandaAtelier(masina);
            //                                    Console.WriteLine("Ati fost repartizat!");
            //                                    Thread.Sleep(2000);
            //                                }
            //                                else
            //                                {
            //                                    Console.WriteLine("Nu mai este loc in atelier pentru masini de acest tip!");
            //                                    Console.WriteLine("Doriti sa asteptati pana se elibereaza un loc? = Tasta 1");
            //                                    Console.WriteLine("Doriti sa plecati? = Tasta 2");
            //                                    var optiune5 = Console.ReadKey(true).KeyChar;
            //                                    switch (optiune5)
            //                                    {
            //                                        case '1':
            //                                            _serviciuAtelier.AdaugaComandaLaCoada(masina, _serviciuAtelier.ObtinePrimulAngajatLiber().Continut.Id);
            //                                            Console.WriteLine("Ati fost adaugat la coada!");
            //                                            Thread.Sleep(2000);
            //                                            break;
            //                                        case '2':
            //                                            Console.WriteLine("Va dorim o zi buna!");
            //                                            Thread.Sleep(2000);
            //                                            break;
            //                                    }
            //                                }
            //                                break;
            //                        }
            //                    }
            //                    break;
            //                default:
            //                    Console.WriteLine("Nu ati introdus o optiune valida!");
            //                    Thread.Sleep(2000);
            //                    break;
            //            }
            //            Console.WriteLine();
            //            Console.WriteLine();
            //            Console.WriteLine();
            //            break;
            //    }
            //}
        }
    }
}
