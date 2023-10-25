using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PasswordManager_R3.Validators;
internal class StringValidationRule : System.Windows.Controls.ValidationRule {
    public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
        if (value is not string) {
            throw new ArgumentException("Value passed to StringValidationRule.Validate() is not a string.", "value");
        }

        bool isValid = !string.IsNullOrWhiteSpace(value as string);
        return new ValidationResult(isValid, "value passed to StringValidationRuile is not a string...");
    }
}
