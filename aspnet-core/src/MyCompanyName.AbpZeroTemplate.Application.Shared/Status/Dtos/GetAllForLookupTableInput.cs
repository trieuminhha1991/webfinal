using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Status.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}