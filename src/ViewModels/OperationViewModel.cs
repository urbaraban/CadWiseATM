using CadWiseAtm;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Input;

namespace CadWiseATMApp.ViewModels
{
    internal abstract class OperationViewModel : CommonNotifyModel
    {
        public event EventHandler<OperationViewModel>? OperationChanged;
        public event EventHandler TimeoutEvent;

        public abstract IEnumerable<ICommand> Operations { get; }

        protected ATM atm { get; }

        private Timer timer;

        public OperationViewModel(ATM atm) 
        {
            this.atm = atm;
            this.Init();
        }

        public OperationViewModel(OperationViewModel operationViewModel) : this(operationViewModel.atm) { }

        public void Init()
        {
            this.timer = new Timer(20000);
            this.timer.Elapsed += Timer_Elapsed;
            this.timer.Start();
        }

        protected void EndSession()
        {
            this.timer.Dispose();
            TimeoutEvent?.Invoke(this, EventArgs.Empty);
        }

        protected void ResetTimeout()
        {
            this.timer.Start();
        }

        protected void Navigate(OperationViewModel operation)
        {
            this.timer.Dispose();
            this.OperationChanged?.Invoke(this, operation);
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e) => EndSession();
    }
}
