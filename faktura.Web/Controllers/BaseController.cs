using faktura.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace faktura.Web.Controllers
{
    public abstract class BaseController<TRequest, TResponse, TKey> : Controller where TResponse : class where TRequest : class, new() where TKey : struct
    {
        private readonly IBase<TRequest, TResponse, TKey> _baseService;

        public BaseController(IBase<TRequest, TResponse, TKey> baseService)
        {
            _baseService = baseService;
        }

        public virtual async Task<ActionResult> Index()
        {
            var entities = await _baseService.GetAll();
            return View(entities);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(new TRequest());
        }

        [HttpPost]
        public virtual async Task<ActionResult> Create(TRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _baseService.Create(model);
                TempData["Success"] = "Record created successfully.";
            }
            catch (Exception e)
            {
                TempData["Error"] = "Error has happened.";

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public virtual async Task<ActionResult> Read(TKey id)
        {
            TResponse response = null;
            try
            {
                response = await _baseService.GetById(id);
            }
            catch
            {
                TempData["Error"] = "Error has happened.";
            }
            return View(response);
        }

        [HttpGet]
        public virtual async Task<ActionResult> Update(TKey? id)
        {
            try
            {
                TRequest uResponse = await _baseService.GetRequestTypeById(id.Value);
                return View(uResponse);
            }
            catch
            {
                TempData["Error"] = "Error has happened.";
                return View(nameof(Index));
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult> Update(TRequest model, TKey id)
        {
            var response = _baseService.Update(model, id);
            return RedirectToAction(nameof(Index));
        }

        public virtual ActionResult Delete(TKey id)
        {
            try
            {
                var response = _baseService.Delete(id);
                TempData["Success"] = "Record deleted successfully";
            }
            catch
            {
                TempData["Error"] = "Error has happened.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}