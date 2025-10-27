using System.Net.Http.Json;
using GestionObrasArte.Shared.Models;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Maui.Devices; // Necesario para DeviceInfo y DevicePlatform

namespace GestionObrasArte.MauiApp.Services
{
    public class PinturasApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl; // Usaremos esta variable consistentemente

        public PinturasApiService()
        {
            _httpClient = new HttpClient();

            // 1. Configurar la URL base. Android debe usar 10.0.2.2 
            //    para acceder al localhost del host.
            //    La URL base debe terminar en /api.
            _apiBaseUrl = DeviceInfo.Platform == DevicePlatform.Android
                                ? "http://10.0.2.2:5103/api"
                                : "http://localhost:5103/api";
        }

        // =======================================================
        // MÉTODOS DE PINTURA (AÑADEN /pinturas A LA URL)
        // =======================================================

        public async Task<List<Pintura>> GetPinturas(string? titulo = null)
        {
            var url = $"{_apiBaseUrl}/pinturas"; // Concatenamos el endpoint /pinturas
            if (!string.IsNullOrWhiteSpace(titulo))
            {
                url += $"?titulo={titulo}";
            }

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<List<Pintura>>() ?? new List<Pintura>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener pinturas: {ex.Message}");
            }
            return new List<Pintura>();
        }

        public async Task<bool> AddPintura(Pintura pintura)
        {
            try
            {
                // Usamos la URL completa: Base + /pinturas
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/pinturas", pintura);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al agregar pintura: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdatePintura(int id, Pintura pintura)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/pinturas/{id}", pintura);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al actualizar pintura: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeletePintura(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/pinturas/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar pintura: {ex.Message}");
                return false;
            }
        }

        // =======================================================
        // MÉTODOS DE SELECCIÓN (AÑADEN /artistas o /tipospintura)
        // =======================================================

        public async Task<List<Artista>> GetArtistasAsync()
        {
            // Usamos la URL base y le concatenamos el endpoint /artistas
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/artistas");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Artista>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Artista>();
            }
            return new List<Artista>();
        }

        public async Task<List<TipoPintura>> GetTiposPinturaAsync()
        {
            // Usamos la URL base y le concatenamos el endpoint /tipospintura
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/tipospintura");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TipoPintura>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<TipoPintura>();
            }
            return new List<TipoPintura>();
        }
    }
}