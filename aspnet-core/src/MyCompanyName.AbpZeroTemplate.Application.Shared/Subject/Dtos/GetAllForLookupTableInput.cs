using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Subject.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}