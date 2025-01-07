using BiteBox.Backend.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var webHost = CreateHostBuilder(args).Build();
        try
        {
            webHost.PrepareDbContext();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        webHost.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
