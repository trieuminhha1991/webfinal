using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.SubjectEducation
{
	[Table("PbSubjectEducations")]
    public class PbSubjectEducation : Entity 
    {

		[Required]
		public virtual string SubjectName { get; set; }
		
		public virtual string Description { get; set; }
		

    }
}