using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionObrasArte.MauiApp.Services;
using GestionObrasArte.Shared.Models;
using System.Collections.ObjectModel;

namespace GestionObrasArte.MauiApp.ViewModels
{
    // Habilitamos la recepción de parámetros de navegación
    [QueryProperty(nameof(Pintura), "Pintura")]
    public partial class PinturaDetailViewModel : ObservableObject
    {
        private readonly PinturasApiService _apiService;

        [ObservableProperty]
        private Pintura _pintura = new();

        [ObservableProperty]
        private string _pageTitle = "Nueva Pintura";

        public ObservableCollection<Artista> Artistas { get; } = new ObservableCollection<Artista>();
        public ObservableCollection<TipoPintura> TiposPintura { get; } = new ObservableCollection<TipoPintura>();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SavePinturaCommand))]
        private Artista selectedArtista;

        // 3. Propiedad para el elemento seleccionado en el Picker de TiposPintura
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SavePinturaCommand))]
        private TipoPintura selectedTipoPintura;

        public PinturaDetailViewModel(PinturasApiService apiService)
        {
            _apiService = apiService;
            LoadSelectionDataAsync();
        }

        [RelayCommand]
        public async Task LoadSelectionDataAsync()
        {
            // Cargar Artistas
            var artistas = await _apiService.GetArtistasAsync();
            Artistas.Clear();
            foreach (var a in artistas) Artistas.Add(a);

            // Cargar Tipos de Pintura
            var tiposPintura = await _apiService.GetTiposPinturaAsync();
            TiposPintura.Clear();
            foreach (var t in tiposPintura) TiposPintura.Add(t);

            // Si estamos editando, seleccionar los valores actuales
            if (Pintura.IdPintura != 0)
            {
                SelectedArtista = Artistas.FirstOrDefault(a => a.IdArtista == Pintura.Fk_IdArtista);
                SelectedTipoPintura = TiposPintura.FirstOrDefault(t => t.IdTipoPintura == Pintura.FK_IdTipoPintura);
            }
        }

        // Este método se dispara cuando la propiedad Pintura (QueryProperty) se establece
        partial void OnPinturaChanged(Pintura value)
        {
            // ... (Configuración de PageTitle) ...

            // Cuando Pintura cambia (ej. al recibirla por QueryProperty), 
            // intentamos cargar los valores seleccionados.
            if (Artistas.Any() && TiposPintura.Any())
            {
                // ASIGNACIÓN CORRECTA: Usar 'SelectedArtista' y 'SelectedTipoPintura' (Mayúscula)
                SelectedArtista = Artistas.FirstOrDefault(a => a.IdArtista == value.Fk_IdArtista);
                SelectedTipoPintura = TiposPintura.FirstOrDefault(t => t.IdTipoPintura == value.FK_IdTipoPintura);
                //             ^        ^
            }
        }

        [RelayCommand(CanExecute = nameof(CanSavePintura))]
        private async Task SavePinturaAsync()
        {
            // ASIGNAR LAS CLAVES FORÁNEAS ANTES DE ENVIAR
            if (SelectedArtista != null)
                Pintura.Fk_IdArtista = SelectedArtista.IdArtista;

            if (SelectedTipoPintura != null)
                Pintura.FK_IdTipoPintura = SelectedTipoPintura.IdTipoPintura;

            // ... (resto de la lógica de guardado)

            // ... (llamada a la API para guardar/actualizar Pintura)
            bool success = Pintura.IdPintura == 0
                ? await _apiService.AddPintura(Pintura)
                : await _apiService.UpdatePintura(Pintura.IdPintura, Pintura);

            if (success)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo guardar la pintura.", "Aceptar");
            }
        }

        private bool CanSavePintura()
        {
            return !string.IsNullOrWhiteSpace(Pintura.TituloPintura) &&
                   Pintura.Precio > 0 &&
                   SelectedArtista != null &&
                   SelectedTipoPintura != null;
        }
    }
}