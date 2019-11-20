using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.DownloadEbook.Dtos
{
    public class GetAllPbDownloadEbooksInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public long? MaxNumberFilter { get; set; }
		public long? MinNumberFilter { get; set; }

		public DateTime? MaxMonthFilter { get; set; }
		public DateTime? MinMonthFilter { get; set; }


		 public string PbEbookEbookNameFilter { get; set; }

		 
    }
}