using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MatrixProcessor.Services;

var builder = WebApplication.CreateBuilder(args);

// Загрузка конфигурации из файлов и переменных среды
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables();

// Добавление сервисов в контейнер DI
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// Добавление CORS с использованием конфигурации
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Добавление логирования с использованием конфигурации
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

// Добавление проверки состояния приложения
builder.Services.AddHealthChecks();

// Регистрация сервиса матриц в контейнере DI
builder.Services.AddSingleton<IMatrixService, MatrixService>();

var app = builder.Build();

// Конфигурация обработки ошибок
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Middleware для обслуживания статических файлов
app.UseStaticFiles();

// Middleware для маршрутизации
app.UseRouting();

// Middleware для CORS
app.UseCors();

// Middleware для авторизации
app.UseAuthorization();

// Настройка конечных точек для Razor Pages и контроллеров
app.MapRazorPages();
app.MapControllers();

// Добавление проверки состояния приложения
app.MapHealthChecks("/health");

app.Run();