using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Identity.TelerikDataAccess.MSSQL.DBEntities;
using Microsoft.AspNet.Identity;
using Telerik.OpenAccess;

namespace AspNet.Identity.TelerikDataAccess.MSSQL
{
    public class UserStore :
        IUserStore<IdentityUser<int>, int>,
        IUserPasswordStore<IdentityUser<int>, int>,
        IUserSecurityStampStore<IdentityUser<int>, int>,
        IUserRoleStore<IdentityUser<int>, int>,
        IUserClaimStore<IdentityUser<int>, int>
    {
        private IdentityModel model;

        public UserStore()
        {
            this.model = new IdentityModel();
        }

        public UserStore(IdentityModel model)
        {
            this.model = model;
        }

        public void Dispose()
        {
            if (this.model != null)
            {
                this.model.Dispose();
                this.model = null;
            }  
        }

        public Task CreateAsync(IdentityUser<int> user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            User dbUser = user.ToDbUser();

            this.model.Add(dbUser);
            this.model.SaveChanges();

            user.Id = dbUser.Id;

            return Task.FromResult<object>(null);
        }

        public Task UpdateAsync(IdentityUser<int> user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            User dbUser = this.model.Users.FirstOrDefault(u => u.Id == user.Id);

            if (dbUser != null)
            {
                user.ToDbUser(dbUser);
                this.model.SaveChanges();
            }

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(IdentityUser<int> user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            User dbUser = this.model.Users.FirstOrDefault(u => u.Id == user.Id);

            if (dbUser != null)
            {
                this.model.Delete(dbUser);
                this.model.SaveChanges();
            }

            return Task.FromResult<object>(null);
        }

        Task<IdentityUser<int>> IUserStore<IdentityUser<int>, int>.FindByIdAsync(int userId)
        {
            IdentityUser<int> result = null;

            User dbUser = this.model.Users.FirstOrDefault(u => u.Id == userId);

            if (dbUser != null)
                result = dbUser.ToIdentityUser();

            return Task.FromResult(result);
        }

        Task<IdentityUser<int>> IUserStore<IdentityUser<int>, int>.FindByNameAsync(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }

            IdentityUser<int> user = null;

            User dbUser = this.model.Users.FirstOrDefault(u => u.UserName == userName);

            if (dbUser != null)
                user = dbUser.ToIdentityUser();

            return Task.FromResult(user);
        }

        public Task SetPasswordHashAsync(IdentityUser<int> user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.PasswordHash = passwordHash;

            return Task.FromResult<object>(null);
        }

        public Task<string> GetPasswordHashAsync(IdentityUser<int> user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            string result = null;

            User dbUser = this.model.Users.FirstOrDefault(u => u.Id == user.Id);

            if (dbUser != null)
                result = dbUser.PasswordHash;

            return Task.FromResult(result);
        }

        public Task<bool> HasPasswordAsync(IdentityUser<int> user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            bool result = false;

            User dbUser = this.model.Users.FirstOrDefault(u => u.Id == user.Id);

            if (dbUser != null)
                result = !String.IsNullOrWhiteSpace(dbUser.PasswordHash);

            return Task.FromResult(result);
        }

        public Task SetSecurityStampAsync(IdentityUser<int> user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            user.SecurityStamp = stamp;

            return Task.FromResult<object>(null);
        }

        public Task<string> GetSecurityStampAsync(IdentityUser<int> user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            string result = null;

            User dbUser = this.model.Users.FirstOrDefault(u => u.Id == user.Id);

            if (dbUser != null)
                result = dbUser.SecurityStamp;

            return Task.FromResult(result);
        }

        public Task AddToRoleAsync(IdentityUser<int> user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (String.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            User dbUser = this.model.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == user.Id);

            Role dbRole =
                this.model.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));

            if (dbUser != null && dbRole != null && !dbUser.Roles.Contains(dbRole))
            {
                dbUser.Roles.Add(dbRole);
                this.model.SaveChanges();
            }

            return Task.FromResult<object>(null);
        }

        public Task RemoveFromRoleAsync(IdentityUser<int> user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (String.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            User dbUser = this.model.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == user.Id);

            Role dbRole =
                this.model.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));

            if (dbUser != null && dbRole != null && dbUser.Roles.Contains(dbRole))
            {
                dbUser.Roles.Remove(dbRole);
                this.model.SaveChanges();
            }

            return Task.FromResult<object>(null);
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser<int> user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            IList<string> result = new List<string>();

            User dbUser = this.model.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == user.Id);

            if (dbUser != null)
            {
                foreach (var r in dbUser.Roles)
                    result.Add(r.Name);
            }

            return Task.FromResult(result);
        }

        public Task<bool> IsInRoleAsync(IdentityUser<int> user, string roleName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (String.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            bool result = false;

            User dbUser = this.model.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == user.Id);

            if (dbUser != null)
            {
                if (dbUser.Roles.Count(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)) > 0)
                    result = true;
            }

            return Task.FromResult(result);
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser<int> user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            IList<Claim> result = new List<Claim>();

            User dbUser = this.model.Users.Include(u => u.UserClaims).FirstOrDefault(u => u.Id == user.Id);

            if (dbUser != null)
            {
                foreach (var c in dbUser.UserClaims)
                    result.Add(new Claim(c.ClaimType, c.ClaimValue));
            }

            return Task.FromResult(result);
        }

        public Task AddClaimAsync(IdentityUser<int> user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            User dbUser = this.model.Users.Include(u => u.UserClaims).FirstOrDefault(u => u.Id == user.Id);

            if (dbUser != null)
            {
                dbUser.UserClaims.Add(new UserClaim()
                {
                    User = dbUser,
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                });
                this.model.SaveChanges();
            }

            return Task.FromResult<object>(null);
        }

        public Task RemoveClaimAsync(IdentityUser<int> user, Claim claim)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (claim == null)
                throw new ArgumentNullException("claim");

            UserClaim dbUserClaim =
                this.model.UserClaims.Include(c => c.User)
                .FirstOrDefault(c => c.UserId == user.Id && c.ClaimType == claim.Type && c.ClaimValue == claim.Value);

            if (dbUserClaim != null)
            {
                dbUserClaim.User.UserClaims.Remove(dbUserClaim);
                this.model.Delete(dbUserClaim);

                this.model.SaveChanges();
            }

            return Task.FromResult<object>(null);
        }
    }
}
