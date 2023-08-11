using CadWiseAtm;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.Generic;
using System.Windows.Input;

namespace CadWiseATMApp.ViewModels
{
    internal class StartViewModel : OperationViewModel
    {
        public override IEnumerable<ICommand> Operations { get; }
        public StartViewModel(ATM atm) : base(atm)
        {
            this.Operations = new ICommand[]
            {
                new ActionCommand(() => { Navigate(new IncrementView(this.atm)); }),
                new ActionCommand(() => { Navigate(new DecrementView(this.atm)); })
            };
        }
    }
}
