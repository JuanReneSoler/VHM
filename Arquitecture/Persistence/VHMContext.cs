using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.Entities;
using Core.Base;

#nullable disable

namespace Persistence
{
    public partial class VHMContext : DbContext
    {
        public VHMContext()
        {
        }

        public VHMContext(DbContextOptions<VHMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductsType> ProductsTypes { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=10.0.0.9;Database=VHM;User Id=sa;Password=Masterxp01;Trusted_Connection=True;Integrated Security=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("Empleado");

                entity.HasIndex(e => e.DocId, "UQ__Empleado__3EF188AC209AE2C9")
                    .IsUnique();

                entity.Property(e => e.DocId)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(76)
                    .IsUnicode(false)
                    .HasComputedColumnSql("(([Names]+' ')+[Surnames])", false);

                entity.Property(e => e.Names)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surnames)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property<bool>("IsDeleted");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PriceUnit).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProveedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__Provee__2E1BDC42");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__TypeId__2F10007B");

                entity.Property<bool>("IsDeleted");
            });

            modelBuilder.Entity<ProductsType>(entity =>
            {
                entity.ToTable("ProductsType");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property<bool>("IsDeleted");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rnc)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("RNC");

                entity.Property<bool>("IsDeleted");
            });

	    modelBuilder.Entity<Empleado>().HasQueryFilter(p => EF.Property<bool>(p, "IsDeleted") == false);
	    modelBuilder.Entity<Product>().HasQueryFilter(p => EF.Property<bool>(p, "IsDeleted") == false);
	    modelBuilder.Entity<ProductsType>().HasQueryFilter(p => EF.Property<bool>(p, "IsDeleted") == false);
	    modelBuilder.Entity<Provider>().HasQueryFilter(p => EF.Property<bool>(p, "IsDeleted") == false);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public override int SaveChanges()
        {
	    SoftDelete();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
	    SoftDelete();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

	private void SoftDelete()
	{
	    foreach(var entry in ChangeTracker.Entries())
	    {
		switch(entry.State)
		{
		    case EntityState.Added:
			entry.CurrentValues["IsDeleted"] = false;
			break;
		    case EntityState.Deleted:
			entry.State = EntityState.Modified;
			entry.CurrentValues["IsDeleted"] = true;
			break;
		}
	    }
	}
        
    }
}
