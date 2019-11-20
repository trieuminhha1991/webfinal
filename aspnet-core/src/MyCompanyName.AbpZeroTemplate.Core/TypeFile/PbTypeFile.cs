using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.TypeFile
{
	[Table("PbTypeFiles")]
    public class PbTypeFile : Entity 
    {

		[Required]
		public virtual string TypeFileName { get; set; }
		
		public virtual string Description { get; set; }
		

    }
}