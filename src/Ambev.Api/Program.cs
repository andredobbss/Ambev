using Ambev.Api.Middlewares;
using Ambev.Api.Shared;
using Ambev.CrossCutting.IoC;
using Ambev.Infraestructure.Database.MongoConfigurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;



#region Configurações Iniciais

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });


builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));


#endregion

#region Configura Swagger

builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Ambev.Api",
        Description = "Uma API Web ASP.NET Core para teste",
        //TermsOfService = new Uri(""),
        Contact = new OpenApiContact
        {
            Name = "Contato",
            //Url = new Uri("")
        },
        License = new OpenApiLicense
        {
            Name = "Licença",
            //Url = new Uri("")
        }
    });


    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Baearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Header de autorização JWT usando o esquema Bearer, \r\n\r\nInforme 'Bearer' [espaço] e o seu Token, \r\n\r\nExemplo: \'Bearer 123456abcdefg\'",
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
           new string[] {}
        }
    });

    c.OperationFilter<SwaggerQueryParamFilter>();

    c.CustomSchemaIds(type => type.FullName); // Usa o nome completo do namespace

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

#endregion

#region Configura serviços

builder.Services.AddInfraestructure(builder.Configuration);

#endregion

#region Configura JWT

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
        ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        RoleClaimType = ClaimTypes.Role
    };
});

#endregion

#region Configura app's

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DbContext? dbContext = null;

    var context = dbContext.Context(scope);

    context.Database.Migrate();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion