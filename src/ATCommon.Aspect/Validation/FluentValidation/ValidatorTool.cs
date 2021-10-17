using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATCommon.Aspect.Validation.FluentValidation
{
    public class ValidatorTool
    {
        protected ValidatorTool()
        {
            
        }
        public static void FluentValidate(IValidator validator,object entity)
        {
            
            var result = validator.Validate((IValidationContext)entity);
            if (result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
