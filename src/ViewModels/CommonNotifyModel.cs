using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CadWiseATMApp.ViewModels
{
    internal class CommonNotifyModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
