using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.DownloadEbook.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}