using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.PriceUser
{
	[Table("PbPriceUsers")]
    public class PbPriceUser : Entity 
    {

		public virtual decimal? Price { get; set; }
		
		public virtual DateTime? Month { get; set; }
		

		public virtual long? UserId { get; set; }
		
        [ForeignKey("UserId")]
		public User UserFk { get; set; }
		
    }
}