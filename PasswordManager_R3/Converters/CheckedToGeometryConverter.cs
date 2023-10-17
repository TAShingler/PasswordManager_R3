using PasswordManager_R3.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager_R3.Converters;
internal class CheckedToGeometryConverter : System.Windows.Data.IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        string pathGeometryFiguresData = string.Empty;

        if (value.Equals(true)) {
            //IsChecked == true
            switch (((App)App.Current).AppVariables.TreeExpandCollapseButtonStyle) {
                case Enums.TreeExpandCollapseButtonStyle.PlusMinusSigns:
                    pathGeometryFiguresData = "M 4 4 L 4 13 L 13 13 L 13 4 L 4 4 z M 5 5 L 12 5 L 12 12 L 5 12 L 5 5 z M 6 8 L 6 9 L 11 9 L 11 8 L 6 8 z ";
                    break;
                case Enums.TreeExpandCollapseButtonStyle.Arrows:
                    pathGeometryFiguresData = "M 5,6 4,7 8,11 12,7 11,6 8,9 Z";
                    break;
                case Enums.TreeExpandCollapseButtonStyle.Folders:
                    pathGeometryFiguresData = "m 3,3 h 4 l 1,1 h 5 c 0.554,0 1,0.446 1,1 H 3 v 7 L 4,6 h 11 l -1,6 c -0.09108,0.546462 -0.446,1 -1,1 H 3 C 2.446,13 2,12.554 2,12 V 4 C 2,3.446 2.446,3 3,3 Z"; //"m 2,3 h 4 l 1,1 h 6 c 0.554,0 1,0.446 1,1 H 2 v 7 L 3,6 h 12 l -1,6 c -0.09108,0.546462 -0.446,1 -1,1 H 2 C 1.446,13 1,12.554 1,12 V 4 C 1,3.446 1.446,3 2,3 Z";
                    break;
                default:
                    pathGeometryFiguresData = "M 5,6 4,7 8,11 12,7 11,6 8,9 Z";
                    break;
            }
        } else {
            //IsChecked == false
            switch (((App)App.Current).AppVariables.TreeExpandCollapseButtonStyle) {
                case Enums.TreeExpandCollapseButtonStyle.PlusMinusSigns:
                    pathGeometryFiguresData = "m 4,4 v 9 h 9 V 4 Z m 1,1 h 7 v 7 H 5 Z m 1,3 v 1 h 2 v 2 H 9 V 9 h 2 V 8 H 9 V 6 H 8 v 2 z";
                    break;
                case Enums.TreeExpandCollapseButtonStyle.Arrows:
                    pathGeometryFiguresData = "M 6,11 7,12 11,8 7,4 6,5 9,8 Z";
                    break;
                case Enums.TreeExpandCollapseButtonStyle.Folders:
                    pathGeometryFiguresData = "m 3,3 h 4 l 1,1 h 5 c 0.554,0 1,0.446 1,1 v 7 c 0,0.554 -0.446,1 -1,1 H 3 C 2.446,13 2,12.554 2,12 V 4 C 2,3.446 2.446,3 3,3 Z"; //"m 2,3 h 4 l 1,1 h 6 c 0.554,0 1,0.446 1,1 v 7 c -0.09108,0.546462 -0.446,1 -1,1 H 2 C 1.446,13 1,12.554 1,12 V 4 C 1,3.446 1.446,3 2,3 Z";
                    break;
                default:
                    pathGeometryFiguresData = "M 6,11 7,12 11,8 7,4 6,5 9,8 Z";
                    break;
            }
        }

        return pathGeometryFiguresData;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
