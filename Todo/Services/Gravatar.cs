using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Todo.Services
{
    public static class Gravatar
    {
        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public static async Task<string> GetNameFromProfile(string emailAddress)
        {
            var hash = GetHash(emailAddress);

            var client = new HttpClient();  
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");             

            var Url = $"https://www.gravatar.com/{hash}.json";

            var response = await client.GetAsync(Url);            
            response.EnsureSuccessStatusCode();

            var json = JObject.Parse(await response.Content.ReadAsStringAsync());

            return (string)json["entry"][0]["name"]["formatted"];
        }
    }
}