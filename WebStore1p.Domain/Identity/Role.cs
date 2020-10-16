using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Identity;

namespace WebStore1p.Domain.Identity
{
    public class Role : IdentityRole
    {
        //ПШ сразу определим константу
        public const string Administrator = "Administrators";

        //ПШ L7 определим константу ролей пользователя
        public const string User = "Users"; 
    }
}
