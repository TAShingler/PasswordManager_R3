using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Converters;
internal class SelectedRecordPropertyMaskedConverter : System.Windows.Data.IMultiValueConverter {
    const string STRING_MASK = "\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022";

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
        //throw new NotImplementedException();
        var srProperty = values[0] as string;
        var tbIsChecked = (bool)values[1];

        if (tbIsChecked == true) {
            return string.IsNullOrWhiteSpace(srProperty) ? string.Empty : STRING_MASK;
        } else {
            return srProperty;
        }

        //return tbIsChecked == true ? STRING_MASK : srProperty;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
