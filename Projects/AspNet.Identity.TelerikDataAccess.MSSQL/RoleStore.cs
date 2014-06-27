using System;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Identity.TelerikDataAccess.MSSQL.DBEntities;
using Microsoft.AspNet.Identity;

namespace AspNet.Identity.TelerikDataAccess.MSSQL
{
    public class RoleStore : IRoleStore<IdentityRole<int>, int>
    {
        private IdentityModel model;

        public RoleStore()
        {
            this.model = new IdentityModel();
        }

        public RoleStore(IdentityModel model)
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

        public Task CreateAsync(IdentityRole<int> role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            Role dbRole = role.ToDbRole();

            this.model.Add(dbRole);
            this.model.SaveChanges();

            role.Id = dbRole.Id;

            return Task.FromResult<object>(null);
        }

        public Task UpdateAsync(IdentityRole<int> role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            Role dbRole = this.model.Roles.FirstOrDefault(r => r.Id == role.Id);

            if (dbRole != null)
            {
                role.ToDbRole(dbRole);
                this.model.SaveChanges();
            }

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(IdentityRole<int> role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            Role dbRole = this.model.Roles.FirstOrDefault(r => r.Id == role.Id);

            if (dbRole != null)
            {
                this.model.Delete(dbRole);
                this.model.SaveChanges();
            }

            return Task.FromResult<object>(null);
        }

        Task<IdentityRole<int>> IRoleStore<IdentityRole<int>, int>.FindByIdAsync(int roleId)
        {
            IdentityRole<int> result = null;

            Role dbRole = this.model.Roles.FirstOrDefault(r => r.Id == roleId);

            if (dbRole != null)
                result = dbRole.ToIdentityRole();

            return Task.FromResult(result);
        }

        Task<IdentityRole<int>> IRoleStore<IdentityRole<int>, int>.FindByNameAsync(string roleName)
        {
            if (roleName == null)
                throw new ArgumentNullException("roleName");

            IdentityRole<int> role = null;

            Role dbRole = this.model.Roles.FirstOrDefault(r => r.Name == roleName);

            if (dbRole != null)
                role = dbRole.ToIdentityRole();

            return Task.FromResult(role);
        }

    }
}
