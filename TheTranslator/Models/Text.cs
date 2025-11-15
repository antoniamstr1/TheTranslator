using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace TheTranslator.Models;

[Table("texts")]
public class TextModel : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    [Column("user_id")]
    public int UserId { get; set; }
    [Column("content")]
    public string Content { get; set; } = string.Empty;
    [Column("languange_from")]
    public string LanguangeFrom { get; set; } = string.Empty;
    [Column("language_to")]
    public string LanguageTo { get; set; } = string.Empty;
    [Column("title")]
    public string Title { get; set; } = string.Empty;
}
