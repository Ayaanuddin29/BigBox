
using ComplaintManagement.API.Data;
using ComplaintManagement.API.Models;
using ComplaintManagement.Business.Interfaces;
using ComplaintManagement.Business.Interfaces.Dashboard;
using ComplaintManagement.Business.Interfaces.Incident;   // For IIncidentService
using ComplaintManagement.Business.Interfaces.KnowledgeBase;
using ComplaintManagement.Business.Interfaces.Master;
using ComplaintManagement.Business.Interfaces.SLM;
using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Business.Interfaces.Workflow;
using ComplaintManagement.Business.Services;
using ComplaintManagement.Business.Services.Dashboard;
using ComplaintManagement.Business.Services.Incident;     // For IncidentService
using ComplaintManagement.Business.Services.KnowledgeBase;
using ComplaintManagement.Business.Services.Master;
using ComplaintManagement.Business.Services.SLM;
using ComplaintManagement.Business.Services.UserManagement;
using ComplaintManagement.Business.Services.Workflow;
using ComplaintManagement.Data.Interfaces.Dashboard;
using ComplaintManagement.Data.Interfaces.Incident;       // For IIncidentRepository
using ComplaintManagement.Data.Interfaces.KnowledgeBase;
using ComplaintManagement.Data.Interfaces.Master;
using ComplaintManagement.Data.Interfaces.SLM;
using ComplaintManagement.Data.Interfaces.UserManagement;
using ComplaintManagement.Data.Interfaces.Workflow;
using ComplaintManagement.Data.Repositories.Dashboard;
using ComplaintManagement.Data.Repositories.Incident;
using ComplaintManagement.Data.Repositories.KnowledgeBase;
using ComplaintManagement.Data.Repositories.Master;
using ComplaintManagement.Data.Repositories.SLM;
using ComplaintManagement.Data.Repositories.UserManagement;
using ComplaintManagement.Data.Repositories.Workflow;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using ComplaintManagement.Business.Interfaces;
using ComplaintManagement.Business.Services;
//using ComplaintManagement.Data.Interfaces.UserManagement;
//using ComplaintManagement.Data.Repositories.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);


// Add services
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IIncidentService, IncidentService>();

builder.Services.AddScoped<IMasterRepository, MasterRepository>();
builder.Services.AddScoped<IMasterService, MasterService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();

builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddScoped<ISltRepository, SltRepository>();
builder.Services.AddScoped<ISltService, SltService>();

builder.Services.AddScoped<IWorkflowRuleRepository, WorkflowRuleRepository>();
builder.Services.AddScoped<IWorkflowRuleService, WorkflowRuleService>();

builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();
builder.Services.AddScoped<IWorkflowService, WorkflowService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

//builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
//builder.Services.AddScoped<IUserRoleService, UserRoleService>();

builder.Services.AddScoped<IAssociateRepository, AssociateRepository>();
builder.Services.AddScoped<IAssociateService, AssociateService>();

builder.Services.AddScoped<IAssociateGroupRepository, AssociateGroupRepository>();
builder.Services.AddScoped<IAssociateGroupService, AssociateGroupService>();

builder.Services.AddScoped<IApproverRepository, ApproverRepository>();
builder.Services.AddScoped<IApproverService, ApproverService>();

builder.Services.AddScoped<IAuditRepository, AuditRepository>();
builder.Services.AddScoped<IAuditService, AuditService>();

builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddScoped<IProcessRelationRepository, ProcessRelationRepository>();
builder.Services.AddScoped<IProcessRelationService, ProcessRelationService>();

builder.Services.AddScoped<IKBCategoryRepository, KBCategoryRepository>();
builder.Services.AddScoped<IKBCategoryService, KBCategoryService>();

builder.Services.AddScoped<IKBArticleRepository, KBArticleRepository>();
builder.Services.AddScoped<IKBArticleService, KBArticleService>();


builder.Services.AddScoped<IKBStatusRepository, KBStatusRepository>();
builder.Services.AddScoped<IKBStatusService, KBStatusService>();


builder.Services.AddScoped<IIncidentReassignRepository, IncidentReassignRepository>();
builder.Services.AddScoped<IIncidentReassignService, IncidentReassignService>();


var jwt = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwt["Key"] ?? "SuperSecretKey@123");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUI", policy =>
    {
        policy.WithOrigins("https://localhost:5002").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

//builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");

// Ensure DB created and seed roles/user at startup (development convenience)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<AppDbContext>();
        db.Database.Migrate();

        var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userMgr = services.GetRequiredService<UserManager<ApplicationUser>>();

        async Task EnsureSeedAsync()
        {
            string[] roles = new[] { "Admin", "User" };
            foreach (var r in roles)
                if (!await roleMgr.RoleExistsAsync(r))
                    await roleMgr.CreateAsync(new IdentityRole(r));

            var admin = await userMgr.FindByNameAsync("admin");
            if (admin == null)
            {
                var a = new ApplicationUser { UserName = "admin", Email = "admin@local", FullName = "Administrator", EmailConfirmed = true };
                var res = await userMgr.CreateAsync(a, "Admin@123");
                if (res.Succeeded)
                    await userMgr.AddToRoleAsync(a, "Admin");
            }
        }

        EnsureSeedAsync().GetAwaiter().GetResult();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"DB seed error: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowUI");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
