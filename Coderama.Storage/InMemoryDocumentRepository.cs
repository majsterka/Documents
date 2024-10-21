using Coderama.Data;
using Coderama.Data.Models;
using Coderama.Services;
using Microsoft.EntityFrameworkCore;

namespace Coderama.Storage
{
    public class InMemoryDocumentRepository : IDocumentRepository
    {
        private readonly InMemoryDocumentContext _inMemoryContext;

        public InMemoryDocumentRepository(InMemoryDocumentContext dbContext)
        {
            _inMemoryContext = dbContext;
        }

        public async Task<Document?> GetDocumentAsync(string id)
        {
            var document = await _inMemoryContext.Documents.FirstOrDefaultAsync(i => i.Id == id);

            return document;
        }

        public async Task<Document> CreateDocumentAsync(Document document)
        {
            var result = _inMemoryContext.Documents.AddAsync(document);
            await _inMemoryContext.SaveChangesAsync();

            return await Task.FromResult(result.Result.Entity);
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            if (await _inMemoryContext.Documents.FindAsync(document.Id) is Document dbDocument)
            {
                _inMemoryContext.Entry(dbDocument).CurrentValues.SetValues(document);

                await _inMemoryContext.SaveChangesAsync();

                return;
            }

            throw new KeyNotFoundException($"Document with ID '{document.Id}' not found.");
        }
    }
}
