using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Interfaces.Services
{
    public interface IUserClient : 
        IUserRoleStore<User>,
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserClaimStore<User>,
        IUserLoginStore<User>,
        IUserLockoutStore<User>,
        IUserTwoFactorStore<User>
    {
    }
}
