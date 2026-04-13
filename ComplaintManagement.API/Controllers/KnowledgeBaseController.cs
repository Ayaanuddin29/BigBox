using ComplaintManagement.Business.Interfaces.KnowledgeBase;
using ComplaintManagement.Util.Models.KnowledgeBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ComplaintManagement.API.Controllers
{
    [ApiController]
    public class KnowledgeBaseController : ControllerBase
    {
        private readonly IKBArticleService _kbService;
        private readonly IConfiguration _configuration;

        public KnowledgeBaseController(IKBArticleService kbService, IConfiguration configuration)
        {
            _kbService = kbService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("api/[controller]/CreateKBArticle")]
        public async Task<IActionResult> CreateKBArticle([FromBody] KBArticleCreateDto article)
        {
            try
            {
                var id = await _kbService.CreateKBArticleAsync(article);
                return Ok(new { ArticleId = id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("api/[controller]/GetKBArticleList")]
        public async Task<IActionResult> GetKBArticleList()
        {
            var articles = await _kbService.GetKBArticleListAsync();
            return Ok(articles);
        }
        [HttpGet]
        [Route("api/[controller]/GetNextArticleId")]
        public async Task<IActionResult> GetNextArticleId()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var nextId = await connection.ExecuteScalarAsync<int>(
                "SELECT IDENT_CURRENT('dbo.robox_t_KB_Articles_2') + 1");

            return Ok(nextId);
        }
        [HttpGet]
        [Route("api/[controller]/GetKBArticle/{id}")]
        public async Task<IActionResult> GetKBArticle(int id)
        {
            var article = await _kbService.GetKBArticleByIdAsync(id);

            if (article == null)
                return NotFound();

            return Ok(article);
        }
        [HttpPut]
        [Route("api/[controller]/UpdateKBArticle/{id}")]
        public async Task<IActionResult> UpdateKBArticle(int id, [FromBody] KBArticleCreateDto dto)
        {
            await _kbService.UpdateKBArticleAsync(id, dto);

            return Ok("Updated successfully");
        }
    }

}