using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PasswordManager_R3.Converters;
internal class BooleanToMaskedStringConverter : System.Windows.Data.IMultiValueConverter {
    const string PASS_MASK = "\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022\u2022";
    //public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
    //    //throw new NotImplementedException();
    //    System.Diagnostics.Debug.WriteLine($"BooleanToMaskedStringConverter.Convert().value = {value}");
    //    System.Diagnostics.Debug.WriteLine($"BooleanToMaskedStringConverter.Convert().parameter = {parameter}");

    //    return "password";
    //}

    //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
    //    throw new NotImplementedException();
    //}

    object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
        //throw new NotImplementedException();
        System.Diagnostics.Debug.WriteLine($"BooleanToMaskedStringConverter.Convert().values[0] = {values[0]}");
        System.Diagnostics.Debug.WriteLine($"BooleanToMaskedStringConverter.Convert().values[1].GetType() = {values[1]}");

        System.Diagnostics.Debug.WriteLine($"BooleanToMaskedStringConverter.Convert().values[1].GetType() = {values[1] is Models.AppVariables}");
        //if (values[1].GetType() != bool)
        //    return string.Empty;
        if (values[1] is not bool) {
            System.Diagnostics.Debug.WriteLine($"BooleanToMaskedStringConverter.values[1] is not bool = {values[1] is not bool}");
            return null;
        }
        if ((bool)values[1] == true) {
            return PASS_MASK;
        } else {
            return values[0];
        }

        //return "password";
    }

    object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
