using CadWiseAtm;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.Generic;
using System.Windows.Input;

namespace CadWiseATMApp.ViewModels
{
    internal class AtmViewModel : CommonNotifyModel
    {
        public OperationViewModel ActualOpeartion => OperationsStack.Peek();
        public Stack<OperationViewModel> OperationsStack { get; } = new Stack<OperationViewModel>();
        private ATM atm { get; }

        public AtmViewModel(ATM atm)
        {
            this.atm = atm;
            this.OperationsStack.Push(new StartViewModel(atm));
        }

        public ICommand BackCommand => new ActionCommand(() =>
        {
            if (OperationsStack.Peek() is not StartViewModel)
            {
                OperationsStack.Pop();
            }
        });

        public void Clear()
        {
            while (OperationsStack.Peek() is not StartViewModel)
            {
                OperationsStack.Pop();
            }
        }
    }
}
