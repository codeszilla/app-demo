

using Microsoft.EntityFrameworkCore;
using MyApp.Server.Components;
using MyApp.Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7218/") });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<JwtAuthService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<LogoutService>();

var app = builder.Build();

// ================================
// HELPER FOR DB SLEEP ERRORS
// ======================================
string FriendlyStartupDbError(Exception ex)
{
    if (ex.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase) ||
        ex.InnerException?.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase) == true)
        return "Database timeout — The SQL server may be asleep or still waking up.";

    if (ex.Message.Contains("paused", StringComparison.OrdinalIgnoreCase) ||
        ex.InnerException?.Message.Contains("paused", StringComparison.OrdinalIgnoreCase) == true)
        return "Database is paused. Azure SQL is currently resuming.";

    return "A database error occurred during startup.";
}

// =====================================
// DB CHECK DURING STARTUP (SAFE)
// =====================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        _ = db.Departments.Any();
        Console.WriteLine("Database connection OK on startup.");
    }
    catch (Exception ex)
    {
        var friendly = FriendlyStartupDbError(ex);
        Console.WriteLine($"Database Warning: {friendly}");
        Console.WriteLine($"Raw error: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

//dotnet ef dbcontext scaffold "Server=LAandEnzo\SQLEXPRESS;Database=apps-demo;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context-dir Data --context AppDbContext --force

//dotnet ef dbcontext scaffold "Server=LAandEnzo\SQLEXPRESS;Database=apps-demo;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context-dir Data --context AppDbContext --force
//dotnet ef dbcontext scaffold "Server=.\SQLEXPRESS;Database=apps-demo;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Models -c MyDbContext

