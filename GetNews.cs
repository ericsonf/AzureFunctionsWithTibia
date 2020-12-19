using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace AzureFunctionsWithTibia
{
    public static class GetNews
    {
        [FunctionName("GetNews")]
        public static async void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var random = new Random();
            int randomNumber = random.Next(3300, 3600);

            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.tibiadata.com/v2/news/{randomNumber}.json");
            var content = await response.Content.ReadAsStringAsync();

            log.LogInformation($"Informations collected: {content}");
        }
    }
}
