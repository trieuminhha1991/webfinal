using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Subject
{
	[Table("PbSubjects")]
    public class PbSubject : Entity 
    {

		[Required]
		public virtual string ClassName { get; set; }
		
		public virtual string ObjectName { get; set; }
		
		public virtual string ChapterName { get; set; }
		
		public virtual string SectionName { get; set; }
		

    }
}