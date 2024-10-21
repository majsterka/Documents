using Coderama.Data.Models;
using Coderama.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Coderama.Services
{
    public interface IDocumentService
    {
        Task<ContentResult> GetDocumentAsync(string id, string format);

        Task<Document> CreateDocumentAsync(DocumentModel model);

        Task UpdateDocumentAsync(string id, DocumentModel model);
    }
}
