using Microsoft.AspNetCore.Mvc;
using Simplifier.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;


namespace Simplifier.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly ILogger<ApplicationsController> _logger;
        private readonly SimplifierContext _context;
        private readonly HttpClient _httpClient;



        public ApplicationsController(ILogger<ApplicationsController> logger, SimplifierContext context, HttpClient httpClient)
        {
            _logger = logger;
            _context = context;
            _httpClient = httpClient;

        }

        [HttpGet]
        public IEnumerable<Application> Get()
        {
            var applicationsWithResponses = _context.Applications
                .Include(a => a.FormResponses)
                .ToList();
            foreach (var app in applicationsWithResponses)
            {
                _logger.LogInformation($"Application: {app.Name}, UserId: {app.UserId}, CreatedAt: {app.CreatedAt}");
            }
            return applicationsWithResponses;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Application application)
        {
            // Add application to the database
            _context.Applications.Add(application);
            _context.SaveChanges();

            // Call ChatGPT API
            var chatGptResponse = await CallChatGptApi(application);
            foreach (var keyValue in chatGptResponse)
            {
                _logger.LogInformation($"Key: {keyValue.Key}, Value: {keyValue.Value}");
                _context.FormResponses.Add(new FormResponses
                {   
                    Uuid = Guid.NewGuid(),
                    ApplicationId = application.Uuid,
                    FormField = keyValue.Key,
                    Response = keyValue.Value,
                });
            }
            await _context.SaveChangesAsync();

            return Ok(new { message = "success", chatGptResponse });
        }

        private async Task<Dictionary<string, string>> CallChatGptApi(Application application)
        {
            var formFields = _context.FormFields
                .Where(f => f.TemplateId == application.TemplateId)
                .ToList();


            var prompt = $@"You are an intelligent assistant tasked with extracting relevant information for a food assistance application from a raw text email. 
                           The email might contain unrelated details, so focus only on the required fields. 
                           Here's a list of fields you need to extract from the text: {{
                               {string.Join(", ", formFields.Select(f => $"'{f.FormField}': '{{'Type': '{f.FormType}', 'Example Response': '{f.ExpectedResponse}'}}'"))}
                           }}
                           Please return responses in the following JSON format and include nothing but this exact format with the 'Response' changed for each: {{
                            {string.Join(", ", formFields.Select(f => $"{f.FormField}': Response"))}
                           }}

                           Here is the raw text for you to parse: 
                           {application.RawText}
                           ";

            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
    {
        new { role = "user", content = prompt }
    }
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openai.com/v1/chat/completions"),
                Content = JsonContent.Create(requestBody)
            };
            request.Headers.Add("Authorization", "Bearer {KEY}");
            request.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonDocument.Parse(responseBody);
            var chatGptResponse = jsonResponse.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
            _logger.LogInformation("ChatGPT response: {response}", chatGptResponse);
            chatGptResponse = chatGptResponse.Replace("'", "\"");
            // Remove everything before and after the JSON object
            var startIndex = chatGptResponse.IndexOf("{");
            var endIndex = chatGptResponse.LastIndexOf("}") + 1;
            chatGptResponse = chatGptResponse.Substring(startIndex, endIndex - startIndex);
            Dictionary<string, string> responseDict = JsonSerializer.Deserialize<Dictionary<string, string>>(chatGptResponse);            // Print key-value pairs

            return responseDict;
        }


        [HttpDelete("{uuid}")]
        public IActionResult Delete(Guid uuid)
        {
            _logger.LogInformation("Deleting application with uuid {uuid}", uuid);
            var application = _context.Applications.FirstOrDefault(a => a.Uuid == uuid);
            if (application == null)
            {
                _logger.LogWarning("Application with uuid {uuid} not found", uuid);
                return NotFound(new { message = "Application not found" });
            }

            // Retrieve and delete all form fields associated with the application
            var formResponses = _context.FormResponses.Where(f => f.ApplicationId == application.Uuid).ToList();
            _context.FormResponses.RemoveRange(formResponses);

            // Delete the application
            _context.Applications.Remove(application);
            _context.SaveChanges();

            _logger.LogInformation("Deleted application with uuid {uuid}", uuid);

            return Ok(new { message = "Successfully deleted application and associated form fields" });
        }


    }
}
