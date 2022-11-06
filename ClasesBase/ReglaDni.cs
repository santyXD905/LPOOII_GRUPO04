using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Diagnostics;
namespace ClasesBase
{
    public class ReglaDni : ValidationRule
    {
        private int _dni;
        public int Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (value.ToString().Equals("No seleccionado"))
            {
                result = new ValidationResult(false, "El cliente es obligatorio");
            }
            return result;
        }
    }
}
