using Microsoft.OpenApi.Models;
using System.Reflection;
using UserManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using static UserManagement.SharedKernel.DependencyInjection;
using static UserManagement.Features.DependencyInjection;
using static UserManagement.Infrastructure.DependencyInjection;
using UserManagement.Domain.ApplicationConfigs.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(x => x.AddFilters());

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndLocalPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cnf => {
    cnf.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "UserManagement" });
    cnf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Authentication Header via Json Web Tokens (JWT). Insert your JWT Token in the following way: 'Bearer 12345abcdef'",
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

var configs = new ApplicationConfigs();
builder.Configuration.Bind("ApplicationConfigs", configs);
builder.Services.AddSingleton(configs);

var dbContextCnn = builder.Configuration.GetConnectionString("UserManagementDB");
builder.Services.AddDbContext<UserManagerDbContext>(x => x.UseSqlServer(dbContextCnn));

AddBindings();
builder.Services.AddMessaging();
builder.Services.AddFeatures();
builder.Services.AddRepositories();

AddLogs(builder);

var app = builder.Build();

app.UseCors("FrontEndLocalPolicy");

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
    logOptions.TableName = "UserManagementLogs";
    
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
        var dbContext = scope.ServiceProvider.GetRequiredService<UserManagerDbContext>();
        dbContext.Database.EnsureCreated();
    }
}