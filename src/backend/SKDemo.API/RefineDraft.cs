using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SKDemo.API
{
    public class RefineDraft
    {
        private readonly ILogger<RefineDraft> _logger;

        public RefineDraft(ILogger<RefineDraft> logger)
        {
            _logger = logger;
        }

        [Function(nameof(RefineDraft))]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "drafts")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // get the draft from the request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            _logger.LogInformation(requestBody);

            var x = await new Agents.RefineDraft().Run(requestBody);


            // call AgentsService to make a revised draft
            return new OkObjectResult(x);
        }
    }
}
