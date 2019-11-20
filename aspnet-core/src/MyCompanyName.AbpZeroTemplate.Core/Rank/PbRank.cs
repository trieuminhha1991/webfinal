using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Rank
{
	[Table("PbRanks")]
    public class PbRank : Entity 
    {

		[Required]
		public virtual string RankName { get; set; }
		
		public virtual string Description { get; set; }
		

    }
}