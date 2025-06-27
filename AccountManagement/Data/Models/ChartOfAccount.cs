namespace AccountingSystem.Models
{
    public class ChartOfAccount
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public int? ParentId { get; set; }
        public string AccountType { get; set; }
    }
}