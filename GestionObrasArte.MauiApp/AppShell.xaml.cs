using GestionObrasArte.MauiApp.Views;

namespace GestionObrasArte.MauiApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(PinturaDetailPage), typeof(PinturaDetailPage));
    }
}
