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

    #region MVC�]�w
    // Add services to the container.
    // �i�b���椤Compile
    // �ϥΦh�y�t
    builder.Services.AddMvc()
                    .AddRazorRuntimeCompilation()
                    .AddViewLocalization();
    #endregion

    #region �̿�`�J
    //�@����U�覡
    //builder.Services.AddScoped<ILoginService, LoginServiceImpl>();
    //builder.Services.AddScoped<ILoginDao, LoginDaoImpl>();
    //�ϥ�Scrutor�NImpl�����B�ͩR�g���ۦP������,�Τ@���U
    builder.Services.Scan(
        scan => scan.FromCallingAssembly()
                    .AddClasses(
                        classes => classes.Where(t => t.Name.EndsWith("Impl", StringComparison.OrdinalIgnoreCase)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

    //�ӧO���USessionHelper�AHttpContextAccessor
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped<SessionHelper>();
    #endregion

    #region ���UDB�s�u
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

    #region AutoMapper�]�w
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    #endregion

    #region Session�]�w
    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        //options.Cookie.HttpOnly = true;
        //options.Cookie.IsEssential = true;
    });
    #endregion

    #region �h�y�t�]�w
    builder.Services.AddLocalization(option =>
    {
        option.ResourcesPath = "Resources";
    });
    #endregion

    #region Serilog �]�w
    builder.Host.UseSerilog();
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();
    #endregion

    #region �s�X�]�w
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

    #region �h�y�t�]�w
    //TODO ������
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

    #region Serilog�]�w
    app.UseSerilogRequestLogging(options =>
    {
        // �p�G�n�ۭq�T�����d���榡�A�i�H�ק�o�̡A���ק��ä��|�v�T���c�ưO�����ݩ�
        //options.MessageTemplate = "Handled {RequestPath}";

        // �w�]��X���������Ŭ� Information�A�A�i�H�b���ק�O������
        // options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

        // �A�i�H�q httpContext ���o HttpContext �U�Ҧ��i�H���o����T�I
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
