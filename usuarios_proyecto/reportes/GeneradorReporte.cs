using System;
using System.IO;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using usuarios_proyecto.conexion;

namespace usuarios_proyecto.reportes
{
    public class GeneradorReporte
    {
        private ConexionBD conexion = new ConexionBD();

        public void GenerarReporte(string rutaRelativa = "reportes/UsuariosExportados.pdf")
        {
            string carpetaDestino = Path.GetDirectoryName(rutaRelativa);
            if (!Directory.Exists(carpetaDestino))
                Directory.CreateDirectory(carpetaDestino);

            using (var conn = conexion.ObtenerConexion())
            {
                string sql = "SELECT * FROM usuarios";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    try
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                            PdfWriter.GetInstance(document, new FileStream(rutaRelativa, FileMode.Create));
                            document.Open();

                            Paragraph titulo = new Paragraph("Lista de Usuarios", new Font(Font.FontFamily.HELVETICA, 16f, Font.BOLD));
                            titulo.Alignment = Element.ALIGN_CENTER;
                            document.Add(titulo);
                            document.Add(new Paragraph("\n"));

                            PdfPTable tabla = new PdfPTable(4);
                            tabla.WidthPercentage = 100;

                            tabla.AddCell("ID");
                            tabla.AddCell("Nombre");
                            tabla.AddCell("Apellido");
                            tabla.AddCell("Correo");

                            while (reader.Read())
                            {
                                tabla.AddCell(reader["id"].ToString());
                                tabla.AddCell(reader["nombre"].ToString());
                                tabla.AddCell(reader["apellido"].ToString());
                                tabla.AddCell(reader["correo"].ToString());
                            }

                            document.Add(tabla);
                            document.Close();

                            Console.WriteLine($"Reporte generado en: {Path.GetFullPath(rutaRelativa)}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al generar reporte: " + ex.Message);
                    }
                }
            }
        }
    }
}
