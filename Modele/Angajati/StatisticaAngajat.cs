using System;
using System.Collections.Generic;
using System.Text;

namespace AtelierAuto.Modele.Angajati
{
    public class StatisticaAngajat
    {
        public int Id { get; set; }
        private double _masiniReparate, _autobuzeNoiReparate, _nrSolicitari;
        private double _bacsis;
        public double MasiniReparate
        {
            get => _masiniReparate;
            set
            {
                _masiniReparate += value;
            }
        }
        public double AutobuzeNoiReparate
        {
            get => _autobuzeNoiReparate;
            set
            {
                _autobuzeNoiReparate += value;
            }
        }
        public double NrSolicitari
        {
            get => _nrSolicitari;
            set
            {
                _nrSolicitari += value;
            }
        }
        public double Bacsis
        {
            get => _bacsis;
            set
            {
                _bacsis += value;
            }
        }
    }
}
