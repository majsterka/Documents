using Coderama.Data.Models;

namespace Coderama.Services.DocumentFormats
{
    public interface IDocumentFormat<T>
    {
        T ConvertTo(Document document);
    }
}
