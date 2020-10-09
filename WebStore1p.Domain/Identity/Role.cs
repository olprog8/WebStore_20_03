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
    }
}
