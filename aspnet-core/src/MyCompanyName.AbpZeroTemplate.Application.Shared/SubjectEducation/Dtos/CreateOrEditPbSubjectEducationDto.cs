
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.SubjectEducation.Dtos
{
    public class CreateOrEditPbSubjectEducationDto : EntityDto<int?>
    {

		[Required]
		public string SubjectName { get; set; }
		
		
		public string Description { get; set; }
		
		

    }
}