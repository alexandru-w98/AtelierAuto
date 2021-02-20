using AtelierAuto.Modele.Angajati;
using AtelierAuto.Modele.Masini;
using AtelierAuto.Servicii;
using AtelierAuto.Servicii.Angajati;
using AtelierAuto.Servicii.Atelier;
using System;

namespace AtelierAuto
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiciuDependente.Register<IServiciuAngajati>(new ServiciuAngajati());
            ServiciuDependente.Register<IServiciuAtelier>(new ServiciuAtelier());

            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Mecanic { Nume = "alex1", DataAngajarii = new DateTime(2019, 2, 20) });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Asistent { Nume = "alex2", DataAngajarii = new DateTime(2019, 2, 20) });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex3" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex4" });
            ServiciuDependente.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex5" });

            //ServiciuDependente.Get<IServiciuAngajati>().AfiseazaAngajati();
            //Console.WriteLine(ServiciuDependente.Get<IServiciuAngajati>().CalculeazaSalariu(2).Continut);
            //Console.WriteLine(ServiciuDependente.Get<IServiciuAngajati>().CalculeazaSalariu(1).Continut);

            var raspuns = ServiciuDependente.Get<IServiciuAtelier>().AdaugaLaCoada(new Autobuz(), 2);
            ServiciuDependente.Get<IServiciuAtelier>().AfiseazaCapacitate();
            var raspuns3 = ServiciuDependente.Get<IServiciuAtelier>().AdaugaLaCoada(new Autobuz(), 2);
            var raspuns2 = ServiciuDependente.Get<IServiciuAtelier>().AdaugaLaCoada(new MasinaStandard(), 1);
            var raspuns4 = ServiciuDependente.Get<IServiciuAtelier>().AdaugaLaCoada(new MasinaStandard(), 1);
            var raspuns5 = ServiciuDependente.Get<IServiciuAtelier>().AdaugaLaCoada(new MasinaStandard(), 1);
            var raspuns1 = ServiciuDependente.Get<IServiciuAtelier>().AdaugaLaCoada(new MasinaStandard(), 1);

            Console.WriteLine(raspuns.Mesaj);
            ServiciuDependente.Get<IServiciuAtelier>().AfiseazaCapacitate();
        }
    }
}
