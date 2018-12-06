using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using Newtonsoft.Json;
using System;

namespace Bing
{
    class Program
    {
        static void Main(string[] args)
        {
            var subKey = "5783dbc5562e4297a79a3e9f2f0aca2c";

            Console.WriteLine("Termo pesquisa:");
            var termo = Console.ReadLine();

            WebSearchClient client = new WebSearchClient(
                new ApiKeyServiceClientCredentials(subKey));

            var res = client.Web.SearchAsync(termo).Result;

            Console.WriteLine(JsonConvert.SerializeObject(res));
            Console.Read();
        }
    }
}
