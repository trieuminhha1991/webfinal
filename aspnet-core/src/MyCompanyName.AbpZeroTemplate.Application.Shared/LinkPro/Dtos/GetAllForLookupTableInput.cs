using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.LinkPro.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}