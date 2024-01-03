using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace PasswordManager_R3.Converters;
internal class ViewStateToVisibilityConverter : System.Windows.Data.IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        System.Windows.Visibility visibility = System.Windows.Visibility.Visible;

        if (parameter == null)
            return visibility;


        //if (parameter.Equals("BTN_MAX"))
        //    visibility = (value.Equals(System.Windows.WindowState.Maximized)) ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        //if (parameter.Equals("BTN_RSTR"))
        //    visibility = value.Equals(System.Windows.WindowState.Maximized) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        //if (parameter.Equals("LCKSCRN_UNLOCK"))
        //    visibility = value.Equals(Enums.LockScreenState.UnlockDatabase) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        //if (parameter.Equals("LCKSCRN_SET_NEW"))
        //    visibility = value.Equals(Enums.LockScreenState.SetNewPassword) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;


        System.Diagnostics.Debug.WriteLine($"value == {value}");
        switch (parameter) {
            case "BTN_MAX":
                visibility = (value.Equals(System.Windows.WindowState.Maximized)) ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
                break;
            case "BTN_RSTR":
                visibility = value.Equals(System.Windows.WindowState.Maximized) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                break;
            case "LCKSCRN_UNLOCK":
                visibility = value.Equals(Enums.LockScreenState.LockDatabase) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                break;
            case "LCKSCRN_SET_NEW":
                visibility = value.Equals(Enums.LockScreenState.SetNewPassword) ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                break;
        }


        return visibility;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
