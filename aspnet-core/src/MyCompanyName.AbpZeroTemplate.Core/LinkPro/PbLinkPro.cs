using MyCompanyName.AbpZeroTemplate.Ebook;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.LinkPro
{
	[Table("PbLinkPros")]
    public class PbLinkPro : Entity 
    {

		[Required]
		public virtual string LinkName { get; set; }
		

		public virtual int? PbEbookId { get; set; }
		
        [ForeignKey("PbEbookId")]
		public PbEbook PbEbookFk { get; set; }
		
    }
}