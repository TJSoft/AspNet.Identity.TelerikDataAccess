using Microsoft.AspNet.Identity;

namespace AspNet.Identity.TelerikDataAccess.MSSQL
{
    public class IdentityRole<TKey> : IRole<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }

        public IdentityRole()
        {
        }

        public IdentityRole(TKey id)
        {
            this.Id = id;
        }
    }
}
