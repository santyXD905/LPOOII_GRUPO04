using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ClasesBase
{
    public class ReglaCantidad: ValidationRule
    {
        private decimal _cantidad;
        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int number;
            ValidationResult result = new ValidationResult(true, null);
            if (string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
            {
                result = new ValidationResult(false, "La cantidad es obligatoria");
            }
            else if (!Int32.TryParse(value.ToString(), out number))
            {
                result = new ValidationResult(false, "Debe ser un numero");
            }
            return result;
        }
    }
}
