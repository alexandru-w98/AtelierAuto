﻿using System;
using System.Collections.Generic;
using System.Text;
using AtelierAuto.Constante;

namespace AtelierAuto.Modele.Angajati
{
    public class Director : Angajat
    {
        Director()
        {
            CoeficientSalariat = ConstanteAngajati.CS_DIRECTOR;
        }
    }
}
