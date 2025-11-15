using System.ComponentModel.DataAnnotations;

namespace TheTranslator.Contracts;

    public class TextResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public string LanguageFrom { get; set; } = string.Empty;
        [Required]
        public string LanguageTo { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }

