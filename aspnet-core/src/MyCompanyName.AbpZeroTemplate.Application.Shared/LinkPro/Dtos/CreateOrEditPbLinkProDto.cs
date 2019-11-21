
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.LinkPro.Dtos
{
    public class CreateOrEditPbLinkProDto : EntityDto<int?>
    {

		[Required]
		public string LinkName { get; set; }
		
		
		 public int? PbEbookId { get; set; }
		 
		 
    }
}