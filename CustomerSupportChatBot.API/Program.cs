using CustomerSupportChatBot.Core.Interfaces.Repositories;
using CustomerSupportChatBot.Core.Interfaces.Services;
using CustomerSupportChatBot.Core.Interfaces;
using CustomerSupportChatBot.Infrastructure.Repositories;
using CustomerSupportChatBot.Infrastructure.UnitOfWorks;
using CustomerSupportChatBot.Infrastructure.External.NLP;
using CustomerSupportChatBot.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
