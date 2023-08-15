using CadWiseAtm;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;

namespace CadWiseATMApp.ViewModels
{
    internal class CasesViewModel : CommonNotifyModel
    {
        public int Nominal => case_.MoneyType.Nominal;
        public MoneyCurrency Currency => case_.MoneyType.Currency;
        public int Limit => case_.Limit;
        public int Count => case_.Count;

        private MoneyCase case_ { get; }

        public CasesViewModel(MoneyCase moneyCase)
        {
            this.case_ = moneyCase;
            this.case_.CountChanged += Case__CountChanged;
        }

        public ICommand RemoveCommand => new ActionCommand(() =>
        {
            this.case_.Remove();
        });

        private void Case__CountChanged(object? sender, System.EventArgs e)
        {
            OnPropertyChanged();
        }
    }
}
