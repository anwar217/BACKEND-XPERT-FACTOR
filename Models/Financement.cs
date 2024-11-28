using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace factoring1.Models
{
    [JsonConverter(typeof(TypeDeFinancementConverter))] // Utilisation du convertisseur personnalisé
    public enum TypeDeFinancement
    {
        Financement,
        LiberationFDG
    }
  
    [JsonConverter(typeof(StatutFinancementConverter))] // Utilisation du convertisseur personnalisé
    public enum StatutFinancement
    {
        Approved, // Correction du nom
        Rejected,
        Pending
    }

    public class Financement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FinancementId { get; set; }

        [Required]
        public TypeDeFinancement TypeDeFinancement { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MontantFinancement { get; set; }

        [Required]
        public DateTime DateDeFinancement { get; set; }

       
        [Required]
        public StatutFinancement StatutFinancement { get; set; }=StatutFinancement.Pending;

        [Required]
        [StringLength(100)]
        public string MethodeDePaiement { get; set; }

        public int ContratId { get; set; }
        public Contrat? Contrat { get; set; }
    }

    public class TypeDeFinancementConverter : JsonConverter<TypeDeFinancement>
    {
        public override TypeDeFinancement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return value switch
            {
                "Financement" => TypeDeFinancement.Financement,
                "LiberationFDG" => TypeDeFinancement.LiberationFDG,
                _ => throw new JsonException($"Unable to convert \"{value}\" to {nameof(TypeDeFinancement)}")
            };
        }

        public override void Write(Utf8JsonWriter writer, TypeDeFinancement value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

   
    public class StatutFinancementConverter : JsonConverter<StatutFinancement>
    {
        public override StatutFinancement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return value switch
            {
                "Approved" => StatutFinancement.Approved, // Correction du nom
                "Rejected" => StatutFinancement.Rejected,
                "Pending" => StatutFinancement.Pending,
                _ => throw new JsonException($"Unable to convert \"{value}\" to {nameof(StatutFinancement)}")
            };
        }

        public override void Write(Utf8JsonWriter writer, StatutFinancement value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
