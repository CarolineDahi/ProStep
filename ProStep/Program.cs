using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ProStep.Base;
using ProStep.Courses.BootcampRepository;
using ProStep.Courses.CommentRepository;
using ProStep.Courses.CourseRepository;
using ProStep.DataSourse.Context;
using ProStep.DataSourse.DataSeed;
using ProStep.General.CategoryRepository;
using ProStep.General.CityRepository;
using ProStep.General.CommonQuestionRepository;
using ProStep.General.CountryRepository;
using ProStep.General.FacultyRepository;
using ProStep.General.NotificationRepository;
using ProStep.General.StatisticsRepository;
using ProStep.General.UniversityRepository;
using ProStep.Maps.RoadMapRepository;
using ProStep.Model.Security;
using ProStep.Profile.PortfolioRepository;
using ProStep.Security.SecurityRepository;
using ProStep.Shared.FileRepository;
using ProStep.SharedKernel.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using VueCliMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.Configure<DataProtectionTokenProviderOptions>(options
    => options.TokenLifespan = TimeSpan.FromHours(1));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy", builder =>
    {
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyOrigin();
    });
});
builder.Services.AddDataProtection();

//builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = builder.Configuration["Redis:ConnectionString"]; });

builder.Services.AddDbContext<ProStepDBContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), op => op.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            ));
});

builder.Services.AddIdentity<User, IdentityRole<Guid>>(o =>
{
    o.Password.RequiredLength = 4;
    o.Password.RequiredUniqueChars = 0;
    o.Password.RequireDigit = false;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireLowercase = false;
    o.User.RequireUniqueEmail = true;
    o.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<ProStepDBContext>().AddDefaultTokenProviders();

#region - Profile -
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

#endregion

#region - Security -
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
#endregion

#region - General -
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();
builder.Services.AddScoped<ICommonQuesRepository, CommonQuesRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
#endregion

#region - Courses -
builder.Services.AddScoped<IBootcampRepository, BootcampRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
#endregion

#region - Maps -
builder.Services.AddScoped<IRoadMapRepository, RoadMapRepository>();
#endregion

#region - Shared -
builder.Services.AddScoped<IFileRepository, FileRepository>();
#endregion

builder.Services.AddMyAuth(builder.Configuration);
builder.Services.AddMySwagger();
builder.Services.AddHttpClient("fcm", c => c.BaseAddress = new Uri("https://fcm.googleapis.com"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ProStepDBContext>();
    await context.Database.MigrateAsync();
    await SecuritySeedData.InitializeAsync(services);
    await SeedData.InitializeAsync(services);
}
app.UseDeveloperExceptionPage();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProStep V1");
    c.DocExpansion(DocExpansion.None);
});
app.UseCors("Policy");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseResponseCaching();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
