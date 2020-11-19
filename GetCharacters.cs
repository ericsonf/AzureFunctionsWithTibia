using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace AzureFunctionsWithTibia
{
    public static class GetCharacters
    {
        [FunctionName("GetCharacters")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting a Tibia Character Information");
            
            var characterName = req.Query["nome"];
            var client = new HttpClient();
            var response = await client.GetAsync($"https://api.tibiadata.com/v2/characters/{characterName}.json");
            var content = await response.Content.ReadAsStringAsync();

            log.LogInformation($"Informations collected: {content}");

            return new OkObjectResult(content);
        }
    }
}
