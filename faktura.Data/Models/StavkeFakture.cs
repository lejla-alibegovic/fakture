using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace faktura.Data.Models
{
    public class StavkeFakture:Base<int>
    {
        [ForeignKey(nameof(Faktura))]
        public int FakturaId { get; set; }
        public Faktura Faktura { get; set; }

        [Required]
        public string Opis { get; set; }

        [Required]
        public int Kolicina { get; set; }

        [Required]
        public double JedininaCijena { get; set; }

        public double UkupnaCijenaBezPDV { get; set; }
    }
}