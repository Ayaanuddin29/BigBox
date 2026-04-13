using ComplaintManagement.Data.Interfaces.KnowledgeBase;
using ComplaintManagement.Util.Models.KnowledgeBase;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ComplaintManagement.Data.Repositories.KnowledgeBase
{
    public class KBArticleRepository : IKBArticleRepository
    {
        private readonly IDbConnection _db;

        public KBArticleRepository(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<KBCategoryModel>> GetKBCategoriesAsync()
        {
            var sql = "SELECT Id, Name, Description FROM dbo.robox_m_KB_Categories";
            return await _db.QueryAsync<KBCategoryModel>(sql);
        }

        public async Task<IEnumerable<KBStatusModel>> GetKBStatusAsync()
        {
            var sql = "SELECT Id, Name, Description FROM dbo.robox_m_KB_Status";
            return await _db.QueryAsync<KBStatusModel>(sql);
        }

        public async Task<int> InsertKBArticleAsync(KBArticleCreateDto dto)
        {
            var p = new DynamicParameters();

            p.Add("@title", dto.Title, DbType.String);
            p.Add("@summary", dto.Summary, DbType.String);
            p.Add("@content", dto.Content, DbType.String);
            p.Add("@categoryid", dto.CategoryId, DbType.Int32);
            p.Add("@statusid", dto.StatusId, DbType.Int32);
            p.Add("@isfeatured", dto.IsFeatured, DbType.Boolean);
            p.Add("@login_created", dto.LoginCreated, DbType.String);
            p.Add("@filepath", dto.FilePath);
            p.Add("@relatedurl", dto.RelatedUrl);
            p.Add("@isgeneralarticle", dto.IsGeneralArticle);
            p.Add("@istechnicalarticle", dto.IsTechnicalArticle);

            p.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _db.ExecuteAsync(
                "dbo.ProcInsert_KBArticle_2",
                p,
                commandType: CommandType.StoredProcedure);

            var newId = p.Get<int?>("@NewId") ?? 0;

            return newId;
        }
        public async Task<List<KBArticleListDto>> GetKBArticleListAsync()
        {
            var result = await _db.QueryAsync<KBArticleListDto>(
                "Proc_Get_KBArticles_List",
                commandType: CommandType.StoredProcedure);

            return result.ToList();
        }
        public async Task<KBArticleCreateDto> GetKBArticleByIdAsync(int id)
        {
            var sql = @"SELECT
                Title,
                Summary,
                Content,
                CategoryId,
                StatusId,
                IsFeatured,
                FilePath,
                RelatedUrl,
                IsGeneralArticle,
                IsTechnicalArticle
                FROM dbo.robox_t_KB_Articles_2
                WHERE Id = @Id";

            return await _db.QueryFirstOrDefaultAsync<KBArticleCreateDto>(sql, new { Id = id });
        }
        public async Task UpdateKBArticleAsync(int id, KBArticleCreateDto dto)
        {
            var p = new DynamicParameters();

            p.Add("@Id", id);
            p.Add("@Title", dto.Title);
            p.Add("@Summary", dto.Summary);
            p.Add("@Content", dto.Content);
            p.Add("@CategoryId", dto.CategoryId);
            p.Add("@StatusId", dto.StatusId);
            p.Add("@IsFeatured", dto.IsFeatured);
            p.Add("@FilePath", dto.FilePath);
            p.Add("@RelatedUrl", dto.RelatedUrl);
            p.Add("@IsGeneralArticle", dto.IsGeneralArticle);
            p.Add("@IsTechnicalArticle", dto.IsTechnicalArticle);

            await _db.ExecuteAsync(
                "ProcUpdate_KBArticle_4",
                p,
                commandType: CommandType.StoredProcedure);
        }
    }
}