using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jsonReader_Angular.Server.Models
{
    public class Receptores
    {
        [Key]
        [Column("numDocumento")]
        [JsonProperty("numDocumento")]
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

        [Column("tipoDocumento")]
        [JsonProperty("tipoDocumento")]
        public string TipoDocumento { get; set; }

        public DTE DTEs { get; set; } 
    }
}
