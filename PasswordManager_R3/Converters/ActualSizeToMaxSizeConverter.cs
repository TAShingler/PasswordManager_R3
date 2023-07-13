using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Converters
{
    class ActualSizeToMaxSizeConverter : System.Windows.Data.IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            //throw new NotImplementedException();
            //System.Diagnostics.Debug.WriteLine("value = " + value.ToString());
            //System.Diagnostics.Debug.WriteLine("targetType = " + targetType.ToString());
            //System.Diagnostics.Debug.WriteLine("parameter = " + parameter.ToString());

            double valueAsDouble = (double)value;
            if (parameter.Equals("height"))
                return Math.Floor((valueAsDouble - 5) * 0.5);

            if (parameter.Equals("width"))
                return Math.Floor((valueAsDouble -5) * 0.4);

            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
