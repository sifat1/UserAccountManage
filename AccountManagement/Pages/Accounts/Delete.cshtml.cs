using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountingSystem.Services;
using System.Data.SqlClient;
using AccountManagement.Data.Models;
using Microsoft.AspNetCore.Authorization;


[Authorize(Roles = "Admin,Accountant")]
public class DeleteAccountModel : PageModel
{
    private readonly SqlService _sql;

    [BindProperty]
    public ChartOfAccount Input { get; set; }

    public DeleteAccountModel(SqlService sql) => _sql = sql;

    public void OnGet(int id)
    {
        var result = _sql.ExecuteReader<ChartOfAccount>("sp_ManageChartOfAccounts",
            new SqlParameter("@Action", "SELECT"));

        Input = result.FirstOrDefault(a => a.AccountId == id);
    }

    public IActionResult OnPost()
    {
        _sql.ExecuteNonQuery("sp_ManageChartOfAccounts",
            new SqlParameter("@Action", "DELETE"),
            new SqlParameter("@AccountId", Input.AccountId));

        return RedirectToPage("/Accounts/Index");
    }
}