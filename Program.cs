using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер DI.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// Добавление CORS, если требуется доступ к API из других доменов.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins("https://example.com")
                     .AllowAnyHeader()
                     .AllowAnyMethod();
    });
});

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

// Middleware для перенаправления HTTP на HTTPS
app.UseHttpsRedirection();

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

// Можно также добавить проверку состояния приложения (Health Checks)
// app.MapHealthChecks("/health");

app.Run();