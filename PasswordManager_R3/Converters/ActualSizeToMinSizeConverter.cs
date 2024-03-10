using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PasswordManager_R3.Converters
{
    class ActualSizeToMinSizeConverter : System.Windows.Data.IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            //throw new NotImplementedException();

            if (value is not double)
                return 0.0;

            //if (parameter is not string)
            //    return 0.0;

            double valueAsDouble = (double)value;
            //string paramAsString = (string)parameter;

            //if (paramAsString.Equals("height"))
            //    return (valueAsDouble - 5) * 0.5;
            //else
            //    return (valueAsDouble - 5) * 0.6;
            return valueAsDouble * 0.6;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
