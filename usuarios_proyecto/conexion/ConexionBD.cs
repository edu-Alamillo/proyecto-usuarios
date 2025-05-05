using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace usuarios_proyecto.conexion
{
    public class ConexionBD
    {
        private string cadenaConexion = "Server=localhost;Database=usuario;Uid=root;Pwd=alamillo;";

        public MySqlConnection ObtenerConexion()
        {
            MySqlConnection conexion = new MySqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
                Console.WriteLine("Conexión a la base de datos establecida.");
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error de conexión: " + e.Message);
            }
            return conexion;
        }
    }
}
