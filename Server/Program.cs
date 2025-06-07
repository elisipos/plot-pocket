using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlotPocket.Server.Models.Entities;
using PlotPocket.Server.Services;
using Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    // options.Cookie.HttpOnly = true;

    options.Events.OnRedirectToLogin = context => {
        if (context.Request.Path.StartsWithSegments("/api")) {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        }
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<ShowService>();
builder.Services.AddSingleton<TMDBService>();

// // Use sessions for user authorization
// builder.Services.AddSession(options => {
//     options.IdleTimeout = TimeSpan.FromHours(1);    
//     options.Cookie.SameSite = SameSiteMode.None;
//     options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//     options.Cookie.Name = ".plotpocket.Session";
//     options.Cookie.HttpOnly = true;
//     options.Cookie.IsEssential = true;

// });

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAllKnownOrigins", policy => {
        policy.WithOrigins(
            "https://animated-sunshine-e1d49b.netlify.app",
            "http://localhost:5000",
            "http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllKnownOrigins");
app.UseRouting();
// app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// app.MapRazorPages()
//    .WithStaticAssets();

app.MapFallbackToFile("index.html");

app.Run();

