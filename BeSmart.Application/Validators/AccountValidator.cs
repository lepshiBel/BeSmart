using BeSmart.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Application.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(a => a.UserName).NotEmpty().Length(3, 50).Matches("^[a-z0-9_-]{3,50}$");
            RuleFor(a => a.UserEmail).NotEmpty().EmailAddress();
            RuleFor(a=>a.UserPassword).NotEmpty().Length(3, 100).Matches("^[a-z0-9_-]{3,100}$");
            RuleFor(a => a.AccountTypeId).NotNull();
            RuleFor(a=>a.AccountType).SetValidator(new AccountTypeValidator());
        }
    }
}
