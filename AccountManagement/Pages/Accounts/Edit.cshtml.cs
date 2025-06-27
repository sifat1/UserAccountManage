using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountingSystem.Models;
using AccountingSystem.Services;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin,Accountant")]
public class EditAccountModel : PageModel
{
    private readonly SqlService _sql;
    public List<ChartOfAccount> Accounts { get; set; }

    [BindProperty]
    public ChartOfAccount Input { get; set; }

    public EditAccountModel(SqlService sql) => _sql = sql;

    public void OnGet(int id)
    {
        Accounts = _sql.ExecuteReader<ChartOfAccount>("sp_ManageChartOfAccounts",
            new SqlParameter("@Action", "SELECT"));

        Input = Accounts.FirstOrDefault(a => a.AccountId == id);
    }

    public IActionResult OnPost()
    {
        var parameters = new[]
        {
            new SqlParameter("@Action", "UPDATE"),
            new SqlParameter("@AccountId", Input.AccountId),
            new SqlParameter("@AccountName", Input.AccountName),
            new SqlParameter("@ParentId", (object?)Input.ParentId ?? DBNull.Value),
            new SqlParameter("@AccountType", Input.AccountType)
        };

        _sql.ExecuteNonQuery("sp_ManageChartOfAccounts", parameters);
        return RedirectToPage("/Accounts/Index");
    }
}