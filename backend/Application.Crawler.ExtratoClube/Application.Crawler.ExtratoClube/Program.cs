using Application.Crawler.ExtratoClube.AppContext;
using Application.Crawler.ExtratoClube.Extensions;
using Application.Crawler.ExtratoClube.Services;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Handlers
builder.Services.AddCustomHandlers();

// Add Services
builder.Services.AddCustomServices();

// Add Repositories
builder.Services.AddCustomRepository();

// Add Data Context
builder.Services.AddCustomDataContext();


// Mediator and queue Consumer
var mediator = builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()
));
var serviceProvider = mediator.BuildServiceProvider();
var service = new UserServiceConsumer(serviceProvider.GetRequiredService<IMediator>());
service.ProcessMessages();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost",
                "http://localhost:4200",
                "https://localhost:7230",
                "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);


app.UseAuthorization();

app.MapControllers();

app.Run();
