using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GitHubBuckets
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            string user = "gabrielschade";
            string repository = "githubbuckets";
            string bucketPath = "/bucket";
            string branch = "master";
            string category_url = $"https://api.github.com/repos/{user}/{repository}/contents{bucketPath}?ref={branch}";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            
            string categoriesRawString = await client.GetStringAsync(category_url);
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesRawString);

            Console.WriteLine("######### Categorias #########");
            categories.ForEach(category => Console.WriteLine(category.FormattedName));

            foreach (Category category in categories)
            {
                Console.WriteLine($"######### {category.FormattedName} #########");
                string content = await client.GetStringAsync(category.download_url);
                string[] items = content.Split("\n");
                foreach (string item in items)
                {
                    Console.WriteLine(item);
                }
            }
            

            Console.ReadKey();
        }
    }
}
