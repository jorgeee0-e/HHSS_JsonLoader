using jsonReader_Angular.Server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace jsonReader_Angular.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class ResumenController : ControllerBase
    {

        private readonly string cadenaSQL;
        public ResumenController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("cadenaSQL");
        }

        [HttpGet]
        [Route("ListaResumen")]

        public IActionResult ListaResumen()
        {
            List<Resumen> lista = new List<Resumen>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("SP_Resument_DTEs", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Resumen()
                            {
                                Id_resumen = Convert.ToInt32(rd["id_resumen"]),
                                CodigoGeneracion = rd["codigoGeneracion"].ToString(),
                                TotalNoSuj = Convert.ToDecimal(rd["totalNoSuj"]),
                                TotalExenta = Convert.ToDecimal(rd["totalExenta"]),
                                TotalGravada = Convert.ToDecimal(rd["totalGravada"]),
                                SubTotalVentas = Convert.ToDecimal(rd["subTotalVentas"]),
                                DescuNoSuj = Convert.ToDecimal(rd["descuNoSuj"]),
                                DescuExenta = Convert.ToDecimal(rd["descuExenta"]),
                                PorcentajeDescuento = Convert.ToDecimal(rd["porcentajeDescuento"]),
                                TotalDescu = Convert.ToDecimal(rd["totalDescu"]),
                                IvaPerci1 = Convert.ToDecimal(rd["ivaPerci1"]),
                                IvaRete1 = Convert.ToDecimal(rd["ivaRete1"]),
                                ReteRenta = Convert.ToDecimal(rd["reteRenta"]),
                                MontoTotalOperacion = Convert.ToDecimal(rd["montoTotalOperacion"]),
                                TotalPagar = Convert.ToDecimal(rd["totalPagar"]),
                                TotalLetras = Convert.ToString(rd["totalLetras"]),
                                SaldoFavor = Convert.ToDecimal(rd["saldoFavor"]),

                            });
                        }

                        return StatusCode(StatusCodes.Status200OK, new { mensake = "ok", response = lista });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = lista });
            }

        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Resumen objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("SP_guardar_resumen_DTEs", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("codigoGeneracion", objeto.CodigoGeneracion);
                    cmd.Parameters.AddWithValue("totalNoSuj", objeto.TotalNoSuj);
                    cmd.Parameters.AddWithValue("totalExenta", objeto.TotalExenta);
                    cmd.Parameters.AddWithValue("totalGravada", objeto.TotalGravada);
                    cmd.Parameters.AddWithValue("subTotalVentas", objeto.SubTotalVentas);
                    cmd.Parameters.AddWithValue("descuNoSuj", objeto.DescuNoSuj);
                    cmd.Parameters.AddWithValue("descuExenta", objeto.DescuExenta);
                    cmd.Parameters.AddWithValue("descuGravada", objeto.DescuGravada);
                    cmd.Parameters.AddWithValue("porcentajeDescuento", objeto.PorcentajeDescuento);
                    cmd.Parameters.AddWithValue("totalDescu", objeto.TotalDescu);
                    cmd.Parameters.AddWithValue("ivaPerci1", objeto.IvaPerci1);
                    cmd.Parameters.AddWithValue("ivaRete1", objeto.IvaRete1);
                    cmd.Parameters.AddWithValue("reteRenta", objeto.ReteRenta);
                    cmd.Parameters.AddWithValue("montoTotalOperacion", objeto.MontoTotalOperacion);
                    cmd.Parameters.AddWithValue("totalNoGravado", objeto.TotalNoGravado);
                    cmd.Parameters.AddWithValue("totalPagar", objeto.TotalPagar);
                    cmd.Parameters.AddWithValue("totalLetras", objeto.TotalLetras);
                    cmd.Parameters.AddWithValue("saldoFavor", objeto.SaldoFavor);
                    cmd.ExecuteNonQuery();
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje= "ok" });
            }
            

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}
