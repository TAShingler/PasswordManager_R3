using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PasswordManager_R3.Converters {
    class SelectedIndexToVisibilityConverter : IValueConverter {
        //public int TargetIndex { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is int selectedIndex && parameter != null && int.TryParse(parameter.ToString(), out int target)) {
                return selectedIndex == target ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
