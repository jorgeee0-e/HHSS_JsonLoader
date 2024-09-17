using API_JSON_FILES.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace API_JSON_FILES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                                TotalLetras = Convert.ToDecimal(rd["totalLetras"]),
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

        public IActionResult Guardar([FromBody] Resumen Objeto)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("SP_guardar_resumen_DTEs", conexion);

                    cmd.Parameters.AddWithValue("codigoGeneracion", Objeto.CodigoGeneracion);
                    cmd.Parameters.AddWithValue("totalNoSuj", Objeto.TotalNoSuj);
                    cmd.Parameters.AddWithValue("totalExenta", Objeto.TotalExenta);
                    cmd.Parameters.AddWithValue("totalGravada", Objeto.TotalGravada);
                    cmd.Parameters.AddWithValue("totalGravada", Objeto.TotalGravada);
                    cmd.Parameters.AddWithValue("subTotalVentas", Objeto.SubTotalVentas);
                    cmd.Parameters.AddWithValue("descuExenta", Objeto.DescuExenta);
                    cmd.Parameters.AddWithValue("descuGravada", Objeto.DescuGravada);
                    cmd.Parameters.AddWithValue("porcentajeDescuento", Objeto.PorcentajeDescuento);
                    cmd.Parameters.AddWithValue("totalDescu", Objeto.TotalDescu);
                    cmd.Parameters.AddWithValue("ivaPerci1", Objeto.IvaPerci1);
                    cmd.Parameters.AddWithValue("ivaRete1", Objeto.IvaRete1);
                    cmd.Parameters.AddWithValue("reteRenta", Objeto.ReteRenta);
                    cmd.Parameters.AddWithValue("montoTotalOperacion", Objeto.MontoTotalOperacion);
                    cmd.Parameters.AddWithValue("totalNoGravado", Objeto.TotalNoGravado);
                    cmd.Parameters.AddWithValue("totalPagar", Objeto.TotalPagar);
                    cmd.Parameters.AddWithValue("totalLetras", Objeto.TotalLetras);
                    cmd.Parameters.AddWithValue("saldoFavor", Objeto.SaldoFavor);
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
