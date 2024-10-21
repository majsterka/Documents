using Coderama.Data;
using Coderama.Services;
using Coderama.Services.DocumentFormats;
using Coderama.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Document API",
        Description = "A simple example of document manipulation",
        Contact = new OpenApiContact
        {
            Name = "Katarína Zelenayová",
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// choose which storage you want to use
//MSSQL database:
//builder.Services.AddDbContext<DbDocumentContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnectionString")));
//builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
//InMemory database:
builder.Services.AddDbContext<InMemoryDocumentContext>(options => options.UseInMemoryDatabase(databaseName: "DocumentsDb"));
builder.Services.AddScoped<IDocumentRepository, InMemoryDocumentRepository>();

builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddSingleton<IDocumentFormat<string>, XmlFormat>();
builder.Services.AddSingleton<IDocumentFormat<byte[]>, MessagePackFormat>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
