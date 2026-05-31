using System.ComponentModel.DataAnnotations;

namespace LiveUpdates.ValidationHelpers
{
    public class ValidationHelper
    {
        public static void ModelValidation<T>(T model) where T : class 
        {
            ValidationContext validationContext = new ValidationContext(model);

            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValidated = Validator.TryValidateObject(model, validationContext, validationResults, true);

            if (!isValidated) 
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
