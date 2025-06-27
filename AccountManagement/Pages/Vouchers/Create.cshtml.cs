using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountingSystem.Services;
using AccountingSystem.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;


[Authorize(Roles = "Accountant")]
public class CreateVoucherModel : PageModel
{
    private readonly SqlService _sql;
    public List<ChartOfAccount> Accounts { get; set; }

    [BindProperty]
    public Voucher Input { get; set; } = new() { Entries = new List<VoucherEntry> { new(), new() } };

    public CreateVoucherModel(SqlService sql) => _sql = sql;

    public void OnGet()
    {
        Accounts = _sql.ExecuteReader<ChartOfAccount>("sp_ManageChartOfAccounts",
            new SqlParameter("@Action", "SELECT"));
    }

    public IActionResult OnPost()
    {
        var dt = new DataTable();
        dt.Columns.Add("AccountId", typeof(int));
        dt.Columns.Add("Debit", typeof(decimal));
        dt.Columns.Add("Credit", typeof(decimal));

        foreach (var e in Input.Entries)
        {
            dt.Rows.Add(e.AccountId, e.Debit, e.Credit);
        }

        var parameters = new[]
        {
            new SqlParameter("@VoucherType", Input.VoucherType),
            new SqlParameter("@ReferenceNo", Input.ReferenceNo),
            new SqlParameter("@VoucherDate", Input.VoucherDate),
            new SqlParameter("@Entries", dt)
            {
                SqlDbType = SqlDbType.Structured,
                TypeName = "VoucherEntryType"
            }
        };

        _sql.ExecuteNonQuery("sp_SaveVoucher", parameters);
        return RedirectToPage("/Accounts/Index");
    }
}