using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.TypeFile.Dtos
{
    public class GetAllPbTypeFilesForExcelInput
    {
		public string Filter { get; set; }

		public string TypeFileNameFilter { get; set; }

		public string DescriptionFilter { get; set; }



    }
}