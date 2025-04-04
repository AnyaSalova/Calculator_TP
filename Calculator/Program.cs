var builder = WebApplication.CreateBuilder(args);

// ��������� �������
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // ��� ������ TempData

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // ��� ������ TempData

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calculator}/{action=Index}/{id?}");

app.Run();