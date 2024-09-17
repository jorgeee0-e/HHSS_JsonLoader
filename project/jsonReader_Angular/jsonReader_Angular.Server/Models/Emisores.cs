using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace jsonReader_Angular.Server.Models
{
    public class Emisores
    {
        [Key]
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
