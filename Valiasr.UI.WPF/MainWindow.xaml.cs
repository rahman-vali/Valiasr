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
    using Valiasr.UI.WPF.ServiceReferencePerson;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource personDtoViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("personDtoViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // personDtoViewSource.Source = [generic data source]
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ServiceReferencePerson.PersonAccountServiceClient client = new PersonAccountServiceClient();
            PersonDto personDto = client.GetPersonByNationalIdentity("1"); 
            CollectionViewSource personDtoViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("personDtoViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            personDtoViewSource.Source = new[] { personDto };
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
             CollectionViewSource personDtoViewSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("personDtoViewSource")));
            ServiceReferencePerson.PersonAccountServiceClient client = new PersonAccountServiceClient();
           //button2.Content = client.UpdatePerson(personDtoViewSource.Source.);

        }
    }
}
