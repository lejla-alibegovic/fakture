using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace faktura.Web.ViewModels
{
    public class UpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string BrojFakture { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DatumKreiranja { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd.MM.yyyy")]
        public DateTime? DatumDospijeca { get; set; }

        public double UkupnaCijenaBezPDV { get; set; }

        public double UkupnaCijenaSaPDV { get; set; }

        public string PrimateljFakture { get; set; }

        public List<Data.Dtos.Requests.StavkeFakture> StavkeFakture { get; set; }

        public List<SelectListItem> PDV { get; set; }
        public string OdabraniPDV { get; set; }
    }
}