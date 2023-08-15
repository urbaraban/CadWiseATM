using CadWiseAtm;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CadWiseATMApp.ViewModels
{
    internal class InsideViewModel : CommonNotifyModel
    {
        public ObservableCollection<MoneyCase> MoneyCases { get; }
        public MoneyCase? SelectedCase { get; set; }

        public int Limit => this.atm_.Limit;
        public int Count => this.atm_.Count;

        private ATM atm_ { get; }
        public InsideViewModel(ATM atm) 
        {
            this.atm_ = atm;
            this.MoneyCases = new ObservableCollection<MoneyCase>(atm);
        }


        public ICommand RemoveCaseCommand => new ActionCommand(() =>
        {
            if (this.atm_.Remove(this.SelectedCase))
            {
                this.MoneyCases.Remove(this.SelectedCase);
                this.SelectedCase = null;
            }
        });

        public ICommand AddCaseCommand => new ActionCommand(() =>
        {
            this.AddToAtm(new MoneyCase(100, MoneyCurrency.RUB, 200, 44));
        });

        public ICommand AddRandomCaseCommand => new ActionCommand(() =>
        {
            Random rnd = new Random();
            int nominal = (1 + rnd.Next(9)) * 100;
            int currency = rnd.Next(3);
            int limit = rnd.Next(1000);
            int count = rnd.Next(1000);
            this.AddToAtm(new MoneyCase(nominal, (MoneyCurrency)currency, limit, count));
        });

        private void AddToAtm(MoneyCase moneyCase)
        {
            if (this.atm_.Add(moneyCase))
            {
                this.MoneyCases.Add(moneyCase);
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged(nameof(Limit));
            }
        }
    }
}
