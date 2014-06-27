using AspNet.Identity.TelerikDataAccess.MSSQL.DBEntities;

namespace AspNet.Identity.TelerikDataAccess.MSSQL
{
    internal static class MappingExtensions
    {
        /// <summary>
        /// Maps an IdentityUser to a database user
        /// </summary>
        /// <param name="identityUser">The IdentityUser to map</param>
        /// <param name="dbUser">The database user to map to. If null, a new database user is created. When updating, an existing database user should be specified</param>
        /// <returns>The mapped database user</returns>
        public static User ToDbUser(this IdentityUser<int> identityUser, User dbUser = null)
        {
            if(dbUser == null)
                dbUser = new User();

            dbUser.Id = identityUser.Id;
            dbUser.Email = identityUser.Email;
            dbUser.EmailConfirmed = identityUser.EmailConfirmed;
            dbUser.PasswordHash = identityUser.PasswordHash;
            dbUser.SecurityStamp = identityUser.SecurityStamp;
            dbUser.PhoneNumber = identityUser.PhoneNumber;
            dbUser.PhoneNumberConfirmed = identityUser.PhoneNumberConfirmed;
            dbUser.TwoFactorEnabled = identityUser.TwoFactorEnabled;
            dbUser.LockoutEndDateUtc = identityUser.LockoutEndDateUtc;
            dbUser.LockoutEnabled = identityUser.LockoutEnabled;
            dbUser.AccessFailedCount = identityUser.AccessFailedCount;
            dbUser.UserName = identityUser.UserName;

            return dbUser;
        }

        /// <summary>
        /// Maps a database user to an IdentityUser
        /// </summary>
        /// <param name="dbUser">The database user to map</param>
        /// <returns>The mapped IdentityUser</returns>
        public static IdentityUser<int> ToIdentityUser(this User dbUser)
        {
            IdentityUser<int> identityUser = new IdentityUser<int>(dbUser.Id);
            identityUser.Email = dbUser.Email;
            identityUser.EmailConfirmed = dbUser.EmailConfirmed;
            identityUser.PasswordHash = dbUser.PasswordHash;
            identityUser.SecurityStamp = dbUser.SecurityStamp;
            identityUser.PhoneNumber = dbUser.PhoneNumber;
            identityUser.PhoneNumberConfirmed = dbUser.PhoneNumberConfirmed;
            identityUser.TwoFactorEnabled = dbUser.TwoFactorEnabled;
            identityUser.LockoutEndDateUtc = dbUser.LockoutEndDateUtc;
            identityUser.LockoutEnabled = dbUser.LockoutEnabled;
            identityUser.AccessFailedCount = dbUser.AccessFailedCount;
            identityUser.UserName = dbUser.UserName;

            return identityUser;
        }

        public static Role ToDbRole(this IdentityRole<int> identityRole, Role dbRole = null)
        {
            if(dbRole == null)
                dbRole = new Role();

            dbRole.Id = identityRole.Id;
            dbRole.Name = identityRole.Name;

            return dbRole;
        }

        public static IdentityRole<int> ToIdentityRole(this Role dbRole)
        {
            IdentityRole<int> identityRole = new IdentityRole<int>(dbRole.Id);
            identityRole.Name = dbRole.Name;

            return identityRole;
        }
    }
}
