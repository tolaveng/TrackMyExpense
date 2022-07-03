using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Enums
{
    public enum PaymentMethod
    {
        Card,
        Cash,
        
        [Display(Name = "Direct Debit")]
        DirectDebit
    }
}
