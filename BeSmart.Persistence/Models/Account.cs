namespace BeSmart.Persistence.Models;

public class Account
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public string PasswordHash { get; set; }

    public int AccountTypeId { get; set; }
    
    public AccountType AccountType { get; set; }
}