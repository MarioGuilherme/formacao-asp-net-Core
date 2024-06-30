using DevFreela.Application.Commands.CreateProject;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevFreelaCS")));

builder.Services.AddControllers();

builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining(typeof(CreateProjectCommand)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();