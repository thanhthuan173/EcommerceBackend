namespace BeverageBackend.Models
{
    public enum OrderStatus
    {
        PendingPayment = 0,
        Paid = 1,
        Packaging = 2,
        Shipping = 3,
        Completed = 4,
        Cancelled = 5
    }
}
