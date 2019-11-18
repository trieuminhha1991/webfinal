using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Class.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}