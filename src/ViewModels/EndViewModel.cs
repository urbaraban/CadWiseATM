using CadWiseAtm;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.Generic;
using System.Windows.Input;

namespace CadWiseATMApp.ViewModels
{
    internal class EndViewModel : OperationViewModel
    {
        public override IEnumerable<ICommand> Operations { get; }
        public EndViewModel(ATM atm) : base(atm)
        {
            Operations = new ICommand[]
            {
                new ActionCommand(() => { EndSession(); })
            };
        }
    }
}
