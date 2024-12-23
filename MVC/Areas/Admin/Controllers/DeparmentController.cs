using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmSaiModels.Admin;
using OmSaiServices.Admin.Implementations;

namespace GeneralTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DeparmentController : Controller
    {
        private readonly DepartmentService _departmentService;
        public DeparmentController()
        {
            _departmentService = new DepartmentService();

        }

        //public IActionResult Index()
        //{

        //    return View(_departmentService.GetAll());
        //}


        public ActionResult Index()
        {
            var allData = _departmentService.GetAll();

            return View(allData);
        }

        public IActionResult Create()
        {
            DepartmentModel s = new DepartmentModel();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["success"] = "Record added successfully!";
                    _departmentService.Create(model);
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

                return RedirectToAction(nameof(Index));// nameof checks method compiletime to avoid errors
                                                       //return RedirectToAction(nameof(Index), model);    // If we pass data, it will append to url as a query string

            }
            catch
            {
                TempData["error"] = "Something went wrong!";
                return View("Index", model);
            }
        }



       
        //[HttpPost]
        //public IActionResult Create(DepartmentModel d)
        //{
        //    DepartmentModel s = new DepartmentModel();
        //    _departmentService.Create(d);

        //    return RedirectToAction("Index");
        //}

        public IActionResult Edit(int id)
        {
            DepartmentModel s =_departmentService.GetById(id);
            return View(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["success"] = "Record updated successfully!";
                    _departmentService.Update(model);
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

                return RedirectToAction(nameof(Index));             // nameof checks method compiletime to avoid errors
            }
            catch
            {
                TempData["error"] = "Something went wrong!";
                return View("Index", model);
            }
        }



        //[HttpPost]
        //public IActionResult Edit(DepartmentModel d)
        //{ 
        //    _departmentService.Update(d);
        //    return RedirectToAction("Index");
        //}


        public IActionResult Delete(int id)
        {
            _departmentService.Delete(id);
            return RedirectToAction("Index");
        }





    }
}
