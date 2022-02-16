using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ProjetoSalesWebMvc.Data;
using ProjetoSalesWebMvc.Services;

namespace LanchesMac;
public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services) {

        services.AddControllersWithViews();


        services.AddScoped<SellerService>();

        services.AddScoped<DepartmentService>();

        // Registrando serviço para popular as tabelas
        services.AddScoped<SeedingService>();


        services.AddScoped<SalesRecordService>();


        // Comando criado a partir da classe context(CURSO)
        services.AddDbContext<SalesWebMvcContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    }

    // Configure configura conportamento das requisições
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService) {

        // Aplicar localização(USA)
        var enUS = new CultureInfo("en-Us");
        var localizationOptions = new RequestLocalizationOptions {

            DefaultRequestCulture = new RequestCulture(enUS),
            SupportedCultures = new List<CultureInfo> { enUS },
            SupportedUICultures = new List<CultureInfo> { enUS }
        };
        app.UseRequestLocalization(localizationOptions);



        if (env.IsDevelopment()) {

            // Perfil de desenvolvimento
            app.UseDeveloperExceptionPage();
            seedingService.Seed();// Chamando para popular a base de dados
        } else {

            // Perfil de produção
            app.UseExceptionHandler("/Home/Error");

            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();





        app.UseEndpoints(endpoints => {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");// Rota básica da aplicação
        });
    }
}