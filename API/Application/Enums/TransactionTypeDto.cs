using System.ComponentModel;

namespace API.Application.Enums
{
    public enum TransactionTypeDto
    {
        [Description("Revenue")]
        Revenue,

        [Description("Expense")]
        Expense
    }
}
