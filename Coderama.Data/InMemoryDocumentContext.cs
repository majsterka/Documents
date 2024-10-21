using Coderama.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Coderama.Data;

public partial class InMemoryDocumentContext : DbContext
{
    public InMemoryDocumentContext()
    {
    }

    public InMemoryDocumentContext(DbContextOptions<InMemoryDocumentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Document> Documents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "DocumentsDb");
    }
}
