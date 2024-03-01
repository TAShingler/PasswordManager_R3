using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Converters;
internal class BooleanToMaxSizeConverter : System.Windows.Data.IValueConverter, System.Windows.Data.IMultiValueConverter {
    object System.Windows.Data.IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        //throw new NotImplementedException();
        var valueAsBool = (bool)value;

        if (valueAsBool is true)
            return 5.0;
        else
            return 0.0;
    }

    object System.Windows.Data.IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
        //throw new NotImplementedException();

        var val0 = (bool)values[0];             //is PanelDisplayed
        var val1 = (double)values[1];           //parent container actual size (height or width)
        var paramAsString = (string)parameter;  //which col/row value is for

        if (val0 is true) {
            switch (paramAsString) {
                case "lft":
                    return val1 * 0.4;
                case "rgt":
                    return val1;
                case "top":
                    return val1;
                case "btm":
                    return val1 * 0.4;
            }
        } else {
            switch (paramAsString) {
                case "lft":
                    return 0.0;
                case "rgt":
                    return val1;
                case "top":
                    return val1;
                case "btm":
                    return 0.0;
            }
        }

        return 0.0;
    }

    object System.Windows.Data.IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }

    object[] System.Windows.Data.IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
