using API_JSON_FILES.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace API_JSON_FILES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptoresController : ControllerBase
    {
        private readonly string cadenaSQL;

        public ReceptoresController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("cadenaSQL");
        }

        [HttpGet]
        [Route("ListaReceptores")]

        public IActionResult ListaEmisores()
        {
            List<Receptores> lista = new List<Receptores>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("SP_Receptores", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Receptores()
                            {
                                Nit = rd["nit"].ToString(),
                                Nombre = rd["nombre"].ToString(),
                                CodActividad = rd["codActividad"].ToString(),
                                Departamento = rd["departamento"].ToString(),
                                Municipio = rd["municipio"].ToString(),
                                Complemento = rd["complemento"].ToString(),
                                Correo = rd["correo"].ToString(),


                            });
                        }

                    }

                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = lista });
            }

        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Receptores Objeto)
        {

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("SP_guardar_Emisores", conexion);
                    cmd.Parameters.AddWithValue("nit", Objeto.Nit);
                    cmd.Parameters.AddWithValue("nombre", Objeto.Nombre);
                    cmd.Parameters.AddWithValue("codActividad", Objeto.CodActividad);
                    cmd.Parameters.AddWithValue("departamento", Objeto.Departamento);
                    cmd.Parameters.AddWithValue("municipio", Objeto.Municipio);
                    cmd.Parameters.AddWithValue("complemento", Objeto.Complemento);
                    cmd.Parameters.AddWithValue("correo", Objeto.Correo);
                    cmd.ExecuteNonQuery();


                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }

        }


    }
}
