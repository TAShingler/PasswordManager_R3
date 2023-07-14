using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Converters {
    class QuickAccessIconSizeToDoubleConverter : System.Windows.Data.IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double sizeValue = 0.0;

            switch(value) {
                case Enums.QuickAccessIconSize.Small:
                    sizeValue = 24.0;
                    break;
                case Enums.QuickAccessIconSize.Medium:
                    sizeValue = 36.0;
                    break;
                case Enums.QuickAccessIconSize.Large:
                    sizeValue = 48.0;
                    break;
                default:
                    sizeValue = 24.0;
                    break;
            }

            System.Diagnostics.Debug.WriteLine("sizeValue = " + sizeValue);

            return sizeValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
