using FirstMvcApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FirstMvcApp.Data;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("PrimaryConnectionString");
builder.Services.AddDbContext<PeopleContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PrimaryConnectionString")));
builder.Services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<FirstMvcAppContext>();
builder.Services.AddDbContext<FirstMvcAppContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
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

app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

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