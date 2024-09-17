using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_JSON_FILES.Models
{
    public class Receptores
    {
        [Column("nit")]
        [JsonProperty("nit")]
        public string Nit { get; set; }

        [Column("nombre")]
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [Column("codActividad")]
        [JsonProperty("codActividad")]
        public string CodActividad { get; set; }

        [Column("departamento")]
        [JsonProperty("departamento")]
        public string Departamento { get; set; }

        [Column("municipio")]
        [JsonProperty("municipio")]
        public string Municipio { get; set; }

        [Column("complemento")]
        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [Column("correo")]
        [JsonProperty("correo")]
        public string Correo { get; set; }
    }
}
