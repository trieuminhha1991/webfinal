using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.PriceUser.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}