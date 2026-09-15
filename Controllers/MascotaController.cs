using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PawCare.Models;
using System.Data;

namespace PawCare.Controllers
{
    public class MascotasController : Controller
    {
        private readonly IConfiguration _configuration;

        public MascotasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            List<Mascota> mascotas = new List<Mascota>();

            string connectionString = _configuration.GetConnectionString("PawCareConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spListarMascotas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Mascota mascota = new Mascota
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                NombreMascota = reader["NombreMascota"].ToString(),
                                NombreDueno = reader["NombreDueno"].ToString(),
                                Tipo = reader["Tipo"].ToString(),
                                Edad = Convert.ToInt32(reader["Edad"]),
                                Telefono = reader["Telefono"].ToString(),
                                Observaciones = reader["Observaciones"].ToString()
                            };

                            mascotas.Add(mascota);
                        }
                    }
                }
            }
            ViewBag.Mascotas = mascotas;

            return View();
        }
        [HttpPost]
        public IActionResult Crear(Mascota mascota)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            string connectionString = _configuration.GetConnectionString("PawCareConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spInsertarMascota", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@NombreMascota", SqlDbType.VarChar, 100)
                    {
                        Value = mascota.NombreMascota
                    });

                    command.Parameters.Add(new SqlParameter("@NombreDueno", SqlDbType.VarChar, 100)
                    {
                        Value = mascota.NombreDueno
                    });

                    command.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.VarChar, 20)
                    {
                        Value = mascota.Tipo
                    });

                    command.Parameters.Add(new SqlParameter("@Edad", SqlDbType.Int)
                    {
                        Value = mascota.Edad
                    });

                    command.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar, 9)
                    {
                        Value = mascota.Telefono
                    });

                    command.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.VarChar, 500)
                    {
                        Value = string.IsNullOrEmpty(mascota.Observaciones)
                            ? DBNull.Value
                            : mascota.Observaciones
                    });

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
    }
}