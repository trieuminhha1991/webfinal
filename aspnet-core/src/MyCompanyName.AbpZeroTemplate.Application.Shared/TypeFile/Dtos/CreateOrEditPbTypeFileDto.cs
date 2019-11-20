
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TypeFile.Dtos
{
    public class CreateOrEditPbTypeFileDto : EntityDto<int?>
    {

		[Required]
		public string TypeFileName { get; set; }
		
		
		public string Description { get; set; }
		
		

    }
}