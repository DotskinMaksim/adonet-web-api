using DotskinWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Настройка кэша для сессий
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;  // Только для HTTP-запросов
    options.Cookie.IsEssential = true;  // Необходимо для работы сессий
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax; // Для корректной работы с куки
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(7, 0, 0))));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Настройка CORS: разрешаем запросы с фронтенда (http://localhost:3000)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")  // Укажите свой фронтенд-домен
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();  // Разрешить отправку cookies с запросом
        });
});

var app = builder.Build();

// Использование CORS
app.UseCors("AllowFrontend");

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

app.UseSession();  // Использование сессий
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
