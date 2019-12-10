using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OEE_WPF
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : Page
    {
        OEEModel oee;
        double qualityValue;
        double performanceValue;
        double disponibilityValue;
        double totalValue;

        public Results(OEEModel oee)
        {
            this.oee = oee;
            InitializeComponent();
            calculateDisponibility();
            calculatePerformance();
            calculateQuality();
            calculateResults();
        }

        private void calculateResults()
        {
            totalValue = qualityValue * disponibilityValue * performanceValue;
            var percent = (totalValue * 100);
            total.Content = Math.Round(percent, 2) + "%";
        }

        private void calculateQuality()
        {
            qualityValue = (oee.performance.actualProduction - oee.quality.defectItems) / oee.performance.actualProduction;
            var percent = (qualityValue * 100);
            quality.Content = Math.Round(percent, 2) + "%";
        }

        private void calculatePerformance()
        {
            performanceValue = oee.performance.actualProduction / oee.performance.productionSchedule.totalProduced;
            var percent = (performanceValue * 100);
            performance.Content = Math.Round(percent, 2) + "%";
        }

        private void calculateDisponibility()
        {
            disponibilityValue = oee.disponibility.workedHours / oee.disponibility.expectedWorkedHours;
            var percent = (disponibilityValue * 100);
            disponibility.Content = Math.Round(percent, 2) + "%";
        }
    }
}
