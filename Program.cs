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

            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "Nume1", DataAngajarii = new DateTime(2019, 2, 20),Prenume = "a" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Mecanic { Nume = "Nume2", DataAngajarii = new DateTime(2019, 2, 20), Prenume = "a" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Asistent { Nume = "Nume3", DataAngajarii = new DateTime(2019, 2, 20), Prenume = "a" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Asistent { Nume = "Nume4", DataAngajarii = new DateTime(2019, 2, 20), Prenume = "a" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Asistent { Nume = "Nume5", DataAngajarii = new DateTime(2019, 2, 20), Prenume = "a" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Asistent { Nume = "Nume6", DataAngajarii = new DateTime(2019, 2, 20), Prenume = "a" });

            var _serviciuAtelier = ServiciuDependente.Get<IServiciuAtelier>();
            var _serviciuAngajati = ServiciuDependente.Get<IServiciuAngajati>();
            var _serviciuMasini = ServiciuDependente.Get<IServiciuMasini>();

            Thread VerificareAtelier = new Thread(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    _serviciuAtelier.VerificaReparatiiTerminate();
                }
            });

            VerificareAtelier.Start();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("*Rapoarte angajati = Tasta 1*");
                Console.WriteLine("*Deschide Atelierul = Tasta 2*");
                var opt1 = Console.ReadKey(true).KeyChar;
                switch (opt1)
                {
                    case '1':
                        var finalizare1 = false;
                        while(!finalizare1)
                        {
                            Console.Clear();
                            Console.WriteLine("*Angajatul cu cea mai mare incarcare in data curenta = Tasta 1*");
                            Console.WriteLine("*Top 3 angajati care au reparat masini ce insumeaza valoarea politei de asigurare maxima = Tasta 2*");
                            Console.WriteLine("*Top 3 angajati care au reparat cele mai multe autobuze noi = Tasta 3*");
                            Console.WriteLine("*Numele si prenumele celor mai solicitati angajati = Tasta 4*");
                            Console.WriteLine("*Bacsisul fiecărui angajat = Tasta 5*");
                            Console.WriteLine("*Meniul anterior = Tasta 6*");
                            var opt2 = Console.ReadKey(true).KeyChar;
                            switch (opt2)
                            {
                                case '1':
                                    var raspuns1 = _serviciuAtelier.ObtineCelMaiMuncitorAngajat();
                                    if (raspuns1.Succes)
                                    {
                                        Console.WriteLine("Cel mai muncitor angajat are id " + raspuns1.Continut.Id);
                                        Thread.Sleep(2000);
                                    }
                                    else
                                    {
                                        Console.WriteLine(ConstanteMesaje.STATISTICA_INEXISTENTA);
                                        Thread.Sleep(2000);
                                    }
                                    break;
                                case '2':
                                    var raspuns2 = _serviciuAtelier.ObtineAngajatiCeAuReparatMasiniEgalaCuPolitaMaxima();
                                    if (string.IsNullOrEmpty(raspuns2))
                                    {
                                        Console.WriteLine(ConstanteMesaje.STATISTICA_INEXISTENTA);
                                    }
                                    else
                                    {
                                        Console.WriteLine(raspuns2);
                                    }
                                    Thread.Sleep(2000);
                                    break;
                                case '3':
                                    var raspuns3 = _serviciuAtelier.ObtineAngajatiCareAuReparatCeleMaiMulteAutobuzeNoi();
                                    if (string.IsNullOrEmpty(raspuns3))
                                    {
                                        Console.WriteLine(ConstanteMesaje.STATISTICA_INEXISTENTA);
                                    }
                                    else
                                    {
                                        Console.WriteLine(raspuns3);
                                    }
                                    Thread.Sleep(2000);
                                    break;
                                case '4':
                                    var raspuns4 = _serviciuAtelier.ObtineCeiMaiSolicitatiAngajati();
                                    if (string.IsNullOrEmpty(raspuns4))
                                    {
                                        Console.WriteLine(ConstanteMesaje.STATISTICA_INEXISTENTA);
                                    }
                                    else
                                    {
                                        Console.WriteLine(raspuns4);
                                    }
                                    Thread.Sleep(2000);
                                    break;
                                case '5':
                                    var raspuns5 = _serviciuAtelier.AfiseazaBacsisulAngajatilor();
                                    if (string.IsNullOrEmpty(raspuns5))
                                    {
                                        Console.WriteLine(ConstanteMesaje.STATISTICA_INEXISTENTA);
                                    }
                                    else
                                    {
                                        Console.WriteLine(raspuns5);
                                    }
                                    Thread.Sleep(2000);
                                    break;
                                case '6':
                                    finalizare1 = true;
                                    break;
                            }
                        }
                        break;
                    case '2':
                        var finalizare2 = false;
                        while(!finalizare2)
                        {
                            var masina = _serviciuMasini.ObtineMasina();
                            if (masina == null)
                            {
                                Console.WriteLine("S-au terminat masinile ^_^");
                                return;
                            }
                            Console.Clear();
                            Console.WriteLine("Lista comenzilor din atelier: ");
                            Console.WriteLine(_serviciuAtelier.AfiseazaComenziAtelier());
                            Console.WriteLine("La coada sunt: ");
                            Console.WriteLine(_serviciuAtelier.AfiseazaCoada());
                            Console.WriteLine();
                            Console.WriteLine("*A venit " + masina.GetType().Name + " cu id " + masina.Id + " *");
                            Console.WriteLine("Doriti sa fiti alocat primului angajat liber = Tasta 1");
                            Console.WriteLine("Doriti un anumit angajat = Tasta 2");
                            var opt3 = Console.ReadKey(true).KeyChar;
                            switch (opt3)
                            {
                                case '1':
                                    if (_serviciuAtelier.EsteAtelierLiber())
                                    {
                                        if (_serviciuAtelier.VerificaCapacitateAtelier(masina))
                                        {
                                            _serviciuAtelier.AdaugaComandaAtelier(masina, _serviciuAtelier.ObtinePrimulAngajatLiber().Continut.Id);
                                            Console.WriteLine("Ati fost repartizat!");
                                            Thread.Sleep(2000);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Nu mai este loc in atelier pentru masini de acest tip!");
                                            Console.WriteLine("Doriti sa asteptati pana se elibereaza un loc? = Tasta 1");
                                            Console.WriteLine("Doriti sa plecati? = Tasta 2");
                                            var opt4 = Console.ReadKey(true).KeyChar;
                                            switch (opt4)
                                            {
                                                case '1':
                                                    _serviciuAtelier.AdaugaComandaLaCoada(masina, _serviciuAtelier.ObtinePrimulAngajatLiber().Continut.Id);
                                                    Console.WriteLine("Ati fost adaugat la coada!");
                                                    Thread.Sleep(2000);
                                                    break;
                                                case '2':
                                                    Console.WriteLine("Va dorim o zi buna!");
                                                    Thread.Sleep(2000);
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Momentan atelierul este plin!");
                                        Console.WriteLine("Doriti sa asteptati pana se elibereaza un loc? = Tasta 1");
                                        Console.WriteLine("Doriti sa plecati? = Tasta 2");
                                        var opt5 = Console.ReadKey(true).KeyChar;
                                        switch (opt5)
                                        {
                                            case '1':
                                                _serviciuAtelier.AdaugaComandaLaCoada(masina, _serviciuAtelier.ObtinePrimulAngajatLiber().Continut.Id);
                                                Console.WriteLine("Ati fost adaugat la coada!");
                                                Thread.Sleep(2000);
                                                break;
                                            case '2':
                                                Console.WriteLine("Va dorim o zi buna!");
                                                Thread.Sleep(2000);
                                                break;
                                        }
                                    }
                                    break;
                                case '2':
                                    Console.WriteLine("Lista angajatilor disponibili: ");
                                    Console.WriteLine(_serviciuAtelier.AfiseazaAngajatiDisponibili());
                                    Console.WriteLine("Introduceti id-ul angajatului pe care il preferati: ");
                                    var opt6 = Int32.Parse(Console.ReadKey(true).KeyChar.ToString());
                                    var raspuns6 = _serviciuAtelier.EsteAngajatLiber(opt6);
                                    if (raspuns6.Succes)
                                    {
                                        if (_serviciuAtelier.VerificaCapacitateAtelier(masina))
                                        {
                                            _serviciuAtelier.AdaugaComandaAtelier(masina, opt6);
                                            _serviciuAtelier.ModificaStatistica(opt6, ConstanteAtelier.NUMAR_SOLICITARI, 1);
                                            Console.WriteLine("Ati fost repartizat!");
                                            Thread.Sleep(2000);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Nu mai este loc in atelier pentru masini de acest tip!");
                                            Console.WriteLine("Doriti sa asteptati pana se elibereaza un loc? = Tasta 1");
                                            Console.WriteLine("Doriti sa plecati? = Tasta 2");
                                            var opt7 = Console.ReadKey(true).KeyChar;
                                            switch (opt7)
                                            {
                                                case '1':
                                                    _serviciuAtelier.AdaugaComandaLaCoada(masina, opt6);
                                                    _serviciuAtelier.ModificaStatistica(opt6, ConstanteAtelier.NUMAR_SOLICITARI, 1);
                                                    Console.WriteLine("Ati fost adaugat la coada!");
                                                    Thread.Sleep(2000);
                                                    break;
                                                case '2':
                                                    Console.WriteLine("Va dorim o zi buna!");
                                                    Thread.Sleep(2000);
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Lista comenzilor din atelier: ");
                                        Console.WriteLine(_serviciuAtelier.AfiseazaComenziAtelier());
                                        Console.WriteLine("La coada sunt: ");
                                        Console.WriteLine(_serviciuAtelier.AfiseazaCoada());
                                        Console.WriteLine();
                                        Console.WriteLine("Angajatul cu id " + opt6 + " nu este disponibil");
                                        Console.WriteLine("Doriti sa plecati? = Tasta 1");
                                        Console.WriteLine("Doriti sa asteptati la acest angajat? = Tasta 2");
                                        Console.WriteLine("Doriti sa asteptati primul angajat liber? = Tasta 3");
                                        var opt8 = Int32.Parse(Console.ReadKey(true).KeyChar.ToString());
                                        switch (opt8)
                                        {
                                            case 1:
                                                Console.WriteLine("Va dorim o zi buna!");
                                                Thread.Sleep(2000);
                                                break;
                                            case 2:
                                                _serviciuAtelier.AdaugaComandaLaCoada(masina, opt6);
                                                _serviciuAtelier.ModificaStatistica(opt6, ConstanteAtelier.NUMAR_SOLICITARI, 1);
                                                Console.WriteLine("Ati fost adaugat la coada!");
                                                Thread.Sleep(2000);
                                                break;
                                            case 3:
                                                if (_serviciuAtelier.VerificaCapacitateAtelier(masina))
                                                {
                                                    _serviciuAtelier.AdaugaComandaAtelier(masina);
                                                    Console.WriteLine("Ati fost repartizat!");
                                                    Thread.Sleep(2000);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Nu mai este loc in atelier pentru masini de acest tip!");
                                                    Console.WriteLine("Doriti sa asteptati pana se elibereaza un loc? = Tasta 1");
                                                    Console.WriteLine("Doriti sa plecati? = Tasta 2");
                                                    var opt9 = Console.ReadKey(true).KeyChar;
                                                    switch (opt9)
                                                    {
                                                        case '1':
                                                            _serviciuAtelier.AdaugaComandaLaCoada(masina, _serviciuAtelier.ObtinePrimulAngajatLiber().Continut.Id);
                                                            Console.WriteLine("Ati fost adaugat la coada!");
                                                            Thread.Sleep(2000);
                                                            break;
                                                        case '2':
                                                            Console.WriteLine("Va dorim o zi buna!");
                                                            Thread.Sleep(2000);
                                                            break;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Nu ati introdus o optiune valida!");
                                    Thread.Sleep(2000);
                                    break;
                            }
                            Console.Clear();
                            Console.WriteLine("Doriti sa va intoarceti la meniul anterior? = Tasta 1");
                            Console.WriteLine("Apasa alta tasta pentru a continua!");
                            var opt10 = Console.ReadKey(true).KeyChar;
                            switch (opt10)
                            {
                                case '1':
                                    finalizare2 = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                }
            }
        }
    }
}
