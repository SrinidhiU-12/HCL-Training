
using System.ComponentModel.DataAnnotations;

namespace BasicBankingAccountApi.Models;

public class BankAccount
{
    public int Id { get; set; }

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
