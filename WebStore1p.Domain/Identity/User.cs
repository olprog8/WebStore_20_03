using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Identity;


namespace WebStore1p.Domain.Identity
{
    public class User: IdentityUser
    {

        //ПШ сразу определим константы
        public const string Administrator = "Administrators";
        public const string DefaultPassword = "AdminPassword";
    }
}
