using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Converters
{
    class DoubleToGridLengthConverter : System.Windows.Data.IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            //throw new NotImplementedException();

            double valueAsDouble = (double)value;

            return Math.Floor((valueAsDouble - 5) * 0.4);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
