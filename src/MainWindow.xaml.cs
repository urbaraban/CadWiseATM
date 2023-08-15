using CadWiseAtm;
using CadWiseATMApp.ViewModels;
using System.Windows;

namespace CadWiseATMApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateNewItem_Click(object sender, RoutedEventArgs e) => CreateNewAtm();

        private void CreateNewAtm()
        {
            var atm = new ATM(5);
            this.DataContext = new AtmViewModel(atm);
        }
    }
}
