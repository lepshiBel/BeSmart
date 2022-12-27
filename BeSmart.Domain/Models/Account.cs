using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models;

public class Account : EntityBase
{
    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public string PasswordHash { get; set; }

    public int AccountTypeId { get; set; }
    
    public AccountType AccountType { get; set; }
}