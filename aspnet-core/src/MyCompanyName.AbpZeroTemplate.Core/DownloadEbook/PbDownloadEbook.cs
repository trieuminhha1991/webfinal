using MyCompanyName.AbpZeroTemplate.Ebook;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.DownloadEbook
{
	[Table("PbDownloadEbooks")]
    public class PbDownloadEbook : Entity 
    {

		public virtual long Number { get; set; }
		
		public virtual DateTime Month { get; set; }
		

		public virtual int? PbEbookId { get; set; }
		
        [ForeignKey("PbEbookId")]
		public PbEbook PbEbookFk { get; set; }
		
    }
}