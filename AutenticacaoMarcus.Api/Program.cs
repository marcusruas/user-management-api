using Microsoft.OpenApi.Models;
using System.Reflection;
using static AutenticacaoMarcus.SharedKernel.DependencyInjection;
using static AutenticacaoMarcus.Features.DependencyInjection;
using static AutenticacaoMarcus.Features.Bindings;
using AutenticacaoMarcus.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(x => x.AdicionarFiltros());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cnf => {
    cnf.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "AutenticacaoMarcus" });
    cnf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Header autenticacao via Json Web Tokens (JWT). insira abaixo o seu token da seguinte forma: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    cnf.AddSecurityRequirement(new OpenApiSecurityRequirement() {
    {
        new OpenApiSecurityScheme
        {
        Reference = new OpenApiReference
            {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        },
        new List<string>()
        }
    });
});

var connectionStringAutenticacaoDB = builder.Configuration.GetConnectionString("AutenticacaoDB");
builder.Services.AddDbContext<AutenticacaoDbContext>(x => x.UseSqlServer(connectionStringAutenticacaoDB));

CriarBindingsParaFeatures();
builder.Services.AdicionarMensageria();
builder.Services.AddFeatures();

AddLogs(builder);

var app = builder.Build();

CreateDatabase(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddLogs(WebApplicationBuilder builder)
{
    var connectionStringLogsDB = builder.Configuration.GetConnectionString("LogsDB");

    var logOptions = new MSSqlServerSinkOptions();
    logOptions.AutoCreateSqlTable = true;
    logOptions.TableName = "AutenticacaoLogs";
    
    Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.MSSqlServer(connectionStringLogsDB, logOptions)
                .CreateLogger();

    builder.Logging.AddSerilog(Log.Logger);
}

void CreateDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AutenticacaoDbContext>();
        dbContext.Database.EnsureCreated();
    }
}