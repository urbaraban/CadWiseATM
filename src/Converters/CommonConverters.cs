using CadWiseAtm;
using CadWiseATMApp.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CadWiseATMApp.Converters
{
    public class CaseViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MoneyCase money)
            {
                return new CasesViewModel(money);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
