using System.Net.Http.Json;
using GestionObrasArte.Shared.Models;

namespace GestionObrasArte.ConsoleApp
{
    public class ApiService
    {
        // IMPORTANTE: Ajusta esta URL a la que usa tu API
        private const string ApiBaseUrl = "http://localhost:5123/api/tipospintura";
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task ListarTiposPintura(string? titulo = null)
        {
            string url = ApiBaseUrl;
            if (!string.IsNullOrWhiteSpace(titulo))
            {
                url += $"?titulo={titulo}";
            }

            var tipos = await _httpClient.GetFromJsonAsync<List<TipoPintura>>(url);
            Console.WriteLine("\n--- Listado de Tipos de Pintura ---");
            if (tipos == null || tipos.Count == 0)
            {
                Console.WriteLine("No se encontraron tipos de pintura.");
                return;
            }
            foreach (var tipo in tipos)
            {
                Console.WriteLine($"[ID: {tipo.IdTipoPintura}] - {tipo.TítuloTipoPintura}");
            }
        }

        public async Task AgregarTipoPintura()
        {
            Console.Write("Introduce el título del nuevo tipo: ");
            string titulo = Console.ReadLine() ?? "";
            var nuevoTipo = new TipoPintura { TítuloTipoPintura = titulo };

            var response = await _httpClient.PostAsJsonAsync(ApiBaseUrl, nuevoTipo);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("¡Tipo de pintura agregado con éxito!");
            }
            else
            {
                Console.WriteLine("Error al agregar el tipo de pintura.");
            }
        }

        public async Task ModificarTipoPintura()
        {
            Console.Write("Introduce el ID del tipo a modificar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID no válido.");
                return;
            }

            Console.Write("Introduce el nuevo título: ");
            string nuevoTitulo = Console.ReadLine() ?? "";

            var tipoModificado = new TipoPintura { IdTipoPintura = id, TítuloTipoPintura = nuevoTitulo };
            var response = await _httpClient.PutAsJsonAsync($"{ApiBaseUrl}/{id}", tipoModificado);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("¡Tipo de pintura modificado con éxito!");
            }
            else
            {
                Console.WriteLine("Error o tipo no encontrado.");
            }
        }

        public async Task EliminarTipoPintura()
        {
            Console.Write("Introduce el ID del tipo a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID no válido.");
                return;
            }

            var response = await _httpClient.DeleteAsync($"{ApiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("¡Tipo de pintura eliminado con éxito!");
            }
            else
            {
                Console.WriteLine("Error o tipo no encontrado.");
            }
        }
    }
}