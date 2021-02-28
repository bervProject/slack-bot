using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SlackAPI;

namespace SlackBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotSendMessageController : ControllerBase
    {

        private readonly ILogger<BotSendMessageController> _logger;
        private readonly SlackTaskClient _client;

        public BotSendMessageController(IConfiguration configuration, ILogger<BotSendMessageController> logger)
        {
            _logger = logger;
            var token = configuration["SlackToken"];
            var slackClient = new SlackTaskClient(token);
            _client = slackClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var channel = "general";
            var message = "Hello World!";
            var response = await _client.PostMessageAsync(channel, message);
            var responseMessage = string.Empty;
            if (response.ok)
            {
                responseMessage = "Message sent successfully";
            }
            else
            {
                responseMessage = "Message sending failed. error: " + response.error;
            }
            Console.WriteLine(responseMessage);
            return Ok(new  { message = responseMessage });
        }
    }
}
