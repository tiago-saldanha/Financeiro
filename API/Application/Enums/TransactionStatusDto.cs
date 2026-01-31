using System.ComponentModel;

namespace API.Application.Enums
{
    public enum TransactionStatusDto
    {
        [Description("Pending")]
        Pending,

        [Description("Pending")]
        Paid,

        [Description("Cancelled")]
        Cancelled
    }
}
