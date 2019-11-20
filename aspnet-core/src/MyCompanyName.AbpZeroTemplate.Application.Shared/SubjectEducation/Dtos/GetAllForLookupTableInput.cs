using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.SubjectEducation.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}