using GestionObrasArte.ConsoleApp;

var apiService = new ApiService();
bool salir = false;

Console.WriteLine("--- Gestión de Tipos de Pintura ---");

while (!salir)
{
    Console.WriteLine("\nSelecciona una opción:");
    Console.WriteLine("1. Listar todos los tipos");
    Console.WriteLine("2. Filtrar tipos por título");
    Console.WriteLine("3. Agregar nuevo tipo");
    Console.WriteLine("4. Modificar un tipo");
    Console.WriteLine("5. Eliminar un tipo");
    Console.WriteLine("6. Salir");
    Console.Write("Opción: ");

    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            await apiService.ListarTiposPintura();
            break;
        case "2":
            Console.Write("Introduce el título a filtrar: ");
            string? filtro = Console.ReadLine();
            await apiService.ListarTiposPintura(filtro);
            break;
        case "3":
            await apiService.AgregarTipoPintura();
            break;
        case "4":
            await apiService.ModificarTipoPintura();
            break;
        case "5":
            await apiService.EliminarTipoPintura();
            break;
        case "6":
            salir = true;
            break;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}

Console.WriteLine("¡Adiós!");