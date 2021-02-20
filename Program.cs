using AtelierAuto.Modele.Angajati;
using AtelierAuto.Servicii;
using AtelierAuto.Servicii.Angajati;
using System;

namespace AtelierAuto
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiciuDependente serviciuDependete = new ServiciuDependente();
            serviciuDependete.Register<IServiciuAngajati>(new ServiciuAngajati());

            serviciuDependete.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex" });
            serviciuDependete.Get<IServiciuAngajati>().AdaugaAngajat(new Mecanic { Nume = "alex1", DataAngajarii = new DateTime(2019, 2, 20) });
            serviciuDependete.Get<IServiciuAngajati>().AdaugaAngajat(new Asistent { Nume = "alex2", DataAngajarii = new DateTime(2019, 2, 20) });
            serviciuDependete.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex3" });
            serviciuDependete.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex4" });
            serviciuDependete.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex5" });

            serviciuDependete.Get<IServiciuAngajati>().AfiseazaAngajati();
            Console.WriteLine(serviciuDependete.Get<IServiciuAngajati>().CalculeazaSalariu(2).Continut);
            Console.WriteLine(serviciuDependete.Get<IServiciuAngajati>().CalculeazaSalariu(1).Continut);
        }
    }
}
