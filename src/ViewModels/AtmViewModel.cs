using CadWiseAtm;
using CadWiseATMApp.Pages;
using CadWiseATMApp.Windows;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CadWiseATMApp.ViewModels
{
    internal class AtmViewModel : CommonNotifyModel
    {
        public OperationViewModel AlreadyOpeartion
        {
            get => _operation;
            set
            {
                this._operation = value;
                OnPropertyChanged(nameof(AlreadyOpeartion));
            }
        }
        private OperationViewModel _operation;

        private ATM atm { get; }

        public AtmViewModel()
        {
            this.atm = new ATM(99);
        }

        public AtmViewModel(ATM atm) : this()
        {
            this.atm = atm;
        }


        public ICommand ShowInsideCommand => new ActionCommand(() =>
        {
            InsideWindow insideWindow = new InsideWindow();
            insideWindow.DataContext = new InsideViewModel(this.atm);
            insideWindow.Show();
        });

        public void Clear()
        {
            
        }
    }
}
