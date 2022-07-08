using faktura.Data.Dtos.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace faktura.Web.ViewModels
{
    public class CreateViewModel
    {
        public CreateViewModel()
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
        public List<SelectListItem> PDV { get; set; }
        public string OdabraniPDV { get; set; }
    }
}