using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountingSystem.Models;
using AccountingSystem.Services;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;



public class CreateAccountModel : PageModel
{
    private readonly SqlService _sql;
    public List<ChartOfAccount> Accounts { get; set; }

    [BindProperty]
    public ChartOfAccount Input { get; set; }

    public CreateAccountModel(SqlService sql) => _sql = sql;

    public void OnGet()
    {
        Accounts = _sql.ExecuteReader<ChartOfAccount>("sp_ManageChartOfAccounts",
            new SqlParameter("@Action", "SELECT"));
    }

    public IActionResult OnPost()
    {
        var parameters = new[]
        {
            new SqlParameter("@Action", "INSERT"),
            new SqlParameter("@AccountName", Input.AccountName),
            new SqlParameter("@ParentId", (object?)Input.ParentId ?? DBNull.Value),
            new SqlParameter("@AccountType", Input.AccountType)
        };

        _sql.ExecuteNonQuery("sp_ManageChartOfAccounts", parameters);
        return RedirectToPage("/Accounts/Index");
    }
}