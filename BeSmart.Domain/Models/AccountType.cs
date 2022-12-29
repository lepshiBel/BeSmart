using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models;

public class AccountType : EntityBase
{
    public string Name { get; set; }

    public string Description { get; set; }
}