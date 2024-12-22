using GeneralTemplate.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmSaiModels.Admin;
using OmSaiServices.Admin.Interfaces;
using OmSaiServices.Admin.Implementations;



namespace GeneralTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DocumentController : Controller
    {
     
        private readonly DocumentService _documentService;
        public DocumentController() 
        {
            _documentService = new DocumentService();
        
        }
        public IActionResult Index()
        {
            ViewBag.documents = _documentService.GetAll();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DocumentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["success"] = "Record added successfully!";
                    _documentService.Create(model);
                }
                else
                {
                    var errorMessages = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errorMessages.Add(error.ErrorMessage);
                        }
                    }
                    TempData["errors"] = errorMessages;
                }

                return RedirectToAction(nameof(Index));             

            }
            catch
            {
                TempData["error"] = "Something went wrong!";
                return View("Index", model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DocumentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["success"] = "Record updated successfully!";
                    _documentService.Update(model);
                }
                else
                {
                    var errorMessages = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errorMessages.Add(error.ErrorMessage);
                        }
                    }
                    TempData["errors"] = errorMessages;
                }

                return RedirectToAction(nameof(Index));            
            }
            catch
            {
                TempData["error"] = "Something went wrong!";
                return View("Index", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["error"] = "Invalid ID. Please try again.";
                return RedirectToAction(nameof(Index));
            }

            _documentService.Delete(id);
            TempData["success"] = "Record deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

    }
    
}
