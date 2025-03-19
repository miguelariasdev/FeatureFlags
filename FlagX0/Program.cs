using FlagX0.Data; // Imports the namespace containing the database context
using Microsoft.AspNetCore.Identity; // Imports ASP.NET Core Identity for user authentication and management
using Microsoft.EntityFrameworkCore; // Imports Entity Framework Core for database operations

var builder = WebApplication.CreateBuilder(args); // Creates a web application builder with default configurations

// Add services to the container.

// Retrieves the connection string from the configuration file (appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configures the application to use Entity Framework Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString));

// Adds support for detailed database-related exception pages during development
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configures ASP.NET Core Identity with default settings, requiring confirmed accounts for sign-in
// and storing user data in the configured database context
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Adds support for controllers and views (MVC pattern)
builder.Services.AddControllersWithViews();

var app = builder.Build(); // Builds the application, preparing it for request handling

//Aplica migraciones automáticamente antes de ejecutar la aplicación**
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate(); // Aplica todas las migraciones pendientes a la base de datos
}

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment()) // Checks if the application is running in the Development environment
{
    app.UseMigrationsEndPoint(); // Enables automatic database migrations in development mode
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Redirects to an error page when an exception occurs in production
    app.UseHsts(); // Enforces HTTP Strict Transport Security (HSTS) for added security
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS for secure communication
app.UseStaticFiles(); // Enables serving of static files (CSS, JavaScript, images, etc.)

app.UseRouting(); // Enables request routing, allowing the application to determine which endpoint to invoke

app.UseAuthorization(); // Enables authorization, ensuring users have access to certain routes

// Configures the default route for MVC controllers (HomeController -> Index action by default)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Maps Razor Pages, enabling support for Razor-based views
app.MapRazorPages();

app.Run(); // Starts the application, making it ready to handle incoming HTTP requests
