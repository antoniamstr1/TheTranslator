namespace TheTranslator.Contracts;
using System.ComponentModel.DataAnnotations;

using TheTranslator.Enums;


public record MarkedWordResponse
{
    public int Id { get; set; }
    public int TextId { get; set; }
    public string UserCode {get; set;} = string.Empty;
    [Required]    
    public string Word { get; set; } = string.Empty;
    public bool? IsPinned { get; set; }
    public WordLevel? Level { get; set; }
}

