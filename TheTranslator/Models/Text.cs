using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace TheTranslator.Models;

[Table("texts")]
public class TextModel : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    [Column("user_code")]
    public string UserCode { get; set; } = string.Empty;
    [Column("content")]
    public string Content { get; set; } = string.Empty;
    [Column("language_from")]
    public string LanguageFrom { get; set; } = string.Empty;
    [Column("language_to")]
    public string LanguageTo { get; set; } = string.Empty;
    [Column("title")]
    public string Title { get; set; } = string.Empty;
}
