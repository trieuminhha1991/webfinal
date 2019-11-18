
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Subject.Dtos
{
    public class PbSubjectDto : EntityDto
    {
		public string ClassName { get; set; }

		public string ObjectName { get; set; }

		public string ChapterName { get; set; }

		public string SectionName { get; set; }



    }
}