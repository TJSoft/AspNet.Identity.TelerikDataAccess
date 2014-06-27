using System;
using Microsoft.AspNet.Identity;

namespace AspNet.Identity.TelerikDataAccess.MSSQL
{
    public class IdentityUser<TKey> : IUser<TKey>
    {
        public TKey Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }

        public IdentityUser()
        {
        }

        public IdentityUser(TKey id)
        {
            this.Id = id;
        }
    }
}
