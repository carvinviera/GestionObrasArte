using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionObrasArte.MauiApp.Services;
using GestionObrasArte.MauiApp.Views;
using GestionObrasArte.Shared.Models;

namespace GestionObrasArte.MauiApp.ViewModels
{
    public partial class PinturasListViewModel : ObservableObject
    {
        private readonly PinturasApiService _apiService;

        [ObservableProperty]
        private ObservableCollection<Pintura> _pinturas = new();

        [ObservableProperty]
        private string _filtroTitulo = string.Empty;

        [ObservableProperty]
        private bool _isBusy;

        public PinturasListViewModel(PinturasApiService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        private async Task LoadPinturasAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var pinturasList = await _apiService.GetPinturas(_filtroTitulo);
                Pinturas.Clear();
                foreach (var p in pinturasList)
                {
                    Pinturas.Add(p);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error cargando pinturas: {ex.Message}");
                // Mostrar alerta al usuario
                await Shell.Current.DisplayAlert("Error", "No se pudieron cargar las pinturas.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoToDetailAsync(Pintura? pintura)
        {
            // Navegamos a la página de detalle, enviando la pintura (o null si es nueva)
            var navigationParameters = new Dictionary<string, object>
            {
                { "Pintura", pintura ?? new Pintura() }
            };
            await Shell.Current.GoToAsync(nameof(PinturaDetailPage), true, navigationParameters);
        }

        [RelayCommand]
        private async Task DeletePinturaAsync(int id)
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmar", "¿Eliminar esta pintura?", "Sí", "No");
            if (confirm)
            {
                await _apiService.DeletePintura(id);
                await LoadPinturasAsync(); // Recargar lista
            }
        }
    }
}