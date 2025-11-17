namespace TheTranslator.Contracts;

public class WordForm
{
    public string Word{ get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
}
public class WordAnalysisResponse
{
    public string Type { get; set; } = string.Empty;

    public string? GramaticalNumber { get; set; } = string.Empty;

    public string? Gender { get; set; } = string.Empty;

    public WordForm Forms { get; set; } = new();

    public List<string> Definitions { get; set; } = new();
}