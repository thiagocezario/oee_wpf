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
    /// Interaction logic for Performance.xaml
    /// </summary>
    public partial class Performance : Page
    {
        OEEModel oee;
        public Performance(OEEModel oee)
        {
            this.oee = oee;
            InitializeComponent();
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            oee.performance.unit1 = typeComboBox.Text;
        }

        private void speedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            oee.performance.unit2 = speedComboBox.Text;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(productionAmountTextField.Text, out double productionAmount);
            double.TryParse(productionTimeFieldText.Text, out double productionTime);
            Production production = new Production
            {
                time = productionTime,
                productionAmount = productionAmount
            };

            oee.performance.actualProductionSchedule.Add(production);
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Content = new string[] { production.productionAmount.ToString(), production.time.ToString() };
            inconsistentProductionList.Items.Add(listViewItem);
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            Calculate(null, null);
            Quality qualityPage = new Quality(this.oee);
            this.NavigationService.Navigate(qualityPage);
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            typeComboBox_SelectionChanged(null, null);
            speedComboBox_SelectionChanged(null, null);
            double.TryParse(expectedProductionAmountTextField.Text, out double expectedProductionAmount);
            
            if (oee.performance.unit2 == "segundo")
            {
                oee.performance.productionSchedule.totalProduced = (oee.disponibility.workedHours * 3600) * expectedProductionAmount;
            } else if (oee.performance.unit2 == "minuto")
            {
                oee.performance.productionSchedule.totalProduced = (oee.disponibility.workedHours * 60) * expectedProductionAmount;

            } else if (oee.performance.unit2 == "hora")
            {
                oee.performance.productionSchedule.totalProduced = oee.disponibility.workedHours * expectedProductionAmount;
            }

            double lessTime = 0;
            double totalInconsistentProduction = 0;

            foreach(Production item in oee.performance.actualProductionSchedule)
            {
                if (oee.performance.unit2 == "segundo")
                {
                    item.totalProduced = (item.time * 3600) * item.productionAmount;
                }
                else if (oee.performance.unit2 == "minuto")
                {
                    item.totalProduced = (item.time * 60) * item.productionAmount;

                }
                else if (oee.performance.unit2 == "hora")
                {
                    item.totalProduced = item.time * item.productionAmount;
                }

                lessTime += item.time;
                totalInconsistentProduction += item.totalProduced;
            }

            double normalTimeProduction = oee.disponibility.workedHours - lessTime;
            double normalProduction = 0;

            if (oee.performance.unit2 == "segundo")
            {
                normalProduction = (normalTimeProduction * 3600) * expectedProductionAmount;
            }
            else if (oee.performance.unit2 == "minuto")
            {
                normalProduction = (normalTimeProduction * 60) * expectedProductionAmount;
            }
            else if (oee.performance.unit2 == "hora")
            {
                normalProduction = normalTimeProduction * expectedProductionAmount;
            }

            oee.performance.actualProduction = normalProduction + totalInconsistentProduction;


            expectedProduction.Content = oee.performance.productionSchedule.totalProduced + " " + typeComboBox.Text;
            actualProduction.Content = oee.performance.actualProduction + " " + typeComboBox.Text;
        }
    }
}
