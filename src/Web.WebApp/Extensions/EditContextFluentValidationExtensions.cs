using FluentValidation;
using FluentValidation.Internal;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq;
using System.Reflection;

namespace Web.WebApp.Extensions
{
    // Ref: https://chrissainty.com/using-fluentvalidation-for-forms-validation-in-razor-components/
    public static class EditContextFluentValidationExtensions
    {
        public static EditContext AddFluentValidation<T, TValidator>(this EditContext editContext) where TValidator : AbstractValidator<T>
        {
            if (editContext == null)
            {
                throw new ArgumentNullException("Edit context cannot be null");
            }

            var validator = (IValidator)Activator.CreateInstance(typeof(TValidator));

            var messages = new ValidationMessageStore(editContext);

            editContext.OnValidationRequested +=
                (sender, eventArgs) => ValidateModel<T>((EditContext)sender, messages, validator);

            editContext.OnFieldChanged +=
                (sender, eventArgs) => ValidateField(editContext, messages, eventArgs.FieldIdentifier, validator);

            return editContext;
        }

        private static void ValidateModel<T>(EditContext editContext, ValidationMessageStore messages, IValidator validator)
        {
            var context = new ValidationContext<T>((T)editContext.Model);
            
            //var validator = GetValidatorForModel(editContext.Model);
            var validationResults = validator.Validate(context);

            messages.Clear();
            foreach (var validationResult in validationResults.Errors)
            {
                messages.Add(editContext.Field(validationResult.PropertyName), validationResult.ErrorMessage);
            }

            editContext.NotifyValidationStateChanged();
        }

        private static void ValidateField(EditContext editContext, ValidationMessageStore messages, in FieldIdentifier fieldIdentifier, IValidator validator)
        {
            var properties = new[] { fieldIdentifier.FieldName };
            var context = new ValidationContext<object>(fieldIdentifier.Model, new PropertyChain(), new MemberNameValidatorSelector(properties));

            //var validator = GetValidatorForModel(fieldIdentifier.Model);
            var validationResults = validator.Validate(context);

            messages.Clear();
            foreach(var error in validationResults.Errors)
            {
                messages.Clear(fieldIdentifier);
                messages.Add(fieldIdentifier, error.ErrorMessage);
            }

            editContext.NotifyValidationStateChanged();
        }

        // *** Fails to get from different assembly
        private static IValidator GetValidatorForModel(object model)
        {
            var abstractValidatorType = typeof(AbstractValidator<>).MakeGenericType(model.GetType());
            var modelValidatorType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.IsSubclassOf(abstractValidatorType));
            var modelValidatorInstance = (IValidator)Activator.CreateInstance(modelValidatorType);

            return modelValidatorInstance;
        }
    }
}
