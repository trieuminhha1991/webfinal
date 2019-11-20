using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Rank.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}