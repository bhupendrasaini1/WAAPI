namespace WAAPI.Helpers
{
    public class UserPurchases
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal PurchaseAmount { get; set; }
        public string? Store { get; set; }

    }
}
