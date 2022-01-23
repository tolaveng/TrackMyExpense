using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using Web.WebApp.Extensions;

namespace Web.WebApp.Components
{
    // Ref: https://chrissainty.com/using-fluentvalidation-for-forms-validation-in-razor-components/
    public class FluentValidationValidator<T, TValidator> : ComponentBase where TValidator : AbstractValidator<T>
    {
        [CascadingParameter] EditContext CurrentEditContext { get; set; }

        protected override void OnInitialized()
        {
            if (CurrentEditContext == null)
            {
                throw new InvalidOperationException($"{nameof(FluentValidationValidator<T, TValidator>)} requires a cascading " +
                    $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(FluentValidationValidator<T, TValidator>)} " +
                    $"inside an {nameof(EditForm)}.");
            }

            CurrentEditContext.AddFluentValidation<T, TValidator>();
        }
    }
}
