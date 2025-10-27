using GestionObrasArte.MauiApp.ViewModels;

namespace GestionObrasArte.MauiApp.Views;

public partial class PinturaDetailPage : ContentPage
{
    public PinturaDetailPage(PinturaDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PinturaDetailViewModel vm)
            await vm.InitializeAsync();
    }

}