using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Status
{
	[Table("PbStatuses")]
    public class PbStatus : Entity 
    {

		[Required]
		public virtual string StatusName { get; set; }
		
		public virtual string Description { get; set; }
		

    }
}