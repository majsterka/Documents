using Coderama.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Coderama.Data;

public partial class DbDocumentContext : DbContext
{
    public DbDocumentContext()
    {
    }

    public DbDocumentContext(DbContextOptions<DbDocumentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Document> Documents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
