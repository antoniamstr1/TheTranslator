using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace TheTranslator.Models;

[Table("texts")]
public class TextModel : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    [Column("UserId")]
    public int UserId { get; set; }
    [Column("Content")]
    public string Content { get; set; } = string.Empty;
    [Column("LanguangeFrom")]
    public string LanguangeFrom { get; set; } = string.Empty;
    [Column("LanguageTo")]
    public string LanguageTo { get; set; } = string.Empty;
    [Column("Title")]
    public string Title { get; set; } = string.Empty;
}
