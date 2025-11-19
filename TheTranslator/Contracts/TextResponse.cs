using System.ComponentModel.DataAnnotations;

namespace TheTranslator.Contracts;

public class TextResponse
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string UserCode { get; set; } = string.Empty;
    [Required]
    public string Content { get; set; } = string.Empty;
    [Required]
    public string LanguageFrom { get; set; } = string.Empty;
    [Required]
    public string LanguageTo { get; set; } = string.Empty;
    [Required]
    public string Title { get; set; } = string.Empty;
}

