using LmsServices.Common;
using OmSaiModels.Worker;
using OmSaiServices.Worker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmSaiServices.Worker.Implementations
{
	public class WorkerAttendanceService : IWorkerAttendanceService
	{
		public void ManageAttendance(WorkerAttendanceModel model)
		{

			var parameters =  new List<KeyValuePair<string, object>>
			{
				new("@WorkerId", model.WorkerId),
				new("@SiteId", model.SiteId),
				new("@ShiftId", model.ShiftId),
				new("@InTime", model.InTime),

				new("@InSelfiPath", model.InSelfiPath),
				new("@InGeoLocation", model.InGeoLocation),
				new("@OutTime", model.OutTime),
				new("@OutSelfiPath", model.OutSelfiPath),
				new("@OutGeoLocation", model.@OutGeoLocation)
			};

			QueryService.NonQuery("usp_ManageWorkerAttendance", parameters);
		}
	}
}
