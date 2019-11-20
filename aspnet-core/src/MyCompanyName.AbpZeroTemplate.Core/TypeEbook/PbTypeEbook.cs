using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.TypeEbook
{
	[Table("PbTypeEbooks")]
    public class PbTypeEbook : Entity 
    {

		[Required]
		public virtual string TypeName { get; set; }
		
		public virtual string Description { get; set; }
		

    }
}