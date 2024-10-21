using Coderama.Data;
using Coderama.Data.Models;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;

namespace Coderama.Test
{
    public class DocumentRepositoryMock
    {
        internal static DbContextMock<DbDocumentContext> GetDbContext(List<Document> initialEntities)
        {
            DbContextMock<DbDocumentContext> dbContextMock = new DbContextMock<DbDocumentContext>(new DbContextOptionsBuilder<DbDocumentContext>().Options);
            dbContextMock.CreateDbSetMock(x => x.Documents, initialEntities);
            return dbContextMock;
        }

        public static List<Document> GetFakeDocumentList()
        {
            return new List<Document>()
            {
                new Document
                {
                    Id = "1",
                    DocumentData = "{\"Id\":\"1\",\"Tags\":[\"important\",\".net\"],\"Data\":{\"some\":\"data\",\"optional\":\"fields\"}}"
                }
            };
        }
    }
}
