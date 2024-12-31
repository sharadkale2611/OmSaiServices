using Microsoft.AspNetCore.Mvc;
using OmSaiModels.Worker;
using OmSaiServices.Admin.Implementations;
using OmSaiServices.Worker.Implementations;
using OmSaiServices.Worker.Implimentation;

namespace GeneralTemplate.Areas.Worker.Controllers
{
	[Area("Worker")]
	public class AttendanceController : Controller
	{
		private readonly WorkerService _workerService;
		private readonly WorkerAttendanceService _attendanceService;

		public AttendanceController()
		{
			_workerService = new WorkerService();
			_attendanceService = new WorkerAttendanceService();
		}

		public IActionResult Index(string wokrman)
		{

			var worker = _workerService.GetProfileById(null, wokrman);

			if (worker == null ||  wokrman == null)
			{
				return RedirectToAction(nameof(Error));
			}

			ViewBag.Worker = worker;

			return View();
		}



		[HttpPost]
		[Route("api/WorkerAttendance")]
		public async Task<IActionResult> WorkerAttendance([FromForm] WorkerAttendanceModel model, IFormFile selfieImage)
		{
			try
			{
				// Validate the model
				if (!ModelState.IsValid)
				{
					return Json(new
					{
						success = false,
						message = "Invalid data provided.",
						errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
					});
				}

				// Check if an image is provided
				if (selfieImage == null || selfieImage.Length == 0)
				{
					return Json(new
					{
						success = false,
						message = "Selfie image is required."
					});
				}

				// Save the selfie image
				var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "selfies");
				if (!Directory.Exists(uploadsFolder))
				{
					Directory.CreateDirectory(uploadsFolder);
				}

				var fileName = $"{Guid.NewGuid()}_{selfieImage.FileName}";
				var filePath = Path.Combine(uploadsFolder, fileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await selfieImage.CopyToAsync(stream);
				}

				// Set the selfie path in the model
				model.InSelfiPath = $"/selfies/{fileName}";

				// Call the service to manage attendance
				_attendanceService.ManageAttendance(model);

				// Return a success response
				return Json(new
				{
					success = true,
					message = "Attendance processed successfully.",
				});
			}
			catch (Exception ex)
			{
				// Log the exception if necessary

				// Return a failure response
				return Json(new
				{
					success = false,
					message = "An error occurred while processing attendance.",
					error = ex.Message
				});
			}
		}





		public IActionResult Error(string id)
		{
			ViewBag.WorkerId = id;

			return View();
		}


	}
	}
