using Supabase;
using TheTranslator.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:8080"); 

//supabase
var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");


builder.Services.AddScoped<Supabase.Client>(_ =>
    new Supabase.Client(
        url,
        key,
        new SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true
        }));

builder.Services.AddScoped<TextService>();
builder.Services.AddScoped<MarkedWordService>();
builder.Services.AddHttpClient<VerbConjugationService>();
builder.Services.AddScoped<VerbConjugationService>();
builder.Services.AddScoped<WordAnalysisService>();



builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => Results.Ok());


app.Run();




