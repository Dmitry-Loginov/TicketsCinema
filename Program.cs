using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketsCinema.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// �������� ������ ����������� �� ����� ������������
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // ����� ����������� �� ������
    options.Password.RequireDigit = false; // �� ��������� �����
    options.Password.RequireLowercase = false; // �� ��������� �������� �����
    options.Password.RequireUppercase = false; // �� ��������� ��������� �����
    options.Password.RequireNonAlphanumeric = false; // �� ��������� ����������� �������
    options.Password.RequiredLength = 1; // ����������� ����� ������
    options.Password.RequiredUniqueChars = 0; // ���������� �������
})
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

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
