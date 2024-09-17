using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_JSON_FILES.Models;
using System.Data;
using System.Data.SqlClient;
using System;

namespace API_JSON_FILES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DTEController : ControllerBase
    {
        private readonly string cadenaSQL; //para poder llamar la conexion a bd

        public DTEController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL"); //cadena de conexion a bd
        }

        [HttpGet]
        [Route("ListaDTE")]
        public IActionResult ListaDTE()
        {
            List<DTE> lista = new List<DTE>(); //crear lista para almacenar los DTE
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL)) //se crea una instancia de la clase SqlConnection por medio de la variable cadenaSQL, se utiliza using para que la conexion se cierre una vez haya terminado su uso 
                {
                    conexion.Open(); //abrir la conexion
                    var cmd = new SqlCommand("SP_DTEs", conexion); //para ejecutar el procedimiento almacenado que se creo en la base de datos, recibe parametros el nombre del sp y la conexion a la bd
                    cmd.CommandType = CommandType.StoredProcedure; //especifica que lo que se va a ejecutar es un sp y no una consulta de SQL

                    using (var rd = cmd.ExecuteReader()) //leer el resultado de lo ejecutado en el stored procedure
                    {
                        while (rd.Read())
                        {
                            lista.Add(new DTE()
                            {
                                CodigoGeneracion = rd["codigoGeneracion"].ToString(),
                                Nit_emisor = rd["nit_emisor"].ToString(),
                                Nit_receptor = rd["nit_receptor"].ToString(),
                                Version = Convert.ToInt32(rd["version_"]),
                                TipoDte = rd["tipoDte"].ToString(),
                                NumeroControl = rd["numeroControl"].ToString(),
                                FecEmi = Convert.ToDateTime(rd["fecEmi"].ToString()),
                                HoraEmi = Convert.ToDateTime(rd["horaEmi"].ToString())
                            }); //agregar cada resultado a la lista de DTEs
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
            }


        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] DTE objeto)
        {
            
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL)) //se crea una instancia de la clase SqlConnection por medio de la variable cadenaSQL, se utiliza using para que la conexion se cierre una vez haya terminado su uso 
                {
                    conexion.Open(); //abrir la conexion
                    var cmd = new SqlCommand("SP_guardar_DTEs", conexion); //para ejecutar el procedimiento almacenado que se creo en la base de datos, recibe parametros el nombre del sp y la conexion a la bd
                    cmd.Parameters.AddWithValue("codigoGeneracion", objeto.CodigoGeneracion);
                    cmd.Parameters.AddWithValue("nit_emisor", objeto.Nit_emisor);
                    cmd.Parameters.AddWithValue("nit_receptor", objeto.Nit_receptor);
                    cmd.Parameters.AddWithValue("version_", objeto.Version);
                    cmd.Parameters.AddWithValue("tipoDte", objeto.TipoDte);
                    cmd.Parameters.AddWithValue("numeroControl", objeto.NumeroControl);
                    cmd.Parameters.AddWithValue("fecEmi", objeto.FecEmi);
                    cmd.Parameters.AddWithValue("horaEmi", objeto.HoraEmi);
                    cmd.CommandType = CommandType.StoredProcedure; //especifica que lo que se va a ejecutar es un sp y no una consulta de SQL
                    cmd.ExecuteNonQuery();

                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok"});
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message});
            }


        }
    }
}
