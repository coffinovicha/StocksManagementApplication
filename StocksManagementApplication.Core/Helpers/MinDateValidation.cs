using System.ComponentModel.DataAnnotations;

namespace StocksApp.CustomValidations
{
    public class MinDateValidation : ValidationAttribute
    {
        private readonly DateTime _date;
        public MinDateValidation(string date)
        {
            _date = DateTime.Parse(date);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime < _date)
                {
                    return new ValidationResult($"Date should be more than {_date}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
