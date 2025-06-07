using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Mockify.API.Middlewares;
using Mockify.API.Models.DB;
using Mockify.API.Services;
using StackExchange.Redis;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMockDataService, MockDataService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200",
                            "https://app-mockify.netlify.app/",
                            "https://app-mockify.netlify.app")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
    });
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "MockifyRateLimit_";
});

// Add rate limiting services
builder.Services.AddMemoryCache(); 
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>(); 
builder.Services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>(); 

builder.Services.Configure<IpRateLimitOptions>(options => {
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.QuotaExceededMessage = "Too many requests, Try again later.";
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*", // All endpoints
            Period = "1m", // 1-minute window
            Limit = 10
        }
    };

});
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = sp.GetService<IConfiguration>();
    string redisConnectionString = configuration.GetConnectionString("Redis") ?? string.Empty;
    return ConnectionMultiplexer.Connect(redisConnectionString);
});

builder.Services.Configure<MockifyDatabaseSettings>(builder.Configuration.GetSection("MockifyDatabase"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}).AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"];
    options.ClientSecret = builder.Configuration["Google:ClientSecret"]; ;
    options.CallbackPath = "/";
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "https://accounts.google.com",
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Google:ClientId"],
        ValidateLifetime = true
    };
});

var app = builder.Build();
app.UseIpRateLimiting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("MyCorsPolicy");


app.MapControllers();
app.UseMiddleware<GlobalExceptionHandler>();

app.Run();
