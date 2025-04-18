using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Mockify.API.Middlewares;
using Mockify.API.Models.DB;
using Mockify.API.Services;
using StackExchange.Redis;

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



// Test Redis connection
using (var scope = app.Services.CreateScope())
{
    var redis = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
    try
    {
        var db = redis.GetDatabase();
        // Option 1: Ping Redis
        var pingResult = db.Ping();
        Console.WriteLine($"Redis connection successful! Ping time: {pingResult.TotalMilliseconds} ms");

        // Option 2: Set and Get a test key (optional)
        string testKey = "test-connection";
        db.StringSet(testKey, "Hello Redis");
        var value = db.StringGet(testKey);
        Console.WriteLine($"Redis test value: {value}");
    }
    catch (RedisConnectionException ex)
    {
        Console.WriteLine($"Redis connection failed: {ex.Message}");
        throw; // Optionally stop the app if connection is critical
    }
}

app.Run();
