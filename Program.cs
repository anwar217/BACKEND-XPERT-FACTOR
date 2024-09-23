using factoring1.FrameworkEtDrivers;
using factoring1.Repositories;
using factoring1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure Entity Framework and MySQL
builder.Services.AddDbContext<FactoringDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 7))));

// Register repositories and services
builder.Services.AddScoped<IIndividuContratService, IndividuContratService>();
builder.Services.AddScoped<IIndividuContratRepository, IndividuContratRepository>();
builder.Services.AddScoped<IContratService, ContratService>();
builder.Services.AddScoped<IContratRepository, ContratRepository>();
builder.Services.AddScoped<IBordereauRepository, BordereauRepository>();
builder.Services.AddScoped<IBordereauService, BordereauService>();
builder.Services.AddScoped<IFinancementRepository, FinancementRepository>();
builder.Services.AddScoped<IFinancementService, FinancementService>();
builder.Services.AddScoped<IIndividuRepository, IndividuRepository>();
builder.Services.AddScoped<IIndividuService, IndividuService>();
builder.Services.AddScoped<ILitigeRepository, LitigeRepository>();
builder.Services.AddScoped<ILitigeService, LitigeService>();
builder.Services.AddScoped<IProrogationRepository, ProrogationRepository>();
builder.Services.AddScoped<IProrogationService, ProrogationService>();
builder.Services.AddScoped<ILimiteRepository, LimiteRepository>();
builder.Services.AddScoped<ILimiteService, LimiteService>();
builder.Services.AddScoped<IFactureRepository, FactureRepository>();
builder.Services.AddScoped<IFactureService, FactureService>();
builder.Services.AddScoped<IDisponibleService, DisponibleService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddMemoryCache();

// Register SmsService
builder.Services.AddSingleton<SmsService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var accountSid = configuration["Twilio:AccountSid"];
    var authToken = configuration["Twilio:AuthToken"];
    var fromNumber = configuration["Twilio:FromNumber"];
    return new SmsService(accountSid, authToken, fromNumber);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);
System.Diagnostics.Debug.WriteLine("secretKey*******************");
System.Diagnostics.Debug.WriteLine(secretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{options.UseSecurityTokenValidators = true;


    options.TokenValidationParameters = new TokenValidationParameters
    {  
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "factoring1", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://10.0.2.2:5000")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});
var app = builder.Build();



app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
