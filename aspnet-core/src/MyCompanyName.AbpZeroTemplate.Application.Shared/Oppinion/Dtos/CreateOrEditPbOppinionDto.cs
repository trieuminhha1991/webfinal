
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Oppinion.Dtos
{
    public class CreateOrEditPbOppinionDto : EntityDto<int?>
    {

		[Required]
		public string Content { get; set; }
		
		
		 public long? UserId { get; set; }
		 
		 		 public int? PbEbookId { get; set; }
		 
		 
    }
}