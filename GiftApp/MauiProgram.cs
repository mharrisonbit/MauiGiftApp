using CommunityToolkit.Maui;
using GiftApp.Interfaces;
using Prism.DryIoc;

namespace GiftApp;


public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp
            .CreateBuilder()
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseShinyFramework(
                new DryIocContainerExtension(),
                prism => prism.OnAppStart("NavigationPage/HomeView")
            )
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold"); 
            });

        builder.Configuration.AddJsonPlatformBundle();
        RegisterServices(builder);
        RegisterViews(builder.Services);

        return builder.Build();
    }


    static void RegisterServices(MauiAppBuilder builder)
    {
        var s = builder.Services;

        s.AddDataAnnotationValidation();
        s.AddSingleton<ISqliteConnection, MySqliteConnection>();

        s.AddGlobalCommandExceptionHandler(new(
#if DEBUG
            ErrorAlertType.FullError
#else
            ErrorAlertType.NoLocalize
#endif
        ));
    }


    static void RegisterViews(IServiceCollection s)
    {
        s.RegisterForNavigation<HomeView, HomeViewModel>();
        s.RegisterForNavigation<AddToListView, AddToListViewModel>();
        s.RegisterForNavigation<AddGiftView, AddGiftViewModel>();
        s.RegisterForNavigation<CompletedListView, CompletedListViewModel>();
    }
}
