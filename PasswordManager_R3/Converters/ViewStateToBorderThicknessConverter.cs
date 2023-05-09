using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Converters;
internal class ViewStateToBorderThicknessConverter : System.Windows.Data.IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        //throw new NotImplementedException();
        //if (parameter == null) return new object(); //return default obj -- might change; might throw exception; idk yet...

        System.Diagnostics.Debug.WriteLine("value to string: " + value.ToString());
        System.Diagnostics.Debug.WriteLine("value type: " + value.GetType());

        //if (value.Equals(System.Windows.WindowState.Normal)) {
        //    return 1;
        //}
        
        if (value.Equals(System.Windows.WindowState.Maximized)) {
            return 8;
        }

        return 1;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
