using System.ComponentModel;

namespace Application.Enums
{
    public enum TransactionStatusDto
    {
        [Description("Pending")]
        Pending,

        [Description("Paid")]
        Paid,

        [Description("Cancelled")]
        Cancelled
    }
}
