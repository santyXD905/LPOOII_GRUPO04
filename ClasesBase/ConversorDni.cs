using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing;
using System.Globalization;
using System.Diagnostics;
namespace ClasesBase
{
    public class ConversorDni : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
            Trace.WriteLine(value.ToString());
            string valueString = value.ToString();
            return Int32.Parse(valueString.Substring(0,valueString.IndexOf(',')));
        }
    }
}
