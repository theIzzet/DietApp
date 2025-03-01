using DietApp.Data;
using DietApp.Hubs;
using DietApp.MessageSection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Get the connection string from appsettings.jsonn
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register DataContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(connectionString));

// Register IdentityContext with the connection string
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlite(connectionString));

// Configure Identity with DietUser and DietRole
builder.Services.AddIdentity<DietUser, DietRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Login";
    options.AccessDeniedPath = "/Login/Login";
    options.SlidingExpiration=true;
    options.ExpireTimeSpan=TimeSpan.FromMinutes(5);
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<DietRole>>();
    await DataSeeder.SeedRoles(roleManager);

    var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
    await DataSeeder.SeedDietTypes(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


