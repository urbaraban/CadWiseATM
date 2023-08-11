using CadWiseAtm;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.Generic;
using System.Windows.Input;

namespace CadWiseATMApp.ViewModels
{
    internal class IncrementView : OperationViewModel
    {
        public override IEnumerable<ICommand> Operations { get; }

        public IncrementView(ATM atm) : base(atm)
        {
            this.Operations = new ICommand[]
            {
                new ActionCommand(() => { Navigate(new EndViewModel(this.atm)); }),
            };
        }
    }
}
