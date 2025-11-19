using Supabase;
using TheTranslator.Models;
using System.Security.Cryptography;
using System.Text;
using TheTranslator.Contracts;

namespace TheTranslator.Services;


public class UserService
{
    private readonly Client _client;

    public UserService(Client client)
    {
        _client = client;
    }

    private static string GenerateUserCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var bytes = RandomNumberGenerator.GetBytes(8);
        var result = new StringBuilder(8);

        foreach (var b in bytes)
        {
            result.Append(chars[b % chars.Length]);
        }

        return result.ToString();
    }

    public async Task<IEnumerable<UserResponse>> CreateUser()
    {
        var generatedCode = GenerateUserCode();

        var user = new UserModel
        {
            Code = generatedCode
        };

        var response = await _client.From<UserModel>().Insert(user);
        return response.Models.Select(u => new UserResponse
        {
            Code = u.Code,

        });
    }
    public async Task<Boolean> CheckCode(string code)
    {
        var response = await _client.From<UserModel>()
            .Where(x => x.Code == code)
            .Get();

        return response.Models.Any();

    }
}
