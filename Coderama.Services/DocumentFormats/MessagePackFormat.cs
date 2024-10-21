using Coderama.Data.Models;
using MessagePack;

namespace Coderama.Services.DocumentFormats
{
    public class MessagePackFormat : IDocumentFormat<byte[]>
    {
        public byte[] ConvertTo(Document document)
        {
            return MessagePackSerializer.ConvertFromJson(document.DocumentData);
        }
    }
}
