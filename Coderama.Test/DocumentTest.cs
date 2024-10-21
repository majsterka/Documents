using Coderama.Data;
using Coderama.Data.Models;
using Coderama.Services;
using Coderama.Storage;
using EntityFrameworkCoreMock;

namespace Coderama.Test
{
    public class DocumentTest
    {
        private IDocumentRepository _repository;

        private static DocumentRepository DocumentRepositoryInit(DbContextMock<DbDocumentContext> dbContextMock)
        {
            return new DocumentRepository(dbContextMock.Object);
        }

        [Fact]
        public void GetDocument()
        {
            //arrange
            DbContextMock<DbDocumentContext> dbContextMock = DocumentRepositoryMock.GetDbContext(DocumentRepositoryMock.GetFakeDocumentList());
            _repository = DocumentRepositoryInit(dbContextMock);

            //act
            Document? result = _repository.GetDocumentAsync("1").Result;

            //assert
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
        }

        [Fact]
        public void CreateDocument()
        {
            //arrange
            DbContextMock<DbDocumentContext> dbContextMock = DocumentRepositoryMock.GetDbContext(DocumentRepositoryMock.GetFakeDocumentList());
            _repository = DocumentRepositoryInit(dbContextMock);

            //act
            Document doc = new Document()
            {
                Id = "2",
                DocumentData = "{\"Id\":\"2\",\"Tags\":[\"important\",\".net\", \"code\"],\"Data\":{\"1\":\"one\",\"optional\":\"fields\"}"
            };
            _repository.CreateDocumentAsync(doc);
            Document? result = _repository.GetDocumentAsync("2").Result;

            //assert
            Assert.NotNull(result);
            Assert.Equal("2", result.Id);
            Assert.Contains("code", result.DocumentData);
            Assert.DoesNotContain("some", result.DocumentData);
        }
    }
}