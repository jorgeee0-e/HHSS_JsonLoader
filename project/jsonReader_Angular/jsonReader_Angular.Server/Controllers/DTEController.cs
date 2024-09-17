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
                                Resumen = new Resumen()
                                {
                                    Id_resumen = Convert.ToInt32(rd["id_resumen"]),
                                    CodigoGeneracion = rd["codigoGeneracion"].ToString(),
                                    TotalNoSuj = Convert.ToDecimal(rd["totalNoSuj"]),
                                    TotalExenta = Convert.ToDecimal(rd["totalExenta"]),
                                    TotalGravada = Convert.ToDecimal(rd["totalGravada"]),
                                    SubTotalVentas = Convert.ToDecimal(rd["subTotalVentas"]),
                                    DescuNoSuj = Convert.ToDecimal(rd["descuNoSuj"]),
                                    DescuExenta = Convert.ToDecimal(rd["descuExenta"]),
                                    DescuGravada = Convert.ToDecimal(rd["descuGravada"]),
                                    PorcentajeDescuento = Convert.ToDecimal(rd["porcentajeDescuento"]),
                                    TotalDescu = Convert.ToDecimal(rd["totalDescu"]),
                                    IvaPerci1 = Convert.ToDecimal(rd["ivaPerci1"]),
                                    IvaRete1 = Convert.ToDecimal(rd["ivaRete1"]),
                                    ReteRenta = Convert.ToDecimal(rd["reteRenta"]),
                                    MontoTotalOperacion = Convert.ToDecimal(rd["montoTotalOperacion"]),
                                    TotalNoGravado = Convert.ToDecimal(rd["totalNoGravado"]),
                                    TotalPagar = Convert.ToDecimal(rd["totalPagar"]),
                                    TotalLetras = rd["totalLetras"].ToString(),
                                    SaldoFavor = Convert.ToDecimal(rd["saldoFavor"])
                                }

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
                    var cmd = new SqlCommand("SP_guardar_DTE_y_Resumen", conexion); //para ejecutar el procedimiento almacenado que se creo en la base de datos, recibe parametros el nombre del sp y la conexion a la bd
                    cmd.Parameters.AddWithValue("codigoGeneracion", objeto.CodigoGeneracion);
                    cmd.Parameters.AddWithValue("nit_emisor", objeto.Nit_emisor);
                    cmd.Parameters.AddWithValue("nit_receptor", objeto.Nit_receptor);
                    cmd.Parameters.AddWithValue("version_", objeto.Version);
                    cmd.Parameters.AddWithValue("tipoDte", objeto.TipoDte);
                    cmd.Parameters.AddWithValue("numeroControl", objeto.NumeroControl);
                    cmd.Parameters.AddWithValue("fecEmi", objeto.FecEmi);

                    // Parámetros del Resumen
                    cmd.Parameters.AddWithValue("@totalNoSuj", objeto.Resumen.TotalNoSuj);
                    cmd.Parameters.AddWithValue("@totalExenta", objeto.Resumen.TotalExenta);
                    cmd.Parameters.AddWithValue("@totalGravada", objeto.Resumen.TotalGravada);
                    cmd.Parameters.AddWithValue("@subTotalVentas", objeto.Resumen.SubTotalVentas);
                    cmd.Parameters.AddWithValue("@descuNoSuj", objeto.Resumen.DescuNoSuj);
                    cmd.Parameters.AddWithValue("@descuExenta", objeto.Resumen.DescuExenta);
                    cmd.Parameters.AddWithValue("@descuGravada", objeto.Resumen.DescuGravada);
                    cmd.Parameters.AddWithValue("@porcentajeDescuento", objeto.Resumen.PorcentajeDescuento);
                    cmd.Parameters.AddWithValue("@totalDescu", objeto.Resumen.TotalDescu);
                    cmd.Parameters.AddWithValue("@ivaPerci1", objeto.Resumen.IvaPerci1);
                    cmd.Parameters.AddWithValue("@ivaRete1", objeto.Resumen.IvaRete1);
                    cmd.Parameters.AddWithValue("@reteRenta", objeto.Resumen.ReteRenta);
                    cmd.Parameters.AddWithValue("@montoTotalOperacion", objeto.Resumen.MontoTotalOperacion);
                    cmd.Parameters.AddWithValue("@totalNoGravado", objeto.Resumen.TotalNoGravado);
                    cmd.Parameters.AddWithValue("@totalPagar", objeto.Resumen.TotalPagar);
                    cmd.Parameters.AddWithValue("@totalLetras", objeto.Resumen.TotalLetras);
                    cmd.Parameters.AddWithValue("@saldoFavor", objeto.Resumen.SaldoFavor);

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
