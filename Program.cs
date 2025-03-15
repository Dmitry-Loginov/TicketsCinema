using CustomIdentityApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketsCinema.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Снять ограничения на пароль
    options.Password.RequireDigit = false; // Не требовать цифры
    options.Password.RequireLowercase = false; // Не требовать строчные буквы
    options.Password.RequireUppercase = false; // Не требовать заглавные буквы
    options.Password.RequireNonAlphanumeric = false; // Не требовать специальные символы
    options.Password.RequiredLength = 1; // Минимальная длина пароля
    options.Password.RequiredUniqueChars = 0; // Уникальные символы
})
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Инициализация ролей и пользователей
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleInitializer.InitializeAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
