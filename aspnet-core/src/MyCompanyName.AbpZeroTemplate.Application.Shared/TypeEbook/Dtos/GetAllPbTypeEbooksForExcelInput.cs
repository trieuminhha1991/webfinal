using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TypeEbook.Dtos
{
    public class GetAllPbTypeEbooksForExcelInput
    {
		public string Filter { get; set; }

		public string TypeNameFilter { get; set; }

		public string DescriptionFilter { get; set; }



    }
}