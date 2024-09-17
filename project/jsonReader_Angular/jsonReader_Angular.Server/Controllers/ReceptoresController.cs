using jsonReader_Angular.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Cors;


namespace jsonReader_Angular.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class ReceptoresController : ControllerBase
    {
        private readonly string cadenaSQL;

        public ReceptoresController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Check")]

        public IActionResult Check()
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "Conexion existosa" });
                }

            }
            catch (Exception ex) { 
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            
            }

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
                                Nit = rd["numDocumento"].ToString(),
                                Nombre = rd["nombre"].ToString(),
                                CodActividad = rd["codActividad"].ToString(),
                                Departamento = rd["departamento"].ToString(),
                                Municipio = rd["municipio"].ToString(),
                                Complemento = rd["complemento"].ToString(),
                                Correo = rd["correo"].ToString(),
                                TipoDocumento = rd["tipoDocumento"].ToString(),

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
                    var cmd = new SqlCommand("SP_guardar_Receptores", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@numDocumento", Objeto.Nit);
                    cmd.Parameters.AddWithValue("@nombre", Objeto.Nombre);
                    cmd.Parameters.AddWithValue("@codActividad", Objeto.CodActividad);
                    cmd.Parameters.AddWithValue("@departamento", Objeto.Departamento);
                    cmd.Parameters.AddWithValue("@municipio", Objeto.Municipio);
                    cmd.Parameters.AddWithValue("@complemento", Objeto.Complemento);
                    cmd.Parameters.AddWithValue("@correo", Objeto.Correo);
                    cmd.Parameters.AddWithValue("@tipoDocumento", Objeto.TipoDocumento);
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
