using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ClasesBase
{
    public class ReglaDni : ValidationRule
    {
        private decimal _dni;
        public decimal Dni
        {
            get { return _dni; }
            set { _dni = value; }
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            if (string.IsNullOrEmpty(value.ToString()))
            {
                result = new ValidationResult(false, "El cliente es obligatorio");
            }
            return result;
        }
    }
}
