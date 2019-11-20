
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.PriceUser.Dtos
{
    public class CreateOrEditPbPriceUserDto : EntityDto<int?>
    {

		public decimal? Price { get; set; }
		
		
		public DateTime? Month { get; set; }
		
		
		 public long? UserId { get; set; }
		 
		 
    }
}