using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountingSystem.Services;
using System.Data.SqlClient;
using AccountManagement.Data.Models;

public class VouchersIndexModel : PageModel
{
    private readonly SqlService _sql;

    public List<VoucherDisplay> Vouchers { get; set; }

    public VouchersIndexModel(SqlService sql)
    {
        _sql = sql;
    }

    public void OnGet()
    {
        // Fetch vouchers
        var vouchers = _sql.ExecuteReader<VoucherSummary>("sp_GetVouchers");

        Vouchers = new List<VoucherDisplay>();

        foreach (var v in vouchers)
        {
            var entries = _sql.ExecuteReader<VoucherEntryDisplay>("sp_GetVoucherEntries",
                new SqlParameter("@VoucherId", v.VoucherId));

            Vouchers.Add(new VoucherDisplay
            {
                VoucherId = v.VoucherId,
                VoucherType = v.VoucherType,
                ReferenceNo = v.ReferenceNo,
                VoucherDate = v.VoucherDate,
                Entries = entries.ToList()
            });
        }

    }
}

public class VoucherDisplay : Voucher
{
    public List<VoucherEntryDisplay> Entries { get; set; }
};

public class VoucherEntryDisplay : VoucherEntry
{
    public string AccountName { get; set; }
}

public class VoucherSummary
{
    public int VoucherId { get; set; }
    public string VoucherType { get; set; }
    public string ReferenceNo { get; set; }
    public DateTime VoucherDate { get; set; }
}
