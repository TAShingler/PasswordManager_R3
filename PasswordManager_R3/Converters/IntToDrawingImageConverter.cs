using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PasswordManager_R3.Converters
{
    class IntToDrawingImageConverter : System.Windows.Data.IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            //throw new NotImplementedException();
            System.Windows.Media.DrawingImage[] recordIconsArr = (System.Windows.Media.DrawingImage[])App.Current.Resources["RecordIconsArray"];

            return recordIconsArr[(int)value] as System.Windows.Media.DrawingImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
