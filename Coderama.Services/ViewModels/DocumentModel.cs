using System.ComponentModel.DataAnnotations;

namespace Coderama.Services.ViewModels
{
    [Serializable]
    public class DocumentModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        public List<string> Tags { get; set; } = null!;

        [Required]
        public Dictionary<string, string> Data { get; set; } = null!;
    }
}
