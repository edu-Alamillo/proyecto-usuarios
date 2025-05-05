using System;
using usuario_proyecto.modelo;
using usuario_proyecto.servicio;
using usuarios_proyecto.reportes;

namespace MiAplicacion
{
    class Program
    {
        static void Main()
        {
            UsuarioCRUD crud = new UsuarioCRUD();
            GeneradorReporte reporte = new GeneradorReporte();
            int opcion;

            do
            {
                Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
                Console.WriteLine("1. Crear Usuario");
                Console.WriteLine("2. Mostrar Usuarios");
                Console.WriteLine("3. Actualizar Usuario");
                Console.WriteLine("4. Eliminar Usuario");
                Console.WriteLine("5. Generar Reporte");
                Console.WriteLine("6. Salir");
                Console.Write("Selecciona una opción: ");
                string entrada = Console.ReadLine();

                if (!int.TryParse(entrada, out opcion))
                {
                    Console.WriteLine("Opción inválida.");
                    continue;
                }

                switch (opcion)
                {
                    case 1: crud.CrearUsuario(); break;
                    case 2: crud.mostrarUsuarios(); break;
                    case 3: crud.ActualizarUsuario(); break;
                    case 4: crud.EliminarUsuario(); break;
                    case 5: reporte.GenerarReporte(); break;
                    case 6: Console.WriteLine("Saliendo del sistema..."); break;
                    default: Console.WriteLine("Opción no válida."); break;
                }

            } while (opcion != 6);
        }
    }
}
