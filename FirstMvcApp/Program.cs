using FirstMvcApp.Models;
using Microsoft.EntityFrameworkCore;
using FirstMvcApp.Data;
using Microsoft.AspNetCore.Mvc.Razor;
using FirstMvcApp;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("PrimaryConnectionString");
builder.Services.AddDbContext<PeopleContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PrimaryConnectionString")));
builder.Services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FirstMvcAppContext>();
builder.Services.AddDbContext<FirstMvcAppContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews().
    AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).
        AddDataAnnotationsLocalization(options =>
        {
            options.DataAnnotationLocalizerProvider = (type, factory) =>
                factory.Create(typeof(SharedResource));
        });

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "MyAllowedOrigins",
        builder => builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

var app = builder.Build();

app.UseCors("MyAllowedOrigins");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

var supportedCultures = new[] { "en", "de-DE", "sr" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "attendant",
    pattern: "attendant/{firstName}-{lastName}",
    defaults: new
    {
        controller = "Home",
        action = "AttendantDetails"
    });

app.MapControllerRoute(
    name: "preview default",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{*slug}");

app.Run();