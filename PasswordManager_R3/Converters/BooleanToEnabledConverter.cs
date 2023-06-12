using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Converters
{
    class BooleanToEnabledConverter : System.Windows.Data.IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (parameter is ViewModels.Database_ViewModel) {
                if (value == null)
                    return false;
                else
                    return true;
            } else {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
