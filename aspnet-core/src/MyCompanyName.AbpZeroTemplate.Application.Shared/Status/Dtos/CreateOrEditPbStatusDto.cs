
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Status.Dtos
{
    public class CreateOrEditPbStatusDto : EntityDto<int?>
    {

		[Required]
		public string StatusName { get; set; }
		
		
		public string Description { get; set; }
		
		

    }
}