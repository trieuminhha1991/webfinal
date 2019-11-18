
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Class.Dtos
{
    public class CreateOrEditPbClassDto : EntityDto<int?>
    {

		[Required]
		public string ClassGroup { get; set; }
		
		
		public string ClassName { get; set; }
		
		

    }
}