using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Class
{
	[Table("PbClasses")]
    public class PbClass : Entity 
    {

		[Required]
		public virtual string ClassGroup { get; set; }
		
		public virtual string ClassName { get; set; }
		

    }
}