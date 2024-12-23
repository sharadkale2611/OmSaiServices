using Microsoft.AspNetCore.Mvc;
using OmSaiModels.Admin;
using OmSaiServices.Admin.Implementations;
using OmSaiServices.Admin.Interfaces;

namespace GeneralTemplate.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class QualificationController : Controller
    {
       
            private readonly QualificationService _qualificationService;
            public QualificationController()
            {

			_qualificationService = new QualificationService();

            }

            //public IActionResult Index()
            //{

            //    return View(_departmentService.GetAll());
            //}


            public ActionResult Index()
            {
                var allData = _qualificationService.GetAll();

                ViewBag.AllData = allData;
                
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(QualificationModel model)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        TempData["success"] = "Record added successfully!";
                        _qualificationService.Create(model);
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
                QualificationModel s = _qualificationService.GetById(id);
                return View(s);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(QualificationModel model)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        TempData["success"] = "Record updated successfully!";
                        _qualificationService.Update(model);
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
                _qualificationService.Delete(id);
                return RedirectToAction("Index");
            }



        }

    }

