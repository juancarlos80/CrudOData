using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using WebApiExample2.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<BloggingContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BloggingContext")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddOData(options => options.Select().Filter().OrderBy());

var builderOdata = new ODataConventionModelBuilder();
builderOdata.EntitySet<Product>("Products");
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata", builderOdata.GetEdmModel()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<BloggingContext>();
    context.Database.EnsureCreated();
    InitBloggingData.Initialize(context);
}

app.UseAuthorization();

app.MapControllers();

app.Run();