using System;
using System.Text.RegularExpressions;

namespace DataLayer.Schema.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VariableIdentifierAttribute : Attribute, IValidationAttribute
    {
        public bool IsValid(object validatedProperty)
        {
            var property = validatedProperty.ToString().Normalize();

            return Reg.IsMatch(property);
        }

        private static readonly Regex Reg = new Regex(@"\w+");
    }
}