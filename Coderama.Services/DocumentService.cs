using Coderama.Data.Models;
using Coderama.Services.DocumentFormats;
using Coderama.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Coderama.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentFormat<string> _xmlFormat;
        private readonly IDocumentFormat<byte[]> _messagePackFormat;

        public DocumentService(IDocumentRepository documentRepository, IDocumentFormat<string> xmlFormat, IDocumentFormat<byte[]> messagePackFormat)
        {
            _documentRepository = documentRepository;
            _xmlFormat = xmlFormat;
            _messagePackFormat = messagePackFormat;
        }

        public async Task<ContentResult> GetDocumentAsync(string id, string format)
        {
            var document = await _documentRepository.GetDocumentAsync(id);
            if (document == null)
            {
                throw new KeyNotFoundException($"Given ID '{id}' not found.");
            }

            switch (format.ToLowerInvariant())
            {
                case "xml":
                    var xml = _xmlFormat.ConvertTo(document);
                    return new ContentResult
                    {
                        ContentType = "application/xml",
                        Content = xml,
                        StatusCode = 200
                    };
                case "mpack":
                case "messagepack":
                    var res = _messagePackFormat.ConvertTo(document);
                    return new ContentResult
                    {
                        ContentType = "application/x-msgpack",
                        Content = Encoding.UTF8.GetString(res),
                        StatusCode = 200
                    };
                default:
                    return new ContentResult
                    {
                        ContentType = "text/plain",
                        Content = "Format not supported",
                        StatusCode = 400
                    };
            }
        }

        public async Task<Document> CreateDocumentAsync(DocumentModel model)
        {
            // Validate
            var Document = await _documentRepository.GetDocumentAsync(model.Id);
            if (Document != null)
            {
                throw new ValidationException($"Document with given ID '{model.Id}' already exists.");
            }

            Document document = new Document()
            {
                Id = model.Id,
                DocumentData = JsonConvert.SerializeObject(model)
            };

            return await _documentRepository.CreateDocumentAsync(document);
        }
       
        public async Task UpdateDocumentAsync(string id, DocumentModel model)
        {
            if(id != model.Id)
            {
                throw new ArgumentException("Id and Id in data are not the same. You can not change Id.");
            }

            Document document = new Document()
            {
                Id = id,
                DocumentData = JsonConvert.SerializeObject(model)
            };

            await _documentRepository.UpdateDocumentAsync(document);

            return;
        }
    }
}
