using Coderama.Data;
using Coderama.Data.Models;
using Coderama.Services;
using Microsoft.EntityFrameworkCore;

namespace Coderama.Storage
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DbDocumentContext _dbContext;

        public DocumentRepository(DbDocumentContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Document?> GetDocumentAsync(string id)
        {
            var document = await _dbContext.Documents.FirstOrDefaultAsync(i => i.Id == id);

            return document;
        }

        public async Task<Document> CreateDocumentAsync(Document document)
        {
            var result = _dbContext.Documents.AddAsync(document);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(result.Result.Entity);
        }

        public async Task UpdateDocumentAsync(Document document)
        {
            if (await _dbContext.Documents.FindAsync(document.Id) is Document dbDocument)
            {
                _dbContext.Entry(dbDocument).CurrentValues.SetValues(document);

                await _dbContext.SaveChangesAsync();

                return;
            }

            throw new KeyNotFoundException($"Document with ID '{document.Id}' not found.");
        }
    }
}
