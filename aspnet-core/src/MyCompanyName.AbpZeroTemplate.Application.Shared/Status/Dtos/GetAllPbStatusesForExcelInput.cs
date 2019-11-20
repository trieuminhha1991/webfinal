using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Status.Dtos
{
    public class GetAllPbStatusesForExcelInput
    {
		public string Filter { get; set; }

		public string StatusNameFilter { get; set; }

		public string DescriptionFilter { get; set; }



    }
}