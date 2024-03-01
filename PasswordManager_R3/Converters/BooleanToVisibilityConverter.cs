using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PasswordManager_R3.Converters;
internal class BooleanToVisibilityConverter : System.Windows.Data.IValueConverter {
    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        //throw new NotImplementedException();

        bool valueAsBool = (bool)value;

        if (valueAsBool is true)
            return System.Windows.Visibility.Visible;
        else
            return System.Windows.Visibility.Collapsed;
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
