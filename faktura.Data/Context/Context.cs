using System.Data.Entity;
using faktura.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;


namespace faktura.Data.Context
{
    public class Context :IdentityDbContext<ApplicationUser>
    {
        public Context() : base(nameof(Context))
        {}

        public DbSet<Models.Faktura> Fakture { get; set; }
        public DbSet<StavkeFakture> StavkeFakture { get; set; }


        public static Context Create()
        {
            return new Context();
        }
    }
    public class MyDbContext : DbContext
    {
        public MyDbContext():base("DefaultConnection")
        {

        }
        public DbSet<Models.Faktura> Fakture { get; set; }
        public DbSet<StavkeFakture> StavkeFakture { get; set; }
    }
}
