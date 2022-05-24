using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RegistrationCertificateAPI.Data
{
    public class CertificateContextDB : IdentityDbContext<User>
    {
        public DbSet<RegistrationCertificate> Certificates { get; set; }

        public CertificateContextDB(DbContextOptions<CertificateContextDB> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
