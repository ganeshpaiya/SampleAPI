namespace LookUpData.Data
{
    using LookUpData.Models;
    using Microsoft.EntityFrameworkCore;
    public class LookUpDbContext : DbContext
    {
        public DbSet<LookUp> LookUps { get; set; }
        public DbSet<LookUpType> LookUpTypes { get; set; }
        public DbSet<CascadingLookUp> CascadingLookUps { get; set; }
        public LookUpDbContext(DbContextOptions<LookUpDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CascadingLookUp>()
                .HasOne(Cl => Cl.ParentLookUp)
                .WithMany(L => L.ParentInCascadingLookUps)
                .HasForeignKey(Cl => Cl.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CascadingLookUp>()
                .HasOne(Cl => Cl.ChildLookUp)
                .WithMany(L => L.ChildInCascadingLookUps)
                .HasForeignKey(Cl => Cl.ChildId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
