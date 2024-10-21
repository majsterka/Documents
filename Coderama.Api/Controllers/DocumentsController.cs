using Coderama.Services;
using Coderama.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Coderama.Api.Controllers
{
    /// <summary>
    /// Document API
    /// </summary>
    /// <param name="documentService"></param>
    [Route("[controller]")]
    [ApiController]
    public class DocumentsController(IDocumentService documentService) : ControllerBase
    {
        private readonly IDocumentService _documentService = documentService;

        /// <summary>
        /// Get document by Id
        /// </summary>
        /// <param name="id">ID of a document</param>
        /// <param name="format">Accept: xml, mpack</param>
        /// <returns>Document</returns>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        public async Task<ContentResult> Get(string id, string format)
        {
            return await _documentService.GetDocumentAsync(id, format);
        }

        /// <summary>
        /// Create new document
        /// </summary>
        /// <param name="model">Data of new document</param>
        /// <returns>New document info</returns>
        /// <response code="400">Bad request</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DocumentModel model)
        {
            return Ok(await _documentService.CreateDocumentAsync(model));
        }

        /// <summary>
        /// Update existing document
        /// </summary>
        /// <param name="id">Document identificator</param>
        /// <param name="model">New document info</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] DocumentModel model)
        {
            await _documentService.UpdateDocumentAsync(id, model);

            return Ok();
        }
    }
}
