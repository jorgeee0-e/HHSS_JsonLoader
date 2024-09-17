using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.Xml;

namespace API_JSON_FILES.Models
{
    public class Resumen
    {
        [Column("id_resumen")]
        [JsonProperty("id_resumen")]
        public int Id_resumen { get; set; }

        [Column("codigoGeneracion")]
        [JsonProperty("codigoGeneracion")]
        public string CodigoGeneracion { get; set; }

        [Column("totalNoSuj")]
        [JsonProperty("totalNoSuj")]
        public decimal TotalNoSuj { get; set; }

        [Column("totalExenta")]
        [JsonProperty("totalExenta")]
        public decimal TotalExenta { get; set; }

        [Column("totalGravada")]
        [JsonProperty("totalGravada")]
        public decimal TotalGravada { get; set; }

        [Column("subTotalVentas")]
        [JsonProperty("subTotalVentas")]
        public decimal SubTotalVentas { get; set; }

        [Column("descuNoSuj")]
        [JsonProperty("descuNoSuj")]
        public decimal DescuNoSuj { get; set; }

        [Column("descuExenta")]
        [JsonProperty("descuExenta")]
        public decimal DescuExenta { get; set; }

        [Column("descuGravada")]
        [JsonProperty("descuGravada")]
        public decimal DescuGravada { get; set; }

        [Column("porcentajeDescuento")]
        [JsonProperty("porcentajeDescuento")]
        public decimal PorcentajeDescuento { get; set; }

        [Column("totalDescu")]
        [JsonProperty("totalDescu")]
        public decimal TotalDescu { get; set; }

        [Column("ivaPerci1")]
        [JsonProperty("ivaPerci1")]
        public decimal IvaPerci1 { get; set; }

        [Column("ivaRete1")]
        [JsonProperty("ivaRete1")]
        public decimal IvaRete1 { get; set; }

        [Column("reteRenta")]
        [JsonProperty("reteRenta")]
        public decimal ReteRenta { get; set; }

        [Column("montoTotalOperacion")]
        [JsonProperty("montoTotalOperacion")]
        public decimal MontoTotalOperacion { get; set; }

        [Column("totalNoGravado")]
        [JsonProperty("totalNoGravado")]
        public decimal TotalNoGravado { get; set; }

        [Column("totalPagar")]
        [JsonProperty("totalPagar")]
        public decimal TotalPagar { get; set; }

        [Column("totalLetras")]
        [JsonProperty("totalLetras")]
        public decimal TotalLetras { get; set; }

        [Column("saldoFavor")]
        [JsonProperty("saldoFavor")]
        public decimal SaldoFavor { get; set; }
    }
}
