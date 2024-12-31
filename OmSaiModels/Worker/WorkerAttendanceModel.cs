using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmSaiModels.Worker
{
	public class WorkerAttendanceModel
	{
		[Key]
		public int? WorkerAttendanceId { get; set; }

		[Required]
		public int WorkerId { get; set; }

		public int? VerifyBy { get; set; }

		[Required]
		public int SiteId { get; set; }

		[Required]
		public int ShiftId { get; set; }

		public DateTime? InTime { get; set; }

		public DateTime? OutTime { get; set; }

		[MaxLength(500)]
		public string InSelfiPath { get; set; }

		[MaxLength(500)]
		public string OutSelfiPath { get; set; }

		[MaxLength(50)]
		public string InGeoLocation { get; set; }

		[MaxLength(50)]
		public string OutGeoLocation { get; set; }

		public int LateIn { get; set; } = 0;

		public int EarlyOut { get; set; } = 0;

		[Required]
		[MaxLength(20)]
		[RegularExpression("Absent|Present|Leave|Miss", ErrorMessage = "Invalid Status")]
		public string Status { get; set; }

		public DateTime? CreatedAt { get; set; }

		public bool? IsDeleted { get; set; } = false;

		public DateTime? DeletedAt { get; set; }
	}
}
