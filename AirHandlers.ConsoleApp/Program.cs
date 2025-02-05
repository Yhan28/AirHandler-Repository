using System;
using System.Linq;
using AirHandlers.Contracts.AirHandlers;
using AirHandlers.Data.Repositories;
using AirHandlers.DataAccess.Contexts;
using AirHandlers.Domain.Entities;
using AirHandlers.Domain.Relations; // Para manejar relaciones Many-to-Many
using Microsoft.EntityFrameworkCore;

namespace AirHandlers.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Configuración del contexto de la base de datos en memoria para pruebas
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("AirHandlersDb") // Base de datos en memoria para pruebas
                .Options;

            using var context = new ApplicationDbContext(options);
            var airHandlerRep = new AirHandlerRepository(context);
            var recipeRepo = new RecipeRepository(context);
            var roomRepo = new RoomRepository(context);
            AirHandlerRecipeRepository airHandlerRecipeRepo = new AirHandlerRecipeRepository(context);
            
            while (true)
            {
                Console.WriteLine("\nSeleccione una opción:");
                Console.WriteLine("1 - Manejadoras de Aire");
                Console.WriteLine("2 - Recetas");
                Console.WriteLine("3 - Habitaciones");
                Console.WriteLine("0 - Salir");

                var option = Console.ReadLine();


                // Crear repositorios
                var airHandlerRe = new AirHandlerRepository(context);
                switch (option)
                {
                    case "1":
                        ManageAirHandlers(airHandlerRep, recipeRepo, roomRepo);
                        break;
                    case "2":
                        ManageRecipes(recipeRepo, airHandlerRep, airHandlerRecipeRepo);
                        break;
                    case "3":
                        ManageRooms(roomRepo, airHandlerRep);
                        break;
                    case "0":
                        return; // Salir del programa
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
        }

        // ==========================
        // Operaciones: AirHandlers
        // ==========================
        private static void ManageAirHandlers(AirHandlerRepository airHandlerRepo, RecipeRepository recipeRepo, RoomRepository roomRepo)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("AirHandlersDb") // Base de datos en memoria para pruebas
               .Options;
            using var context = new ApplicationDbContext(options);
            var airHandlerRecipeRepo = new AirHandlerRecipeRepository(context);

            Console.WriteLine("\n=== Gestión de Manejadoras de Aire ===");
            Console.WriteLine("1 - Crear Manejadora de Aire");
            Console.WriteLine("2 - Listar Manejadoras de Aire");
            Console.WriteLine("3 - Actualizar Manejadora de Aire");
            Console.WriteLine("4 - Eliminar Manejadora de Aire");
            Console.WriteLine("5 - Seleccionar Manejadora de Aire para relacionar con Recetas");
            Console.WriteLine("6 - Seleccionar Manejadora de Aire para relacionar con Habitaciones");
            Console.WriteLine("0 - Volver al menú principal");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    CreateAirHandler(airHandlerRepo);
                    break;
                case "2":
                    ListAirHandlers(airHandlerRepo);
                    break;
                case "3":
                    UpdateAirHandler(airHandlerRepo);
                    break;
                case "4":
                    DeleteAirHandler(airHandlerRepo);
                    break;
                case "5":
                    ManageRecipeAssociationsForAirHandler(airHandlerRepo, recipeRepo, airHandlerRecipeRepo);
                    break;
                case "6":
                    ManageRoomAssociationsForAirHandler(airHandlerRepo, roomRepo);
                    break;
                case "0":
                    return; // Volver al menú principal
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        private static void CreateAirHandler(AirHandlerRepository airHandlerRepo)
        {
            Console.Write("Ingrese el código identificador: ");
            var identifierCode = Console.ReadLine();

            Console.Write("¿Está operativa? (true/false): ");
            var isOperating = bool.Parse(Console.ReadLine());

            Console.Write("Ingrese la fecha del último cambio del filtro (yyyy-MM-dd): ");
            var filterChangeDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Ingrese la temperatura de referencia: ");
            var referenceTemperature = double.Parse(Console.ReadLine());

            Console.Write("Ingrese la humedad de referencia: ");
            var referenceHumidity = double.Parse(Console.ReadLine());

            var airHandler = new AirHandlerEntity(
                identifierCode: identifierCode,
                isOperating: isOperating,
                filterChangeDate: filterChangeDate,
                referenceTemperature: referenceTemperature,
                referenceHumidity: referenceHumidity
            );

            airHandlerRepo.AddAirHandler(airHandler);
            Console.WriteLine($"Manejadora de Aire creada con ID: {airHandler.Id}");
        }

        private static void ListAirHandlers(AirHandlerRepository airHandlerRepo)
        {
            var allAirHandlers = airHandlerRepo.GetAllAirHandler<AirHandlerEntity>().ToList();
            foreach (var ah in allAirHandlers)
            {
                Console.WriteLine($"ID: {ah.Id}, Código: {ah.IdentifierCode}, Operativa: {ah.IsOperating}");
                Console.WriteLine($"Último cambio del filtro: {ah.FilterChangeDate}, Temperatura Ref.: {ah.ReferenceTemperature}, Humedad Ref.: {ah.ReferenceHumidity}");
            }
        }

        private static void UpdateAirHandler(AirHandlerRepository airHandlerRepo)
        {
            Console.Write("Ingrese el ID de la Manejadora de Aire a actualizar: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                var airHandler = airHandlerRepo.GetAirHandlerById<AirHandlerEntity>(id);
                if (airHandler != null)
                {
                    airHandler.IsOperating = !airHandler.IsOperating; // Alternar estado operativo
                    airHandlerRepo.UpdateAirHandler(airHandler);
                    Console.WriteLine($"Estado operativo actualizado a: {airHandler.IsOperating}");
                }
                else
                {
                    Console.WriteLine("Manejadora de Aire no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID no válido.");
            }
        }

        private static void DeleteAirHandler(AirHandlerRepository airHanderRepo)
        {
            Console.Write("Ingrese el ID de la Manejadora de Aire a eliminar: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                airHanderRepo.DeleteAirHandler(id);
                Console.WriteLine($"Manejadora de Aire con ID {id} eliminada.");
            }
            else
            {
                Console.WriteLine("ID no válido.");
            }
        }

        private static void ManageRecipeAssociationsForAirHandler(AirHandlerRepository airhandlerrepo, RecipeRepository recipe_repo, AirHandlerRecipeRepository airHandlerRecipeRepo)
        {
            Console.Write("Ingrese el ID de la Manejadora de Aire: ");
            if (Guid.TryParse(Console.ReadLine(), out var airhandlerId))
            {
                while (true)
                {
                    Console.WriteLine("\n=== Relación entre Recetas y esta Manejadora de Aire ===");
                    Console.WriteLine("1 - Asociar una Receta a esta Manejadora de Aire");
                    Console.WriteLine("2 - Desasociar una Receta a esta Manejadora de Aire");
                    Console.WriteLine("3 - Listar Recetas asociadas a esta Manejadora de Aire");
                    Console.WriteLine("0 - Volver al menú anterior");

                    var option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            AssociateRecipeToAirhandler(airhandlerrepo, recipe_repo, airHandlerRecipeRepo);
                            break;
                        case "2":
                            DisassociateRecipeFromAirhandler(airhandlerrepo, recipe_repo, airHandlerRecipeRepo);
                            break;
                        case "3":
                            ListAssociatedRecipes(airhandlerrepo, airHandlerRecipeRepo);
                            break;
                        case "0":
                            return; // Volver al menú anterior
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
            }
        }

        private static async Task AssociateRecipeToAirhandler(AirHandlerRepository airhandlerrepo, RecipeRepository recipe_repo, AirHandlerRecipeRepository airHandlerRecipeRepo)
        {
            Console.Write("Ingrese el ID del Air Handler: ");
            if (Guid.TryParse(Console.ReadLine(), out var airhandlerId))
            {
                Console.Write("Ingrese el ID de la Recipe a asociar: ");
                if (Guid.TryParse(Console.ReadLine(), out var recipeId))
                {
                    await airHandlerRecipeRepo.AddAsync(new AirHandlerRecipe { RecipeID = recipeId, AirHandlerID = airhandlerId });
                    Console.WriteLine($"Receta {recipeId} asociada a la Manejadora {airhandlerId}.");
                }
                else
                {
                    Console.WriteLine("ID de Receta no válido.");
                }
            }
        }

        private static async Task DisassociateRecipeFromAirhandler(AirHandlerRepository airhandlerrepo, RecipeRepository recipe_repo, AirHandlerRecipeRepository airHandlerRecipeRepo)
        {
            Console.Write("Ingrese el ID del Air Handler: ");
            if (Guid.TryParse(Console.ReadLine(), out var airhandlerId))
            {
                Console.Write("Ingrese el ID de la Recipe a desasociar: ");
                if (Guid.TryParse(Console.ReadLine(), out var recipeId))
                {
                    await airHandlerRecipeRepo.DeleteAsync(airhandlerId, recipeId);
                    Console.WriteLine($"Receta {recipeId} desasociada del Air Handler {airhandlerId}.");
                }
                else
                {
                    Console.WriteLine("ID de Receta no válido.");
                }
            }
        }

        private static async Task ListAssociatedRecipes(AirHandlerRepository airhandlerrepo, AirHandlerRecipeRepository airHandlerRecipeRepo)
        {
            Console.Write("Ingrese el ID del Air Handler para listar recetas asociadas: ");
            if (Guid.TryParse(Console.ReadLine(), out var airhandlerId))
            {
                var associatedRecipes = await airHandlerRecipeRepo.GetAllAsync();

                var recipes = associatedRecipes.Where(ar => ar.AirHandlerID == airhandlerId).Select(ar => ar.RecipeID).ToList();

                if (recipes.Count > 0)
                {
                    foreach (var recipe in recipes)
                    {
                        Console.WriteLine($"Receta asociada ID: {recipe}");
                    }
                }
                else
                {
                    Console.WriteLine($"No hay recetas asociadas a la Manejadora con ID: {airhandlerId}");
                }
            }
        }
        private static void ManageRoomAssociationsForAirHandler(AirHandlerRepository airhandlerrepo, RoomRepository roomRepo)
        {
            Console.Write("Ingrese el ID de la Manejadora de Aire: ");
            if (Guid.TryParse(Console.ReadLine(), out var airhandlerId))
            {
                while (true)
                {
                    Console.WriteLine("\n=== Relación entre Habitaciones y esta Manejadora de Aire ===");
                    Console.WriteLine("1 - Asociar una Habitación a esta Manejadora de Aire");
                    Console.WriteLine("2 - Desasociar una Habitación de esta Manejadora de Aire");
                    Console.WriteLine("3 - Listar Habitaciones asociadas a esta Manejadora de Aire");
                    Console.WriteLine("0 - Volver al menú anterior");

                    var option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            AssociateRoomToAirhandler(airhandlerrepo, roomRepo, airhandlerId);
                            break;
                        case "2":
                            DisassociateRoomFromAirhandler(airhandlerrepo, roomRepo, airhandlerId);
                            break;
                        case "3":
                            ListRoomsForAirHandler(roomRepo, airhandlerId);
                            break;
                        case "0":
                            return; // Volver al menú anterior
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("ID de Manejadora de Aire no válido.");
            }
        }

        private static void AssociateRoomToAirhandler(AirHandlerRepository airhandlerrepo, RoomRepository roomRepo, Guid airhandlerId)
        {
            Console.Write("Ingrese el ID de la Habitación a asociar: ");
            if (Guid.TryParse(Console.ReadLine(), out var roomId))
            {
                var room = roomRepo.GetRoomById<Room>(roomId);
                if (room != null)
                {
                    room.AssociatedHandlerId = airhandlerId; // Asocia la habitación al Air Handler.
                    roomRepo.UpdateRoom(room); // Actualiza la habitación en la base de datos.
                    Console.WriteLine($"Habitación {room.Id} asociada a la Manejadora de Aire {airhandlerId}.");
                }
                else
                {
                    Console.WriteLine("Habitación no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID de Habitación no válido.");
            }
        }

        private static void DisassociateRoomFromAirhandler(AirHandlerRepository airhandlerrepo, RoomRepository roomRepo, Guid airhandlerId)
        {
            Console.Write("Ingrese el ID de la Habitación a desasociar: ");
            if (Guid.TryParse(Console.ReadLine(), out var roomId))
            {
                var room = roomRepo.GetRoomById<Room>(roomId);
                if (room != null)
                {
                    room.AssociatedHandlerId = Guid.Empty; // Desasocia la habitación.
                    roomRepo.UpdateRoom(room); // Actualiza la habitación en la base de datos.
                    Console.WriteLine($"Habitación {room.Id} desasociada de la Manejadora de Aire {airhandlerId}.");
                }
                else
                {
                    Console.WriteLine("Habitación no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID de Habitación no válido.");
            }
        }

        private static void ListRoomsForAirHandler(RoomRepository roomRepo, Guid airhandlerId)
        {
            var rooms = roomRepo.GetAllRooms<Room>()
                                 .Where(r => r.AssociatedHandlerId == airhandlerId)
                                 .ToList();

            if (rooms.Count > 0)
            {
                foreach (var room in rooms)
                {
                    Console.WriteLine($"Habitación asociada ID: {room.Id}, Número: {room.Number}, Volumen: {room.Volume}");
                }
            }
            else
            {
                Console.WriteLine($"No hay habitaciones asociadas a la Manejadora con ID: {airhandlerId}");
            }
        }

        // ==========================
        // Operaciones: Recipes
        // ==========================
        private static void ManageRecipes(RecipeRepository recipe_repo, AirHandlerRepository airHandlerRepositoy, AirHandlerRecipeRepository airHandlerRecipeRepo)
        {
            Console.WriteLine("\n=== Gestión de Recetas ===");
            Console.WriteLine("1 - Crear Receta");
            Console.WriteLine("2 - Listar Recetas");
            Console.WriteLine("3 - Actualizar Receta");
            Console.WriteLine("4 - Eliminar Receta");
            Console.WriteLine("5 - Seleccionar Receta para relacionar con Manejadoras");
            Console.WriteLine("0 - Volver al menú principal");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    CreateRecipe(recipe_repo);
                    break;
                case "2":
                    ListRecipes(recipe_repo);
                    break;
                case "3":
                    UpdateRecipe(recipe_repo);
                    break;
                case "4":
                    DeleteRecipe(recipe_repo);
                    break;
                case "5":
                    ManageRecipeAssociationsForAirHandler(airHandlerRepositoy, recipe_repo, airHandlerRecipeRepo);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        private static async Task ManageAssociationsWithHandlersAsync(RecipeRepository recipe_repo, AirHandlerRepository airhandlerrepo, AirHandlerRecipeRepository airHandlerRecipeRepo)
        {
            Console.Write("Ingrese el ID de la receta: ");
            if (Guid.TryParse(Console.ReadLine(), out var recipeId))
            {
                while (true)
                {
                    Console.WriteLine("\n=== Asociaciones entre Receta y Manejadoras ===");
                    Console.WriteLine("1 - Asociar una Manejadora a esta Receta");
                    Console.WriteLine("2 - Desasociar una Manejadora a esta Receta");
                    Console.WriteLine("3 - Listar Manejadoras asociadas a esta Receta");
                    Console.WriteLine("0 - Volver al menú anterior");

                    var option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            await AssociateAirhandlersToRecipe(airHandlerRecipeRepo, recipeId); // Aquí pasas ambos parámetros
                            break;
                        case "2":
                            await DisassociateAirhandlersFromRecipe(airHandlerRecipeRepo, recipeId);
                            break;
                        case "3":
                            await ListAssociatedHandlers(airHandlerRecipeRepo, recipeId);
                            break;
                        case "0":
                            return; // Volver al menú anterior
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("ID de receta no válido.");
            }
        }

        private static async Task AssociateAirhandlersToRecipe(AirHandlerRecipeRepository airHandlerRecipeRepo, Guid recipeId)
        {
            Console.Write("Ingrese el ID del Handler: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid handlerId))
            {
                var association = new AirHandlerRecipe { RecipeID = recipeId, AirHandlerID = handlerId };
                await airHandlerRecipeRepo.AddAsync(association);
                Console.WriteLine($"Asociación creada entre Handler: {handlerId} y receta: {recipeId}.");
            }
            else
            {
                Console.WriteLine("ID de Handler no válido.");
            }
        }

        private static async Task DisassociateAirhandlersFromRecipe(AirHandlerRecipeRepository airHandlerRecipeRepo, Guid recipeId)
        {
            Console.Write("Ingrese el ID del Handler: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid handlerId))
            {
                await airHandlerRecipeRepo.DeleteAsync(handlerId, recipeId);
                Console.WriteLine($"Desasociación creada entre Handler: {handlerId} y receta: {recipeId}.");
            }
            else
            {
                Console.WriteLine("ID de Handler no válido.");
            }
        }

        private static async Task ListAssociatedHandlers(AirHandlerRecipeRepository airHandlerRecipeRepo, Guid recipeId)
        {
            var handlers = await airHandlerRecipeRepo.GetAllAsync(); // Obtener todos los AirHandlerRecipe

            var associatedHandlers = handlers
                .Where(ar => ar.RecipeID == recipeId) // Filtrar por receta
                .Select(ar => ar.AirHandlerID) // Seleccionar solo los IDs de los manejadores
                .ToList();

            if (associatedHandlers.Count > 0)
            {
                foreach (var handler in associatedHandlers)
                {
                    Console.WriteLine($"Manejador asociado ID: {handler}");
                }
            }
            else
            {
                Console.WriteLine($"No hay manejadoras asociadas a la receta con ID: {recipeId}");
            }
        }

        private static void CreateRecipe(RecipeRepository recipe_repo)
        {
            Console.Write("Ingrese el nombre de la receta: ");
            string name = Console.ReadLine();

            double referenceTemperature;
            double referenceHumidity;

            do
            {
                Console.Write("Ingrese la temperatura de referencia: ");
            } while (!double.TryParse(Console.ReadLine(), out referenceTemperature));

            do
            {
                Console.Write("Ingrese la humedad de referencia: ");
            } while (!double.TryParse(Console.ReadLine(), out referenceHumidity));

            DateTime startDate;
            DateTime endDate;

            do
            {
                Console.Write("Ingrese la fecha de inicio (Año-Mes-Dia): ");
            } while (!DateTime.TryParse(Console.ReadLine(), out startDate));

            do
            {
                Console.Write("Ingrese la fecha final (Año-Mes-Dia): ");
            } while (!DateTime.TryParse(Console.ReadLine(), out endDate));

            var recipe = new Recipe(
                name,
                referenceTemperature,
                referenceHumidity,
                startDate,
                endDate
            );

            recipe_repo.AddRecipe(recipe);
            Console.Write($"Receta creada con ID:{recipe.Id}");
        }

        private static void ListRecipes(RecipeRepository recipe_repo)
        {
            var allrecipes = recipe_repo.GetAllRecipes<Recipe>().ToList();

            foreach (var recipe in allrecipes)
            {
                Console.Write($"ID:{recipe.Id}, Nombre:{recipe.Name}, Temperatura Referencia:{recipe.ReferenceTemperature}, Humedad Referencia:{recipe.ReferenceHumidity}");
            }
        }
        private static void UpdateRecipe(RecipeRepository recipe_repo)
        {
            Console.Write("Ingrese el ID de la receta a actualizar: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                var recipe = recipe_repo.GetRecipeById<Recipe>(id);
                if (recipe != null)
                {
                    Console.WriteLine($"Receta encontrada: {recipe.Name}");

                    Console.Write("Ingrese el nuevo nombre de la receta (deje vacío para no cambiar): ");
                    var newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        recipe.Name = newName;
                    }

                    Console.Write("Ingrese la nueva temperatura de referencia (deje vacío para no cambiar): ");
                    var tempInput = Console.ReadLine();
                    if (double.TryParse(tempInput, out var newTemperature))
                    {
                        recipe.ReferenceTemperature = newTemperature;
                    }

                    Console.Write("Ingrese la nueva humedad de referencia (deje vacío para no cambiar): ");
                    var humidityInput = Console.ReadLine();
                    if (double.TryParse(humidityInput, out var newHumidity))
                    {
                        recipe.ReferenceHumidity = newHumidity;
                    }

                    Console.Write("Ingrese la nueva fecha de inicio (yyyy-MM-dd, deje vacío para no cambiar): ");
                    var startDateInput = Console.ReadLine();
                    if (DateTime.TryParse(startDateInput, out var newStartDate))
                    {
                        recipe.StartDate = newStartDate;
                    }

                    Console.Write("Ingrese la nueva fecha de fin (yyyy-MM-dd, deje vacío para no cambiar): ");
                    var endDateInput = Console.ReadLine();
                    if (DateTime.TryParse(endDateInput, out var newEndDate))
                    {
                        recipe.EndDate = newEndDate;
                    }

                    recipe_repo.UpdateRecipe(recipe);
                    Console.WriteLine($"Receta actualizada con éxito. ID: {recipe.Id}");
                }
                else
                {
                    Console.WriteLine("Receta no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID no válido.");
            }
        }

        private static void DeleteRecipe(RecipeRepository recipe_repo)
        {
            Console.Write("Ingrese el ID de la receta a eliminar: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                var recipe = recipe_repo.GetRecipeById<Recipe>(id);
                if (recipe != null)
                {
                    recipe_repo.DeleteRecipe(recipe);
                    Console.WriteLine($"Receta con ID {id} eliminada con éxito.");
                }
                else
                {
                    Console.WriteLine("Receta no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID no válido.");
            }
        }

        // ==========================
        // Operaciones: Rooms
        // ==========================
        private static void ManageRooms(RoomRepository roomrepo, AirHandlerRepository aiHanlderRepositoy)
        {
            Console.WriteLine("\n=== Gestión de Habitaciones ===");
            Console.WriteLine("1 - Crear Habitación");
            Console.WriteLine("2 - Listar Habitaciones");
            Console.WriteLine("3 - Actualizar Habitación");
            Console.WriteLine("4 - Eliminar Habitación");
            Console.WriteLine("5 - Seleccionar Habitación para ver su Manejadora asociada");
            Console.WriteLine("0 - Volver al menú principal");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    CreateRoom(roomrepo);
                    break;
                case "2":
                    ListRooms(roomrepo);
                    break;
                case "3":
                    UpdateRoom(roomrepo);
                    break;
                case "4":
                    DeleteRoom(roomrepo);
                    break;
                case "5":
                    ShowAssociatedAirHandlersForRoom(roomrepo);
                    break;
                case "0":
                    return; // Volver al menú principal
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        private static void CreateRoom(RoomRepository roomrepo)
        {
            int number;
            double volume;

            do
            {
                Console.Write("Ingrese el número de la habitación: ");
            } while (!int.TryParse(Console.ReadLine(), out number));

            do
            {
                Console.Write("Ingrese el volumen de la habitación en m³: ");
            } while (!double.TryParse(Console.ReadLine(), out volume));

            var room = new Room(number, volume);
            roomrepo.AddRoom(room);
            Console.WriteLine($"Habitación creada con ID: {room.Id}");
        }

        private static void ListRooms(RoomRepository roomrepo)
        {
            var allRooms = roomrepo.GetAllRooms<Room>().ToList();
            foreach (var room in allRooms)
            {
                Console.WriteLine($"ID: {room.Id}, Número: {room.Number}, Volumen: {room.Volume}");
            }
        }

        private static void UpdateRoom(RoomRepository roomrepo)
        {
            Console.Write("Ingrese el ID de la habitación a actualizar: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                var room = roomrepo.GetRoomById<Room>(id);
                if (room != null)
                {
                    Console.WriteLine($"Habitación encontrada: Número {room.Number}, Volumen {room.Volume}");

                    Console.Write("Ingrese el nuevo número de la habitación (deje vacío para no cambiar): ");
                    var newNumberInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newNumberInput) && int.TryParse(newNumberInput, out var newNumber))
                    {
                        room.Number = newNumber;
                    }

                    Console.Write("Ingrese el nuevo volumen de la habitación en m³ (deje vacío para no cambiar): ");
                    var newVolumeInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newVolumeInput) && double.TryParse(newVolumeInput, out var newVolume))
                    {
                        room.Volume = newVolume;
                    }

                    roomrepo.UpdateRoom(room);
                    Console.WriteLine($"Habitación actualizada con éxito. ID: {room.Id}");
                }
                else
                {
                    Console.WriteLine("Habitación no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID no válido.");
            }
        }

        private static void DeleteRoom(RoomRepository roomrepo)
        {
            Console.Write("Ingrese el ID de la habitación a eliminar: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                // Busca la habitación por ID
                var room = roomrepo.GetRoomById<Room>(id);
                if (room != null)
                {
                    roomrepo.DeleteRoom(room); // Elimina la habitación existente
                    Console.WriteLine($"Habitación con ID {id} eliminada.");
                }
                else
                {
                    Console.WriteLine("Habitación no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID no válido.");
            }
        }

        private static void ShowAssociatedAirHandlersForRoom(RoomRepository roomrepo)
        {
            Console.Write("Ingrese el ID de la habitación para ver su Manejadora asociada: ");
            if (Guid.TryParse(Console.ReadLine(), out var roomId))
            {
                var room = roomrepo.GetRoomById<Room>(roomId);
                if (room != null)
                {
                    if (room.AssociatedHandlerId != Guid.Empty)
                    {
                        // Aquí se podría realizar una consulta adicional para obtener detalles del AirHandler.
                        // Por simplicidad, solo mostramos el ID.
                        Console.WriteLine($"La Manejadora de Aire asociada a esta habitación es ID: {room.AssociatedHandlerId}");
                    }
                    else
                    {
                        Console.WriteLine("Esta habitación no tiene una Manejadora de Aire asociada.");
                    }
                }
                else
                {
                    Console.WriteLine("Habitación no encontrada.");
                }
            }
            else
            {
                Console.WriteLine("ID no válido.");
            }
        }
    }

}
