using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ClasesBase
{
    public class ConversorEstados : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valuesString = (string)value;
            if (valuesString == "PENDIENTE") return new SolidColorBrush(Colors.Red);
            if (valuesString == "PAGADA")  return new SolidColorBrush(Colors.Green);
            if (valuesString == "CONTABILIZADA") return new SolidColorBrush(Colors.Blue);
            if (valuesString == "ANULADA") return new SolidColorBrush(Colors.Gray);
          
            //vistas y perfomance
            
            return valuesString ;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
