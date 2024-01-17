using HelpDeskApp.Data;
using HelpDeskApp.Helpers;
using HelpDeskApp.Models;
using HelpDeskApp.Models.CommonViewModel;
using HelpDeskApp.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped<ApplicationDbContext>();
var _ApplicationInfo = builder.Configuration.GetSection("ApplicationInfo").Get<ApplicationInfo>();

string _GetConnString = builder.Configuration.GetConnectionString("DefaultConnectionMSSQLNoCred");
if (_ApplicationInfo.IsMSSQLDB == 1)
{

    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_GetConnString));
}
else
{
    builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySql(_GetConnString, ServerVersion.AutoDetect(_GetConnString)));
}

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

DefaultIdentityOptions _DefaultIdentityOptions = null;

var _IServiceScopeFactory = builder.Services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
var _CreateScope = _IServiceScopeFactory.CreateScope();
var _ServiceProvider = _CreateScope.ServiceProvider;
var _context = _ServiceProvider.GetRequiredService<ApplicationDbContext>();
bool IsDBCanConnect = _context.Database.CanConnect();


if (IsDBCanConnect && _context.DefaultIdentityOptions.Count() > 0)
    _DefaultIdentityOptions = _context.DefaultIdentityOptions.Where(x => x.Id == 1).SingleOrDefault();
else
{
    IConfigurationSection _IConfigurationSection = builder.Configuration.GetSection("IdentityDefaultOptions");
    builder.Services.Configure<DefaultIdentityOptions>(_IConfigurationSection);
    _DefaultIdentityOptions = _IConfigurationSection.Get<DefaultIdentityOptions>();
}


AddIdentityOptions.SetOptions(builder.Services, _DefaultIdentityOptions);

// Get Super Admin Default options
builder.Services.Configure<SuperAdminDefaultOptions>(builder.Configuration.GetSection("SuperAdminDefaultOptions"));

builder.Services.AddTransient<ICommon, Common>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IRoles, Roles>();
builder.Services.AddTransient<IFunctional, Functional>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});



var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseSession();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();


//var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;
//var _WebApplicationOptions = services.GetRequiredService<WebApplicationOptions>();
//RotativaConfiguration.Setup(_WebApplicationOptions.WebRootPath, "Rotativa");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Dashboard}/{action=Index}/{id?}");
});

ProgramTaskExtension.SeedingData(app);
app.Run();
