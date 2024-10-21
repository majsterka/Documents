using Coderama.Data.Models;

namespace Coderama.Services
{
    public interface IDocumentRepository
    {
        Task<Document?> GetDocumentAsync(string id);

        Task<Document> CreateDocumentAsync(Document document);

        Task UpdateDocumentAsync(Document document);
    }
}
