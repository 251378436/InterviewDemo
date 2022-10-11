using API.Repositories;
using API.Services;
using API.Support.FileProvider;
using API.Support.Mapping;
using API.Support.Validation;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<RequestValidator>(ServiceLifetime.Transient);

builder.Services.AddAutoMapper(exp => exp.AddProfile(typeof(AutoMapperProfile)));
builder.Services.AddSingleton<IGuestService, GuestService>();
builder.Services.AddSingleton<IDataManager, DataManager>();
builder.Services.AddSingleton<FileProviderService>();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyHeader().AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
