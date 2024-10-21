using System.ComponentModel.DataAnnotations;

namespace Coderama.Data.Models;

public partial class Document
{
    [Key]
    [StringLength(30)]
    public string Id { get; set; } = null!;

    [MaxLength()]
    public string DocumentData { get; set; } = null!;
}
