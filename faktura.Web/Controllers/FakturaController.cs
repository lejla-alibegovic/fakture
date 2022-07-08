using faktura.PDV;
using faktura.Services.IServices;
using faktura.Web.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace faktura.Web.Controllers
{
    [Authorize]
    public class FakturaController : BaseController<Data.Dtos.Requests.Faktura, Data.Dtos.Responses.Faktura, int>
    {
        private readonly IFakturaService _fakturaService;

        [ImportMany]
        public IEnumerable<Lazy<IPDV, IPDVData>> PDV { get; private set; }
        public FakturaController(IFakturaService fakturaService) : base(fakturaService)
        {
            _fakturaService = fakturaService;
        }
        public override async Task<ActionResult> Index()
        {
            var entities = await _fakturaService.GetAllByUserId(User.Identity.GetUserId());
            return View(entities);
        }

        [HttpGet]
        public override async Task<ActionResult> Update(int? id)
        {
            try
            {
                var invoice = await _fakturaService.GetRequestTypeById(id.Value);
                var model = new UpdateViewModel
                {
                    Id = invoice.Id,
                    DatumKreiranja = invoice.DatumKreiranja,
                    DatumDospijeca = invoice.DatumDospijeca,
                    PrimateljFakture = invoice.PrimateljFakture,
                    BrojFakture = invoice.BrojFakture,
                    UkupnaCijenaSaPDV = invoice.UkupnaCijenaSaPDV,
                    UkupnaCijenaBezPDV = invoice.UkupnaCijenaBezPDV,
                    StavkeFakture = invoice.StavkeFakture,
                    PDV = PDV.Select(x => new SelectListItem
                    {
                        Value = x.Metadata.PDV,
                        Text = x.Metadata.PDV
                    }).ToList()
                };

                TempData["Success"] = "Record successfully updated";
                return View(model);
            }
            catch (Exception e)
            {
                TempData["Error"] = "Error has happened.";
                return RedirectToAction(nameof(Index));
            }
        }

        public override async Task<ActionResult> Update(Data.Dtos.Requests.Faktura model, int id)
        {
            try
            {
                model.ApplicationUserId = User.Identity.GetUserId();
                var pdvService = PDV.FirstOrDefault(x => x.Metadata.PDV == model.PDV);
                await _fakturaService.UpdateWithPDV(id, model, pdvService.Value);
                TempData["Success"] = "Record updated successfully";
            }
            catch (Exception e)
            {
                TempData["Error"] = "Error has happened";
            }

            return RedirectToAction("Index");
        }

        public override ActionResult Create()
        {
            var model = new CreateViewModel
            {
                PDV = PDV.Select(x => new SelectListItem
                {
                    Value = x.Metadata.PDV,
                    Text = x.Metadata.PDV
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public override async Task<ActionResult> Create(Data.Dtos.Requests.Faktura model)
        {
            if (!ModelState.IsValid)
            {
                var createViewModel = new CreateViewModel()
                {
                    PDV = PDV.Select(x => new SelectListItem
                    {
                        Value = x.Metadata.PDV,
                        Text = x.Metadata.PDV
                    }).ToList(),
                    DatumKreiranja = model.DatumKreiranja,
                    DatumDospijeca = model.DatumDospijeca,
                    BrojFakture = model.BrojFakture,
                    PrimateljFakture = model.PrimateljFakture,
                    OdabraniPDV = model.PDV,
                    StavkeFakture = model.StavkeFakture
                };
                return View(createViewModel);
            }
            try
            {
                model.ApplicationUserId = (User.Identity.GetUserId());
                var taxService = PDV.FirstOrDefault(x => x.Metadata.PDV == model.PDV);
                await _fakturaService.CreateWithPDV(model, taxService.Value);
                TempData["Success"] = "Record created successfully.";
            }
            catch (Exception e)
            {
                TempData["Error"] = "Error has happened.";

            }
            return RedirectToAction(nameof(Index));
        }
    }
}