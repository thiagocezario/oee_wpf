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
    /// Interaction logic for Quality.xaml
    /// </summary>
    public partial class Quality : Page
    {
        OEEModel oee;
        public Quality(OEEModel oee)
        {
            this.oee = oee;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(badItemsAmount.Text, out double badItems);
            oee.quality.defectItems = badItems;

            Results resultsPage = new Results(this.oee);
            this.NavigationService.Navigate(resultsPage);
        }
    }
}
