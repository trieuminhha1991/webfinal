
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.TypeEbook.Dtos
{
    public class CreateOrEditPbTypeEbookDto : EntityDto<int?>
    {

		[Required]
		public string TypeName { get; set; }
		
		
		public string Description { get; set; }
		
		

    }
}