
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.PriceUser.Dtos
{
    public class PbPriceUserDto : EntityDto
    {
		public decimal? Price { get; set; }

		public DateTime? Month { get; set; }


		 public long? UserId { get; set; }

		 
    }
}