using Coderama.Data.Models;
using Coderama.Services.ViewModels;
using Newtonsoft.Json;

namespace Coderama.Services.DocumentFormats
{
    public class XmlFormat : IDocumentFormat<string>
    {
        public string ConvertTo(Document document)
        {
            DocumentModel? model = JsonConvert.DeserializeObject<DocumentModel>(document.DocumentData);

            if(model == null)
            {
                throw new KeyNotFoundException("Deserialization failed. Document model is empty.");
            }

            //add root object
            RootData input = new RootData()
            {
                root = model
            };

            string json = JsonConvert.SerializeObject(input);

            var result = JsonConvert.DeserializeXmlNode(json);
            if (result == null)
            {
                throw new KeyNotFoundException("Deserialization to xml failed. Result is empty.");
            }

            return result.InnerXml;
        }

        public class RootData
        {
            public required DocumentModel root;
        }
    }
}
