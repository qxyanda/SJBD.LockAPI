using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using SJBD.Convert.Services;
using Microsoft.AspNetCore.Http;



namespace SJBD.LockAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordToPdfController
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public string WordToPdfPost(string wordUri)
        {
            WordToPdfService wordToPdfService = new WordToPdfService();
            return wordToPdfService.WordToPdf(wordUri);
        }
    }
}