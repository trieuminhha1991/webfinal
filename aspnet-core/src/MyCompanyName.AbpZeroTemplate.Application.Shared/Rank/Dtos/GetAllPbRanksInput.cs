using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Rank.Dtos
{
    public class GetAllPbRanksInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string RankNameFilter { get; set; }

		public string DescriptionFilter { get; set; }



    }
}