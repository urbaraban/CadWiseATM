using CadWiseAtm;
using CadWiseATMApp.ViewModels;
using CadWiseATMApp.Windows;
using System.Windows;
using System.Windows.Data;

namespace CadWiseATMApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateNewItem_Click(object sender, RoutedEventArgs e) => CreateNewAtm();

        private void InsideItem_Click(object sender, RoutedEventArgs e) => ShowInsideWindow();

        private void ShowInsideWindow()
        {
            InsideWindow insideWindow = new InsideWindow();
            Binding contextbinding = new Binding("DataContext");
            contextbinding.Source = this;
            insideWindow.SetBinding(Window.DataContextProperty, contextbinding);
            insideWindow.Show();
        }

        private void CreateNewAtm()
        {
            var atm = new ATM(5);
            this.DataContext = new AtmViewModel(atm);
        }
    }
}
