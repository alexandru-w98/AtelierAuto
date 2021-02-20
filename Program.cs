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
            serviciuDependete.Get<IServiciuAngajati>().AdaugaAngajat(new Mecanic { Nume = "alex1" });
            serviciuDependete.Get<IServiciuAngajati>().AdaugaAngajat(new Asistent { Nume = "alex2" });
            serviciuDependete.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex3" });
            serviciuDependete.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex4" });
            serviciuDependete.Get<IServiciuAngajati>().AdaugaAngajat(new Director { Nume = "alex5" });

            serviciuDependete.Get<IServiciuAngajati>().AfiseazaAngajati();
        }
    }
}
