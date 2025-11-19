using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace TheTranslator.Models;

[Table("users")]
public class UserModel : BaseModel
{
    [PrimaryKey("id")]  
    public int Id { get; set; }
    [Column("code")]
    public string Code { get; set; } = string.Empty;
    /* [Column("email")]
    public string Email { get; set; } = string.Empty; */
}
