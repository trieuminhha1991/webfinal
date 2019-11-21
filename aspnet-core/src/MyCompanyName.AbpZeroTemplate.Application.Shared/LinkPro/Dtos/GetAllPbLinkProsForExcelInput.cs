using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.LinkPro.Dtos
{
    public class GetAllPbLinkProsForExcelInput
    {
		public string Filter { get; set; }

		public string LinkNameFilter { get; set; }


		 public string PbEbookEbookNameFilter { get; set; }

		 
    }
}