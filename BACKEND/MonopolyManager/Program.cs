using MonopolyManager.Data;

namespace MonopolyManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IGameRepository, GameRepository>();


            var app = builder.Build();
            
            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapGet("/", () => "Hello World!");

            app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin());

            app.Run();
        }
    }
}
