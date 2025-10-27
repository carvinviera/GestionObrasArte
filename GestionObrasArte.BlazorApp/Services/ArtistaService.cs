using GestionObrasArte.Shared.Models;

namespace GestionObrasArte.BlazorApp.Services
{
    public class ArtistasService
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "http://localhost:5103/api/artistas";

        public ArtistasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ✅ Obtener lista de artistas (con filtro opcional)
        public async Task<List<Artista>> GetArtistasAsync(string? nombre = null)
        {
            string url = ApiBaseUrl;
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                url += $"?nombre={nombre}";
            }

            return await _httpClient.GetFromJsonAsync<List<Artista>>(url)
                   ?? new List<Artista>();
        }

        // ✅ Agregar un nuevo artista
        public async Task<bool> AddArtistaAsync(Artista artista)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiBaseUrl, artista);
            return response.IsSuccessStatusCode;
        }

        // ✅ Editar un artista existente
        public async Task<bool> UpdateArtistaAsync(Artista artista)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiBaseUrl}/{artista.IdArtista}", artista);
            return response.IsSuccessStatusCode;
        }

        // ✅ Eliminar un artista
        public async Task<bool> DeleteArtistaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiBaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
