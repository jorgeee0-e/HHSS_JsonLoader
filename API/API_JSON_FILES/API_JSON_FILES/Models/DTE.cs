
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_JSON_FILES.Models
{
    public class DTE
    {
        [Column("codigoGeneracion")]
        [JsonProperty("codigoGeneracion")]
        public string CodigoGeneracion {  get; set; }

        [Column("nit_emisor")]
        [JsonProperty("nit_emisor")]
        public string Nit_emisor { get; set; }

        [Column("nit_receptor")]
        [JsonProperty("nit_receptor")]
        public string Nit_receptor { get; set; }

        [Column("version_")]
        [JsonProperty("version_")]
        public int Version { get; set; }

        [Column("tipoDte")]
        [JsonProperty("tipoDte")]
        public string TipoDte { get; set; }

        [Column("numeroControl")]
        [JsonProperty("numeroControl")]
        public string NumeroControl { get; set; }

        [Column("fecEmi")]
        [JsonProperty("fecEmi")]
        public DateTime FecEmi { get; set; }

        [Column("horaEmi")]
        [JsonProperty("horaEmi")]
        public DateTime HoraEmi { get; set; }
       
    }
}
