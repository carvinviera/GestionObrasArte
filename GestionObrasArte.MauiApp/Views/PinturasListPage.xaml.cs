using GestionObrasArte.MauiApp.ViewModels;

namespace GestionObrasArte.MauiApp.Views;

public partial class PinturasListPage : ContentPage
{
    public PinturasListPage(PinturasListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    // Recargar datos cuando la página aparece
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as PinturasListViewModel)?.LoadPinturasCommand.Execute(null);
    }
}