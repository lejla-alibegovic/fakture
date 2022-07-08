using faktura.Data.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faktura.Data.Models
{
    public class Faktura : Base<int>
    {
        [Required]
        public string BrojFakture { get; set; }
        [Required]
        public DateTime? DatumKreiranja { get; set; }
        [Required]
        public DateTime? DatumDospijeca { get; set; }
        public double UkupnaCijenaBezPDV { get; set; }
        public double UkupnaCijenaSaPDV { get; set; }
        public string PrimateljFakture { get; set; }
        public ICollection<StavkeFakture> StavkeFakture { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
