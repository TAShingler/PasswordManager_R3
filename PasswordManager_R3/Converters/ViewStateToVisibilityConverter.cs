using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Windows;

namespace PasswordManager_R3.Converters;
internal class ViewStateToVisibilityConverter : System.Windows.Data.IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        Visibility visibility = Visibility.Visible;

        if (parameter != null) {
            //Debug.WriteLine(parameter.GetType());
            //BTN_MAX
            if (parameter.Equals("BTN_MAX")) {
                visibility = (value.Equals(WindowState.Maximized)) ? Visibility.Collapsed : Visibility.Visible;
            }

            //BTN_RSTR
            if (parameter.Equals("BTN_RSTR")) {
                //visibility |= Visibility.Visible;  -- could be useful in future
                visibility = value.Equals(WindowState.Maximized) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        //throw new NotImplementedException();
        return visibility;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
