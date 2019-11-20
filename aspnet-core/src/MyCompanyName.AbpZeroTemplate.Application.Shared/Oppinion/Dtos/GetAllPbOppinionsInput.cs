using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Oppinion.Dtos
{
    public class GetAllPbOppinionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string ContentFilter { get; set; }


		 public string UserNameFilter { get; set; }

		 		 public string PbEbookEbookNameFilter { get; set; }

		 
    }
}