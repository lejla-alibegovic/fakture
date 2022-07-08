using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faktura.Data.Dtos.Requests
{
    public class Faktura
    {
        public Faktura()
        {
            StavkeFakture = new List<StavkeFakture>();
        }

        public int Id { get; set; }
        [Required]
        public string BrojFakture { get; set; }
        [Required]
        public DateTime? DatumKreiranja { get; set; }
        [Required]
        public DateTime? DatumDospijeca { get; set; }
        public double UkupnaCijenaBezPDV { get; set; }
        public double UkupnaCijenaSaPDV { get; set; }
        public string PrimateljFakture { get; set; }
        public string ApplicationUserId { get; set; }
        public List<StavkeFakture> StavkeFakture { get; set; }
        public string PDV { get; set; }
    }
}
