using Microsoft.AspNetCore.Mvc.RazorPages;
using AccountingSystem.Services;
using System.Data.SqlClient;
using AccountManagement.Data.Models;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin,Accountant")]
public class AccountsIndexModel : PageModel
{
    private readonly SqlService _sql;
    public List<ChartOfAccount> Accounts { get; set; }

    public AccountsIndexModel(SqlService sql) => _sql = sql;

    public void OnGet()
    {
        Accounts = _sql.ExecuteReader<ChartOfAccount>("sp_ManageChartOfAccounts",
            new SqlParameter("@Action", "SELECT"));
    }
}