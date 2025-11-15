using Microsoft.AspNetCore.Mvc;
using Supabase;
using System.Security.Permissions;
using System.ComponentModel.DataAnnotations;
using TheTranslator.Contracts;
using TheTranslator.Models;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

/* get all texts for one user */
app.MapGet("/texts/{user_id}", async (Supabase.Client client, int user_id) =>
{
    var result = await client
        .From<TextModel>()
        .Where(t => t.UserId == user_id)
        .Get();
    var texts = result.Models;

    if (!texts.Any())
        return Results.NotFound();

    var response = texts.Select(t => new TextResponse
    {
        Id = t.Id,
        UserId = t.UserId,
        Title = t.Title,
        Content = t.Content,
        LanguageFrom = t.LanguangeFrom,
        LanguageTo = t.LanguageTo,

    });

    return Results.Ok(response);
});

app.MapPost("/text", async (TextModel req, Supabase.Client client) =>
{
    var text = new TextModel
    {
        UserId = req.UserId,
        Content = req.Content,
        LanguangeFrom = req.LanguangeFrom,
        LanguageTo = req.LanguageTo,
        Title = req.Title
    };
    var response = await client.From<TextModel>().Insert(text);
    
    return Results.Ok(response.Models.First().Id);

});

app.MapDelete("/text/{text_id}", async (int text_id, Supabase.Client client) =>{
    await client
        .From<TextModel>()
        .Where(t => t.Id == text_id)
        .Delete();

    return Results.NoContent();
});

app.MapGet("/words/{text_id}", async (Supabase.Client client, int text_id) =>
{
    var result = await client
        .From<WordModel>()
        .Where(word => word.TextId == text_id)
        .Get();
    var words = result.Models;

    if (!words.Any())
        return Results.NotFound();

    var response = words.Select(w => new WordResponse
    {
        Id = w.Id,
        Word = w.Word,
        IsPinned = w.IsPinned,
        Level = w.Level,

    });

    return Results.Ok(response);
});

app.MapPost("/word", async (WordModel req, Supabase.Client client) =>
{
    var word = new WordModel
    {
        TextId = req.TextId,
        UserCode = req.UserCode,
        Word = req.Word,
        IsPinned = req.IsPinned,
        Level = req.Level
    };
    var response = await client.From<WordModel>().Insert(word);
    
    return Results.Ok(response.Models.First().Id);

});

app.MapPut("/word/{word_id}", async (int word_id, WordModel req, Supabase.Client client) =>
{
    var updatedFields = new WordModel
    {
        IsPinned = req.IsPinned,
        Level = req.Level
    };

    var response = await client
        .From<WordModel>()
        .Where(w => w.Id == word_id)
        .Set(w => w.IsPinned, updatedFields.IsPinned)
        .Set(w => w.Level, updatedFields.Level)
        .Update();

    if (!response.Models.Any())
        return Results.NotFound();

    return Results.Ok(response.Models.First().Id);
});



app.MapDelete("/word/{word_id}", async (int word_id, Supabase.Client client) =>{
    await client
        .From<WordModel>()
        .Where(t => t.Id == word_id)
        .Delete();

    return Results.NoContent();
});

app.MapGet("/wordsReview/{user_code}", async (Supabase.Client client, string user_code) =>
{
    var result = await client
        .From<WordModel>()
        .Where(word => word.UserCode == user_code)
        .Get();
    var words = result.Models;

    if (!words.Any())
        return Results.NotFound();

    var response = words.Select(w => new WordResponse
    {
        Id = w.Id,
        Word = w.Word,
        Level = w.Level,

    });

    return Results.Ok(response);
});



app.MapGet("/", () => Results.Ok());

app.Run();
