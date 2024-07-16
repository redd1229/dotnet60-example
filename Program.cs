using dotnet60_example.Contexts;
using dotnet60_example.Helpers;
using dotnet60_example.Helpers.Extensions;
using dotnet60_example.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Globalization;
using System.Text;

try
{
    Log.Information("Starting web host");

    var builder = WebApplication.CreateBuilder(args);

    #region MVC設定
    // Add services to the container.
    // 可在執行中Compile
    // 使用多語系
    builder.Services.AddMvc()
                    .AddRazorRuntimeCompilation()
                    .AddViewLocalization();
    #endregion

    #region 依賴注入
    //一般註冊方式
    //builder.Services.AddScoped<ILoginService, LoginServiceImpl>();
    //builder.Services.AddScoped<ILoginDao, LoginDaoImpl>();
    //使用Scrutor將Impl結尾且生命週期相同的物件,統一註冊
    builder.Services.Scan(
        scan => scan.FromCallingAssembly()
                    .AddClasses(
                        classes => classes.Where(t => t.Name.EndsWith("Impl", StringComparison.OrdinalIgnoreCase)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

    //個別註冊SessionHelper，HttpContextAccessor
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped<SessionHelper>();
    #endregion

    #region 註冊DB連線
    var apdbConnectionString = builder.Configuration.GetConnectionString("APDB")
        ?? throw new InvalidOperationException("Connection string 'APDB' not found.");
    var hrdbConnectionString = builder.Configuration.GetConnectionString("HRDB")
        ?? throw new InvalidOperationException("Connection string 'HRDB' not found.");

    builder.Services.AddDbContext<ApdbContext>(options =>
    {
        options.UseSqlServer(apdbConnectionString);
        if (builder.Environment.IsDevelopment())
        {
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
            options.LogTo(Console.WriteLine);
        }

    });
    builder.Services.AddDbContext<HrdbContext>(options =>
        options.UseSqlServer(hrdbConnectionString));
    #endregion

    #region AutoMapper設定
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    #endregion

    #region Session設定
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        //options.Cookie.HttpOnly = true;
        //options.Cookie.IsEssential = true;
    });
    #endregion

    #region 多語系設定
    builder.Services.AddLocalization(option =>
    {
        option.ResourcesPath = "Resources";
    });
    #endregion

    #region Serilog 設定
    builder.Host.UseSerilog();
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();
    #endregion

    #region 編碼設定
    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    #endregion

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();
    app.UseSession();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Login}/{id?}");
    app.MapRazorPages();

    #region 多語系設定
    //TODO 未完成
    var supportedCultures = new List<CultureInfo>()
            {
                new CultureInfo("zh"),
                new CultureInfo("en"),
            };
    app.UseRequestLocalization(new RequestLocalizationOptions()
    {
        DefaultRequestCulture = new RequestCulture("zh"),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures,
    });
    #endregion

    #region Serilog設定
    app.UseSerilogRequestLogging(options =>
    {
        // 如果要自訂訊息的範本格式，可以修改這裡，但修改後並不會影響結構化記錄的屬性
        //options.MessageTemplate = "Handled {RequestPath}";

        // 預設輸出的紀錄等級為 Information，你可以在此修改記錄等級
        // options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

        // 你可以從 httpContext 取得 HttpContext 下所有可以取得的資訊！
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            diagnosticContext.Set("UserID", httpContext.Session.GetObject<UserSession>("UserSession")?.UserId);
        };
    });
    #endregion

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
