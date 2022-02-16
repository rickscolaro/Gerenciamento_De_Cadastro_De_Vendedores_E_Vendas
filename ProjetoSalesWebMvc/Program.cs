
namespace LanchesMac;
public class Program {

    public static void Main(string[] args) {

        CreateHostBuilder(args)
           .Build()
           .Run();
    }

    // CreateHostBuilder Aciona a classe Startup
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => {
                webBuilder.UseStartup<Startup>();
            });
}
