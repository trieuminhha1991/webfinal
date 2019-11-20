using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.PriceUser.Dtos
{
    public class GetAllPbPriceUsersInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public decimal? MaxPriceFilter { get; set; }
		public decimal? MinPriceFilter { get; set; }

		public DateTime? MaxMonthFilter { get; set; }
		public DateTime? MinMonthFilter { get; set; }


		 public string UserNameFilter { get; set; }

		 
    }
}