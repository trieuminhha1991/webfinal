using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Place.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}