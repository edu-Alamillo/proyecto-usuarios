using MySql.Data.MySqlClient;
using usuario_proyecto.modelo;
using usuarios_proyecto.conexion;   

namespace usuario_proyecto.servicio;

public class UsuarioCRUD
{

    private ConexionBD conexion = new ConexionBD();

    public void CrearUsuario()
    {
        Usuario nuevo = new Usuario();

        Console.Write("Nombre: ");
        nuevo.Nombre = Console.ReadLine();

        Console.Write("Apellido: ");
        nuevo.Apellido = Console.ReadLine();

        Console.Write("Correo: ");
        nuevo.Correo = Console.ReadLine();

        Console.Write("Contraseña: ");
        nuevo.Contrasena = Console.ReadLine();

        using (var conn = conexion.ObtenerConexion())
        {
            string sql = "INSERT INTO usuarios (nombre, apellido, correo, contrasena) VALUES (@nombre, @apellido, @correo, @contrasena)";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nombre", nuevo.Nombre);
                cmd.Parameters.AddWithValue("@apellido", nuevo.Apellido);
                cmd.Parameters.AddWithValue("@correo", nuevo.Correo);
                cmd.Parameters.AddWithValue("@contrasena", nuevo.Contrasena);

                try
                {
                    int filas = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Usuario creado exitosamente ({filas} fila/s afectada/s).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar usuario: " + ex.Message);
                }
            }
        }
        Console.WriteLine("Usuario creado exitosamente.");
    }

    public void mostrarUsuarios()
    {
        using (var conn = conexion.ObtenerConexion())
        {
            string sql = "SELECT * FROM usuarios";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("\n--- Lista de Usuarios ---");

                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            string nombre = reader.GetString("nombre");
                            string apellido = reader.GetString("apellido");
                            string correo = reader.GetString("correo");

                            Console.WriteLine($"ID: {id} - {nombre} {apellido} | Correo: {correo}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Error al leer usuarios: " + ex.Message);
                }
            }
        }
    }


    public void ActualizarUsuario()
    {
        Console.Write("Ingrese el ID del usuario a actualizar: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Nuevo Nombre: ");
        string nuevoNombre = Console.ReadLine();

        Console.Write("Nuevo Apellido: ");
        string nuevoApellido = Console.ReadLine();

        Console.Write("Nuevo Correo: ");
        string nuevoCorreo = Console.ReadLine();

        Console.Write("Nueva Contraseña: ");
        string nuevaContrasena = Console.ReadLine();

        using (var conn = conexion.ObtenerConexion())
        {
            string sql = "UPDATE usuarios SET nombre = @nombre, apellido = @apellido, correo = @correo, contrasena = @contrasena WHERE id = @id";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nombre", nuevoNombre);
                cmd.Parameters.AddWithValue("@apellido", nuevoApellido);
                cmd.Parameters.AddWithValue("@correo", nuevoCorreo);
                cmd.Parameters.AddWithValue("@contrasena", nuevaContrasena);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0)
                        Console.WriteLine("✅ Usuario actualizado.");
                    else
                        Console.WriteLine("❌ Usuario no encontrado.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Error al actualizar: " + ex.Message);
                }
            }
        }
    }
    public void EliminarUsuario()
    {
        Console.Write("Ingrese el ID del usuario a eliminar: ");
        int id = int.Parse(Console.ReadLine());

        using (var conn = conexion.ObtenerConexion())
        {
            string sql = "DELETE FROM usuarios WHERE id = @id";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0)
                        Console.WriteLine("Usuario eliminado.");
                    else
                        Console.WriteLine("Usuario no encontrado.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar: " + ex.Message);
                }
            }
        }
    }
}

