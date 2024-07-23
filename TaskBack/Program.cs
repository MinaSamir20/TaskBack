using Microsoft.EntityFrameworkCore;
using TaskBack.Models.Database;
using TaskBack.Models.Repositories.DepartmentRepo;
using TaskBack.Models.Repositories.ReminderRepo;
using TaskBack.Models.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Retrieve the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register repositories
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IReminderRepository, ReminderRepository>();

// Register email sender and reminder service
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddHostedService<ReminderService>();

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

app.UseAuthorization();

// Ensure WebSocket support is enabled
app.UseWebSockets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
