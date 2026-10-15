
using BasicBankingAccountApi.Data;
using BasicBankingAccountApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BasicBankingAccountApi.Controllers;

[ApiController]
[Route("api/accounts")]
public class BankAccountsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BankAccountsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/accounts?accountType=Savings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BankAccount>>> GetAccounts(
        [FromQuery] string? accountType)
    {
        var query = _context.BankAccounts.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(accountType))
        {
            if (accountType != "Savings" && accountType != "Current")
            {
                return BadRequest(
                    "AccountType must be Savings or Current.");
            }

            query = query.Where(a => a.AccountType == accountType);
        }

        var accounts = await query
            .OrderBy(a => a.Id)
            .ToListAsync();

        return Ok(accounts);
    }

    // GET: api/accounts/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BankAccount>> GetAccount(int id)
    {
        var account = await _context.BankAccounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        if (account == null)
        {
            return NotFound(new { message = "Account not found." });
        }

        return Ok(account);
    }

    // POST: api/accounts
    [HttpPost]
    public async Task<ActionResult<BankAccount>> CreateAccount(
        CreateAccountRequest request)
    {
        var account = new BankAccount
        {
            AccountHolderName = request.AccountHolderName,
            AccountNumber = request.AccountNumber,
            Balance = request.Balance,
            AccountType = request.AccountType
        };

        _context.BankAccounts.Add(account);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            return Conflict(new
            {
                message = "An account with this account number already exists."
            });
        }

        return CreatedAtAction(
            nameof(GetAccount),
            new { id = account.Id },
            account);
    }

    // PUT: api/accounts/1
    [HttpPut("{id:int}")]
    public async Task<ActionResult<BankAccount>> UpdateAccount(
        int id,
        UpdateAccountRequest request)
    {
        var account = await _context.BankAccounts.FindAsync(id);

        if (account == null)
        {
            return NotFound(new { message = "Account not found." });
        }

        // Only permitted fields are updated.
        // The existing balance is deliberately preserved.
        account.AccountHolderName = request.AccountHolderName;
        account.AccountNumber = request.AccountNumber;
        account.AccountType = request.AccountType;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (IsUniqueViolation(ex))
        {
            return Conflict(new
            {
                message = "An account with this account number already exists."
            });
        }

        return Ok(account);
    }

    // DELETE: api/accounts/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAccount(int id)
    {
        var account = await _context.BankAccounts.FindAsync(id);

        if (account == null)
        {
            return NotFound(new { message = "Account not found." });
        }

        _context.BankAccounts.Remove(account);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/accounts/1/deposit
    [HttpPost("{id:int}/deposit")]
    public async Task<ActionResult<BankAccount>> Deposit(
        int id,
        AmountRequest request)
    {
        var account = await _context.BankAccounts.FindAsync(id);

        if (account == null)
        {
            return NotFound(new { message = "Account not found." });
        }

        if (request.Amount <= 0)
        {
            return BadRequest(
                "Deposit amount must be greater than zero.");
        }

        if (request.Amount > 9999999999999999.99m - account.Balance)
        {
            return BadRequest("The resulting balance is too large.");
        }

        account.Balance += request.Amount;

        await _context.SaveChangesAsync();

        return Ok(account);
    }

    // POST: api/accounts/1/withdraw
    [HttpPost("{id:int}/withdraw")]
    public async Task<ActionResult<BankAccount>> Withdraw(
        int id,
        AmountRequest request)
    {
        var account = await _context.BankAccounts.FindAsync(id);

        if (account == null)
        {
            return NotFound(new { message = "Account not found." });
        }

        if (request.Amount <= 0)
        {
            return BadRequest(
                "Withdrawal amount must be greater than zero.");
        }

        if (request.Amount > account.Balance)
        {
            return BadRequest("Insufficient balance.");
        }

        account.Balance -= request.Amount;

        await _context.SaveChangesAsync();

        return Ok(account);
    }

    private static bool IsUniqueViolation(DbUpdateException ex)
    {
        return ex.GetBaseException() is SqlException sqlException
            && (sqlException.Number == 2601
                || sqlException.Number == 2627);
    }
}

// Request body for creating an account
public class CreateAccountRequest
{
    [Required]
    [StringLength(100)]
    public string AccountHolderName { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string AccountNumber { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "9999999999999999.99")]
    public decimal Balance { get; set; }

    [Required]
    [RegularExpression("^(Savings|Current)$",
        ErrorMessage = "AccountType must be Savings or Current.")]
    public string AccountType { get; set; } = "Savings";
}

// Request body for updating permitted account details
public class UpdateAccountRequest
{
    [Required]
    [StringLength(100)]
    public string AccountHolderName { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string AccountNumber { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^(Savings|Current)$",
        ErrorMessage = "AccountType must be Savings or Current.")]
    public string AccountType { get; set; } = "Savings";
}

// Request body for deposits and withdrawals
public class AmountRequest
{
    [Range(typeof(decimal), "0.01", "9999999999999999.99")]
    public decimal Amount { get; set; }
}
