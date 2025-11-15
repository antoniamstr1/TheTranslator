using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using TheTranslator.Enums;

namespace TheTranslator.Models;

[Table("words")]
public class WordModel : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    [Column("UserCode")]
    public string UserCode { get; set; } = string.Empty;
    [Column("TextId")]
    public int TextId { get; set; }
    [Column("Word")]
    public string Word { get; set; } = string.Empty;
    [Column("is_pinned")]
    public bool IsPinned { get; set; }
    [Column("Level")]
    public WordLevel Level { get; set; }
}
