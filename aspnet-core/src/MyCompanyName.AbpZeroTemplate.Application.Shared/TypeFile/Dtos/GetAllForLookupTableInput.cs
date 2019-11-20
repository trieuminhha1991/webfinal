using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.TypeFile.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}