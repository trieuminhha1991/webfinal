using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Subject.Dtos
{
    public class GetAllPbSubjectsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string ClassNameFilter { get; set; }

		public string ObjectNameFilter { get; set; }

		public string ChapterNameFilter { get; set; }

		public string SectionNameFilter { get; set; }



    }
}