using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GestionObrasArte.MauiApp.Services;
using GestionObrasArte.Shared.Models;
using System.Collections.ObjectModel;

namespace GestionObrasArte.MauiApp.ViewModels
{
    [QueryProperty(nameof(Pintura), "Pintura")]
    public partial class PinturaDetailViewModel : ObservableObject
    {
        private readonly PinturasApiService _apiService;

        [ObservableProperty]
        private Pintura _pintura = new();

        [ObservableProperty]
        private string _pageTitle = "Nueva Pintura";

        public ObservableCollection<Artista> Artistas { get; } = new();
        public ObservableCollection<TipoPintura> TiposPintura { get; } = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SavePinturaCommand))]
        private Artista selectedArtista;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SavePinturaCommand))]
        private TipoPintura selectedTipoPintura;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SavePinturaCommand))]
        private string titulo;

        [ObservableProperty]
        private decimal? precio;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SavePinturaCommand))]
        private string precioTexto;

        public PinturaDetailViewModel(PinturasApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task InitializeAsync()
        {
            await LoadSelectionDataAsync();
        }

        partial void OnTituloChanged(string value)
       {
            // Forzar que el botón de guardar se actualice
            SavePinturaCommand.NotifyCanExecuteChanged();
        }

        partial void OnPrecioChanged(decimal? value)
        {
            precio = !value.HasValue ? 0 : value.Value;
            SavePinturaCommand.NotifyCanExecuteChanged();
        }

        partial void OnPrecioTextoChanged(string value)
        {
            if (decimal.TryParse(value, out var result))
                Precio = result;
            else
                Precio = null; // vacío o inválido

            SavePinturaCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        public async Task LoadSelectionDataAsync()
        {
            var artistas = await _apiService.GetArtistasAsync();
            Artistas.Clear();
            foreach (var a in artistas) Artistas.Add(a);

            var tipos = await _apiService.GetTiposPinturaAsync();
            TiposPintura.Clear();
            foreach (var t in tipos) TiposPintura.Add(t);

            // Si estamos editando, asignar valores
            if (Pintura != null && Pintura.IdPintura != 0)
            {
                SelectedArtista = Artistas.FirstOrDefault(a => a.IdArtista == Pintura.Fk_IdArtista);
                SelectedTipoPintura = TiposPintura.FirstOrDefault(t => t.IdTipoPintura == Pintura.FK_IdTipoPintura);
                Titulo = Pintura.TituloPintura;
                Precio = Pintura.Precio;
            }
        }

        partial void OnPinturaChanged(Pintura value)
        {
            if (value == null) return;

            Titulo = value.TituloPintura;
            Precio = value.Precio;

            if (Artistas.Any())
                SelectedArtista = Artistas.FirstOrDefault(a => a.IdArtista == value.Fk_IdArtista);

            if (TiposPintura.Any())
                SelectedTipoPintura = TiposPintura.FirstOrDefault(t => t.IdTipoPintura == value.FK_IdTipoPintura);
        }

        [RelayCommand(CanExecute = nameof(CanSavePintura))]
        private async Task SavePinturaAsync()
        {
            if (SelectedArtista == null || SelectedTipoPintura == null) return;

            // Actualizar modelo con valores del formulario
            Pintura.TituloPintura = Titulo;
            Pintura.Precio = Precio.Value;
            Pintura.Fk_IdArtista = SelectedArtista.IdArtista;
            Pintura.FK_IdTipoPintura = SelectedTipoPintura.IdTipoPintura;

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
            return !string.IsNullOrWhiteSpace(Titulo)
                   && Precio.HasValue && Precio.Value > 0
                   && SelectedArtista != null
                   && SelectedTipoPintura != null;
        }

        [RelayCommand]
        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
