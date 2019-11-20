using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.SubjectEducation.Dtos
{
    public class GetAllPbSubjectEducationsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string SubjectNameFilter { get; set; }

		public string DescriptionFilter { get; set; }



    }
}