
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Subject.Dtos
{
    public class CreateOrEditPbSubjectDto : EntityDto<int?>
    {

		[Required]
		public string ClassName { get; set; }
		
		
		public string ObjectName { get; set; }
		
		
		public string ChapterName { get; set; }
		
		
		public string SectionName { get; set; }
		
		

    }
}