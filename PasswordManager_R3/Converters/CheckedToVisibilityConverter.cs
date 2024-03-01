using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace PasswordManager_R3.Converters;
internal class CheckedToVisibilityConverter : IValueConverter { //might delete - not sure where this is being used, or whether it is needed...
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value == null) return (Visibility)parameter;

        //check that IsChecked == true
        //if ((bool)value == true) {
            //check parameter state
            if ((Visibility)parameter == Visibility.Visible) return Visibility.Collapsed;
            else return Visibility.Visible;
        //}
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}


//** MAY DELETE THIS CONVERTER -- 5-13-2023