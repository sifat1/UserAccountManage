namespace AccountManagement.Data.Models
{
    public class Voucher
    {
        public int VoucherId { get; set; }    
        public string VoucherType { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public List<VoucherEntry> Entries { get; set; }
    }
}