using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tablestz_api.Interfaces;
using tablestz_api.Models;

namespace tablestz_api.Controllers
{
    [Route("api/table")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly IWordRepository _wordRepository;
        public TableController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        [HttpGet("words")]
        public async Task<IActionResult> GetAllWords()
        {
            var words = await _wordRepository.GetAllWordsAsync();
            return Ok(words);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchWords([FromQuery] string letter)
        {
            var words = await _wordRepository.SearchWordsAsync(letter);
            return words is null ? NoContent() : Ok(words);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveWords([FromBody] List<Word> words)
        {
            var result = await _wordRepository.SaveWordsAsync(words);
            return Ok();
        }
    }
}
