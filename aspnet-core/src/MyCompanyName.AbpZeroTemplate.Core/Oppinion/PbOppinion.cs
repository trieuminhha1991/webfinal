using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using MyCompanyName.AbpZeroTemplate.Ebook;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Oppinion
{
	[Table("PbOppinions")]
    public class PbOppinion : Entity 
    {

		[Required]
		public virtual string Content { get; set; }
		

		public virtual long? UserId { get; set; }
		
        [ForeignKey("UserId")]
		public User UserFk { get; set; }
		
		public virtual int? PbEbookId { get; set; }
		
        [ForeignKey("PbEbookId")]
		public PbEbook PbEbookFk { get; set; }
		
    }
}