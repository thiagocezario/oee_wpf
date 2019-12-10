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
    /// Interaction logic for Disponibility.xaml
    /// </summary>
    public partial class Disponibility : Page
    {
        OEEModel oee;
        public Disponibility()
        {
            this.oee = new OEEModel();
            InitializeComponent();
        }

        private void AddPlannedStop(object sender, RoutedEventArgs e)
        {
            string description = plannedDescriptionTextBox.Text;
            double.TryParse(plannedTimeTextBox.Text, out double time);
            Downtime downtime = new Downtime
            {
                reason = description,
                timeDown = time
            };
            oee.disponibility.expectedDowntime.Add(downtime);
            plannedList.Items.Add(downtime.reason + " - " + downtime.timeDown + "h");
        }

        private void AddUnplannedStop(object sender, RoutedEventArgs e)
        {
            string description = unplannedDescriptionTextBox.Text;
            double.TryParse(unplannedTimeTextBox.Text, out double time);
            Downtime downtime = new Downtime
            {
                reason = description,
                timeDown = time
            };
            oee.disponibility.unexpectedDowntime.Add(downtime);
            unplannedList.Items.Add(downtime.reason + " - " + downtime.timeDown + "h");
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            double.TryParse(productionTimeInDays.Text, out double daysOfProdution);
            double.TryParse(productionTimeInHours.Text, out double hoursOfProduction);

            double workedTime = daysOfProdution * hoursOfProduction;
            double expectedDowntime = 0;

            foreach(Downtime downtime in oee.disponibility.expectedDowntime)
            {
                expectedDowntime += downtime.timeDown;
            }

            oee.disponibility.expectedWorkedHours = workedTime - expectedDowntime;

            double unexpectedDowntime = 0;

            foreach(Downtime downtime in oee.disponibility.unexpectedDowntime)
            {
                unexpectedDowntime += downtime.timeDown;
            }

            oee.disponibility.workedHours = oee.disponibility.expectedWorkedHours - unexpectedDowntime;

            expectedWorkTime.Content = oee.disponibility.expectedWorkedHours + "h";
            actualWorkedTime.Content = oee.disponibility.workedHours + "h";
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            Calculate(null, null);
            Performance performancePage = new Performance(this.oee);
            this.NavigationService.Navigate(performancePage);
        }
    }
}
