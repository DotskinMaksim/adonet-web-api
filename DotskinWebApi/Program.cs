using DotskinWebApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache(); // Кэш для сессий
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(7, 0, 0))));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseSession(); 
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(options => options
    .WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseAuthorization();

app.MapControllers();

app.Run();