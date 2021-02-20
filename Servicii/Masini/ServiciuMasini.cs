using AtelierAuto.Modele.Masini;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Servicii.Masini
{
    public class ServiciuMasini : IServiciuMasini
    {
        private List<Masina> _masini;
        private int _id;

        public ServiciuMasini()
        {
            _masini = new List<Masina>();
            _id = 0;

            Initializare();
        }

        public Masina ObtineMasina()
        {
            if (_id < _masini.Count)
            {
                return _masini[_id++];
            } else
            {
                return null;
            }
        }

        private void Initializare()
        {
            _masini.Add(new MasinaStandard
            {
                Id = 0,
                NrKilometri = 150000,
                AnulFabricatiei = 2005,
                EsteMotorDiesel = true,
                ModTransmisie = Constante.ConstanteMasini.ModTransmisie.Automat
            });
            _masini.Add(new MasinaStandard
            {
                Id = 10,
                NrKilometri = 150000,
                AnulFabricatiei = 2005,
                EsteMotorDiesel = true,
                ModTransmisie = Constante.ConstanteMasini.ModTransmisie.Automat
            });
            _masini.Add(new Autobuz
            {
                Id = 11,
                NrKilometri = 150000,
                AnulFabricatiei = 2005,
                EsteMotorDiesel = true
            });
            _masini.Add(new MasinaStandard
            {
                Id = 14,
                NrKilometri = 150000,
                AnulFabricatiei = 2005,
                EsteMotorDiesel = true,
                ModTransmisie = Constante.ConstanteMasini.ModTransmisie.Automat
            });
            _masini.Add(new Autobuz
            {
                Id = 12,
                NrKilometri = 150000,
                AnulFabricatiei = 2005,
                EsteMotorDiesel = true
            });
            _masini.Add(new MasinaStandard
            {
                Id = 15,
                NrKilometri = 150000,
                AnulFabricatiei = 2005,
                EsteMotorDiesel = true,
                ModTransmisie = Constante.ConstanteMasini.ModTransmisie.Automat
            });
            _masini.Add(new MasinaStandard
            {
                Id = 16,
                NrKilometri = 150000,
                AnulFabricatiei = 2005,
                EsteMotorDiesel = true,
                ModTransmisie = Constante.ConstanteMasini.ModTransmisie.Automat
            });
            _masini.Add(new Camion
            {
                Id = 17,
                NrKilometri = 150000,
                AnulFabricatiei = 2005,
                EsteMotorDiesel = true
            });
            _masini.Add(new Camion
            {
                Id = 18,
                NrKilometri = 150000,
                AnulFabricatiei = 2005,
                EsteMotorDiesel = true
            });
        }


    }
}
