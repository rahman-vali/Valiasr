using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Valiasr.UI.WPF
{
    using Valiasr.UI.WPF.AccountServiceReference;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var client = new AccountServiceClient();
            var customerDtos = client.GetCustomerByAccountNo(AccountNo.Text);
            var customerDtoViewSource = ((CollectionViewSource)(this.FindResource("customerDtoViewSource")));
            customerDtoViewSource.Source = customerDtos;

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            // Load data by setting the CollectionViewSource.Source property:
            // customerDtoViewSource.Source = [generic data source]
        }
    }
}
