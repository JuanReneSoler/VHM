using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Persistence;

public class IdentityContext : IdentityDbContext
{
    public IdentityContext()
    {}

    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {}
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("Server=10.0.0.9;Database=VHM;User Id=sa;Password=Masterxp01;Trusted_Connection=True;Integrated Security=False");
        }
    }
}
