using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PasswordManager_R3.Converters
{
    class ActualSizeToMaxSizeConverter : System.Windows.Data.IValueConverter, System.Windows.Data.IMultiValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            //throw new NotImplementedException();
            System.Diagnostics.Debug.WriteLine("value = " + value.ToString());
            //System.Diagnostics.Debug.WriteLine("targetType = " + targetType.ToString());
            //System.Diagnostics.Debug.WriteLine("parameter = " + parameter.ToString());
            
            double valueAsDouble = (double)value;

            if (valueAsDouble > 0.0) {
                if (parameter.Equals("height"))
                    return Math.Floor((valueAsDouble - 5) * 0.5);

                if (parameter.Equals("width"))
                    return Math.Floor((valueAsDouble - 5) * 0.4);
            }

            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            //throw new NotImplementedException();

            var actualSize = (double)values[0];
            var isRecordDetailsPanelEnabled = (bool)values[1];
            var dimension = (string)parameter;

            if (isRecordDetailsPanelEnabled is false)
                return 0.0;

            if (actualSize > 0.0) {
                if (parameter.Equals("height"))
                    return Math.Floor((actualSize - 5) * 0.5);

                if (parameter.Equals("width"))
                    return Math.Floor((actualSize - 5) * 0.4);
            }
            
            return 0.0;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
