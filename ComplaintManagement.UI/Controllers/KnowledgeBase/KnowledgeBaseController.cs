using ClosedXML.Excel;
using ComplaintManagement.UI.Models;
using ComplaintManagement.Util.Models.KnowledgeBase;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http.Json;
//using System.Reflection.Emit;
//using System.Reflection.Metadata;


namespace ComplaintManagement.UI.Controllers
{
    public class KnowledgeBaseController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public KnowledgeBaseController(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _config = config;
            _clientFactory = clientFactory;
        }

        // =========================
        // CREATE ARTICLE (GET)
        // =========================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            int nextId = 0;

            try
            {
                nextId = await client.GetFromJsonAsync<int>($"{apiBase}/api/KnowledgeBase/GetNextArticleId");
            }
            catch
            {
                nextId = 0;
            }

            var model = new KBArticleCreateViewModel
            {
                ArticleNumber = nextId
            };

            return View(model);
        }

        // =========================
        // CREATE ARTICLE (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KBArticleCreateViewModel model, List<string> RelatedUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            if (RelatedUrl != null && RelatedUrl.Count > 0)
            {
                model.RelatedUrl = string.Join(",", RelatedUrl);
            }

            List<string> filePaths = new List<string>();

            if (model.UploadFiles != null && model.UploadFiles.Count > 0)
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                foreach (var file in model.UploadFiles)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    filePaths.Add("/uploads/" + fileName);
                }
            }

            var dto = new KBArticleCreateDto
            {
                Title = model.Title,
                Summary = model.Summary,
                Content = model.Content,
                CategoryId = model.CategoryId ?? 0,
                StatusId = model.StatusId ?? 0,
                IsFeatured = model.IsFeatured,
                RelatedUrl = model.RelatedUrl,
                IsGeneralArticle = model.IsGeneralArticle,
                IsTechnicalArticle = model.IsTechnicalArticle,
                FilePath = string.Join(",", filePaths),
                LoginCreated = HttpContext.Session.GetString("UserName") ?? "admin"
            };

            var response = await client.PostAsJsonAsync($"{apiBase}/api/KnowledgeBase/CreateKBArticle", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Knowledge Article created successfully";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating Knowledge Article");
            return View(model);
        }

        // =========================
        // ARTICLE LIST
        // =========================
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"{apiBase}/api/KnowledgeBase/GetKBArticleList");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to fetch KB Articles";
                return View(new List<KBArticleListItemViewModel>());
            }

            var articles = await response.Content.ReadFromJsonAsync<List<KBArticleListItemViewModel>>();

            return View(articles);
        }

        // =========================
        // MANAGE ARTICLE (GET)
        // =========================
        [HttpGet]
        public async Task<IActionResult> Manage(int id)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var article = await client.GetFromJsonAsync<KBArticleCreateViewModel>(
                $"{apiBase}/api/KnowledgeBase/GetKBArticle/{id}");

            if (article == null)
                return NotFound();

            article.ArticleNumber = id;

            return View(article);
        }

        // =========================
        // MANAGE ARTICLE (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(KBArticleCreateViewModel model, List<string> RelatedUrl)
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            if (RelatedUrl != null && RelatedUrl.Count > 0)
            {
                model.RelatedUrl = string.Join(",", RelatedUrl);
            }

            List<string> filePaths = new List<string>();

            if (!string.IsNullOrEmpty(model.FilePath))
            {
                filePaths = model.FilePath.Split(',').ToList();
            }

            if (model.UploadFiles != null && model.UploadFiles.Count > 0)
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                foreach (var file in model.UploadFiles)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    filePaths.Add("/uploads/" + fileName);
                }
            }

            var dto = new KBArticleCreateDto
            {
                Title = model.Title,
                Summary = model.Summary,
                Content = model.Content,
                CategoryId = model.CategoryId,
                StatusId = model.StatusId,
                IsFeatured = model.IsFeatured,
                RelatedUrl = model.RelatedUrl,
                IsGeneralArticle = model.IsGeneralArticle,
                IsTechnicalArticle = model.IsTechnicalArticle,
                FilePath = string.Join(",", filePaths)
            };

            var response = await client.PutAsJsonAsync(
                $"{apiBase}/api/KnowledgeBase/UpdateKBArticle/{model.ArticleNumber}", dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Article Updated Successfully";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error updating article");
            return View(model);
        }
        public async Task<IActionResult> ExportToExcel()
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var articles = await client.GetFromJsonAsync<List<KBArticleListItemViewModel>>
                ($"{apiBase}/api/KnowledgeBase/GetKBArticleList");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("KB Articles");

                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Title";
                worksheet.Cell(1, 3).Value = "Category";
                worksheet.Cell(1, 4).Value = "Status";
                worksheet.Cell(1, 5).Value = "Created By";
                worksheet.Cell(1, 6).Value = "Created At";

                for (int i = 0; i < articles.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = articles[i].Id;
                    worksheet.Cell(i + 2, 2).Value = articles[i].Title;
                    worksheet.Cell(i + 2, 3).Value = articles[i].CategoryName;
                    worksheet.Cell(i + 2, 4).Value = articles[i].StatusName;
                    worksheet.Cell(i + 2, 5).Value = articles[i].CreatedBy;
                    worksheet.Cell(i + 2, 6).Value = articles[i].CreatedAt;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "KnowledgeBase.xlsx");
                }
            }
        }
        public async Task<IActionResult> ExportToPdf()
        {
            var apiBase = _config["ApiBaseUrl"].TrimEnd('/');
            var client = _clientFactory.CreateClient();

            var articles = await client.GetFromJsonAsync<List<KBArticleListItemViewModel>>
                ($"{apiBase}/api/KnowledgeBase/GetKBArticleList");

            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                document.Add(new Paragraph("Knowledge Base Articles"));

                Table table = new Table(6);

                table.AddHeaderCell("ID");
                table.AddHeaderCell("Title");
                table.AddHeaderCell("Category");
                table.AddHeaderCell("Status");
                table.AddHeaderCell("Created By");
                table.AddHeaderCell("Created At");

                foreach (var item in articles)
                {
                    table.AddCell(item.Id.ToString());
                    table.AddCell(item.Title);
                    table.AddCell(item.CategoryName);
                    table.AddCell(item.StatusName);
                    table.AddCell(item.CreatedBy);
                    table.AddCell(item.CreatedAt.ToString());
                }

                document.Add(table);
                document.Close();

                return File(stream.ToArray(), "application/pdf", "KnowledgeBase.pdf");
            }
        }
    }
}