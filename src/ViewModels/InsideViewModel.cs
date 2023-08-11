using CadWiseAtm;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.Generic;
using System.Windows.Input;

namespace CadWiseATMApp.ViewModels
{
    internal class InsideViewModel
    {
        public IEnumerable<MoneyCase> MoneyCases => atm_;

        private ATM atm_ { get; }
        public InsideViewModel(ATM atm) { }

        public ICommand AddCaseCommand => new ActionCommand(() =>
        {

        });
    }
}
