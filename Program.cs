using ScopeIndia.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Configuration.GetConnectionString("DBConnect");
builder.Services.AddScoped<IStudent, StudentClass>();
builder.Services.AddScoped<ICourse, DerivedCourse>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.IsEssential = true; // Ensure session cookies are not blocked
});
builder.Services.AddDistributedMemoryCache(); // Required for session
builder.Services.AddHttpContextAccessor(); // Access session in controllers
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/WebHome/Login"; // Redirect to Login page if not authenticated
        options.LogoutPath = "/WebHome/Logout"; // Logout URL
        options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect if access is denied
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WebHome}/{action=Home}/{id?}");

app.Run();
