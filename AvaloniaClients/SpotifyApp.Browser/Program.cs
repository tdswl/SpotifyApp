using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using Microsoft.Extensions.DependencyInjection;
using SpotifyApp.Browser.DI;
using SpotifyApp.Shared;

[assembly: SupportedOSPlatform("browser")]

internal partial class Program
{
    private static Task Main(string[] args)
        => AppBuilder.Configure(AppFactory)
            .WithInterFont()
            .LogToTrace()
            .StartBrowserAppAsync("out");
    
    private static App AppFactory()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddBrowser();
        return new App(serviceCollection);
    }
}