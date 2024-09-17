using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using jsonReader_Angular.Server.Models;
using System.Data;
using System.Data.SqlClient;
using System;
using Microsoft.AspNetCore.Cors;


namespace jsonReader_Angular.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class EmisorController : ControllerBase
    {
        private readonly string cadenaSQL;

        public EmisorController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("cadenaSQL");
        }

        [HttpGet]
        [Route("ListaEmisores")]

        public IActionResult ListaEmisores()
        {
            List<Emisores> lista = new List<Emisores>();
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("SP_Emisores", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Emisores()
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

        public IActionResult Guardar([FromBody] Emisores Objeto)
        {
            
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("SP_guardar_Emisores", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message});
            }

        }
    }
}
