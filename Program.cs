using Microsoft.EntityFrameworkCore;
using PMS_API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Register controllers (our ProductsController)
builder.Services.AddControllers();

// 2. Register the database context (connection string comes from appsettings.json)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// 3. Swagger (auto-generated API test page)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "PMS API - Product Management";
        options.InjectStylesheet("/swagger-ui/lilac.css"); // our lilac theme
    });
}

app.UseHttpsRedirection();

// Needed so uploaded images (wwwroot/uploads) and lilac.css can be served
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

app.Run();
