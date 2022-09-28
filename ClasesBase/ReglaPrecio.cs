using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ClasesBase
{
    public class ReglaPrecio: ValidationRule
    {
        private decimal _precio;
        public decimal Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            decimal number;
            ValidationResult result = new ValidationResult(true, null);
            if (string.IsNullOrEmpty(value.ToString()))
            {
                result = new ValidationResult(false, "El precio es obligatorio");
            }
            else if (!Decimal.TryParse(value.ToString(),out number)){
                result = new ValidationResult(false, "Debe ser un decimal");
            }
            return result;
        }
    }
}
