using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TypeEbook.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}