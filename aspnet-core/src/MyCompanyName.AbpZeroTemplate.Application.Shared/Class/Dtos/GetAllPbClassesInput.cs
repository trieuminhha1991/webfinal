using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Class.Dtos
{
    public class GetAllPbClassesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string ClassGroupFilter { get; set; }

		public string ClassNameFilter { get; set; }



    }
}