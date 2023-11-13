using System;
using System.Net.Sockets;
using System.Text;

namespace La_Subasta
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Bienvenido a La Subasta");
            Console.WriteLine("------------------------");

            Console.WriteLine("Ingrese su rol:");
            var rol = Console.ReadLine();
            var user = new User(rol);
            var service = new ExampleService();
            var subasta = new Subasta();
            Client client = new Client();

            if (AccessControl.CheckAccess(service, user))
            {
                Console.Clear(); // Limpiar la consola antes de mostrar el menú principal
                string tituloASCII = @"
 ________                          .___                 .___                 __                 __                       
 /  _____/_____    ____ _____     __| _/___________      |   | ____   _______/  |______    _____/  |_  ____   ____  ____  
/   \  ___\__  \  /    \\__  \   / __ |/  _ \_  __ \     |   |/    \ /  ___/\   __\__  \  /    \   __\/    \_/ __ \/  _ \ 
\    \_\  \/ __ \|   |  \/ __ \_/ /_/ (  <_> )  | \/     |   |   |  \\___ \  |  |  / __ \|   |  \  | |   |  \  ___(  <_> )
 \______  (____  /___|  (____  /\____ |\____/|__|        |___|___|  /____  > |__| (____  /___|  /__| |___|  /\___  >____/ 
        \/     \/     \/     \/      \/                           \/     \/            \/     \/          \/     \/   ";

                Console.WriteLine(tituloASCII);
                Console.WriteLine("Menú Principal");
                Console.WriteLine("--------------");
                Console.WriteLine("1. Realizar Subasta");
                Console.WriteLine("2. Salir");
                Console.Write("Seleccione una opción: ");

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Clear(); // Limpiar la consola antes de la subasta
                        Console.WriteLine("Subasta en curso...");
                        subasta.Jugadores();
                        break;
                    
                    case "2":
                        Console.WriteLine("Saliendo del programa. ¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Acceso denegado. Cerrando la aplicación.");
            }

            Console.ReadLine(); // Esperar a que el usuario presione Enter antes de cerrar la consola
        }
    }
}
