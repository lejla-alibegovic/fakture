using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faktura.Data.Dtos.Requests
{
    public class StavkeFakture
    {
        public int? Id { get; set; }

        public int FakturaId { get; set; }

        [Required]
        public string Opis { get; set; }

        [Required]
        public int Kolicina { get; set; }

        [Required]
        public double JedininaCijena { get; set; }

        public double UkupnaCijenaBezPDV => Kolicina * JedininaCijena;
    }
}
