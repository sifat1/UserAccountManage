namespace AccountManagement.Data.Models
{
    public class VoucherEntry
    {
        public int AccountId { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}