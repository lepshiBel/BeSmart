using BeSmart.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Application.Validators
{
    public class AccountTypeValidator : AbstractValidator<AccountType>
    {
        public AccountTypeValidator()
        {
            RuleFor(a => a.Name).NotEmpty().Length(3, 30);
            RuleFor(a => a.Description).Length(3, 150);
        }
    }
}
