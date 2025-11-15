using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using TheTranslator.Enums;

namespace TheTranslator.Models;

[Table("words")]
public class WordModel : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    [Column("user_code")]
    public string UserCode { get; set; } = string.Empty;
    [Column("text_id")]
    public int TextId { get; set; }
    [Column("word")]
    public string Word { get; set; } = string.Empty;
    [Column("is_pinned")]
    public bool IsPinned { get; set; }
    [Column("level")]
    public WordLevel Level { get; set; }
}
