using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faktura.Data.Dtos.Responses
{
    public class Faktura
    {
        public int Id { get; set; } 
        public string BrojFakture { get; set; }  
        public DateTime? DatumKreiranja { get; set; }
        public DateTime? DatumDospijeca { get; set; }
        public double UkupnaCijenaBezPDV { get; set; }
        public double UkupnaCijenaSaPDV { get; set; }
        public string PrimateljFakture { get; set; }
        public List<StavkeFakture> StavkeFakture { get; set; }
      
    }
}
