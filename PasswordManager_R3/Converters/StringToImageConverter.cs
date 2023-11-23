using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Converters;
internal class StringToImageConverter : System.Windows.Data.IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
        //var recordIconsDictionary = App.Current.Resources.MergedDictionaries.Where(rd => rd.)
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
