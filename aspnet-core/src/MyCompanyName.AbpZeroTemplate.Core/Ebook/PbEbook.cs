using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using MyCompanyName.AbpZeroTemplate.Class;
using MyCompanyName.AbpZeroTemplate.Place;
using MyCompanyName.AbpZeroTemplate.Rank;
using MyCompanyName.AbpZeroTemplate.Status;
using MyCompanyName.AbpZeroTemplate.Subject;
using MyCompanyName.AbpZeroTemplate.SubjectEducation;
using MyCompanyName.AbpZeroTemplate.TypeEbook;
using MyCompanyName.AbpZeroTemplate.TypeFile;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Ebook
{
	[Table("PbEbooks")]
    public class PbEbook : Entity 
    {

		[Required]
		public virtual string EbookName { get; set; }
		
		[Required]
		public virtual string Link { get; set; }
		
		public virtual DateTime? EbookDateStart { get; set; }
		
		public virtual bool Pro { get; set; }
		
		public virtual string LinkPro { get; set; }
		
		public virtual decimal? EbookPrice { get; set; }
		
		public virtual long EbookView { get; set; }
		
		public virtual long EbookLike { get; set; }
		
		public virtual long EbookDislike { get; set; }
		
		public virtual string Discription { get; set; }
		
		public virtual string EbookCover { get; set; }
		
		public virtual long? BookPage { get; set; }
		

		public virtual long UserId { get; set; }
		
        [ForeignKey("UserId")]
		public User UserFk { get; set; }
		
		public virtual int? PbClassId { get; set; }
		
        [ForeignKey("PbClassId")]
		public PbClass PbClassFk { get; set; }
		
		public virtual int? PbPlaceId { get; set; }
		
        [ForeignKey("PbPlaceId")]
		public PbPlace PbPlaceFk { get; set; }
		
		public virtual int PbRankId { get; set; }
		
        [ForeignKey("PbRankId")]
		public PbRank PbRankFk { get; set; }
		
		public virtual int PbStatusId { get; set; }
		
        [ForeignKey("PbStatusId")]
		public PbStatus PbStatusFk { get; set; }
		
		public virtual int? PbSubjectId { get; set; }
		
        [ForeignKey("PbSubjectId")]
		public PbSubject PbSubjectFk { get; set; }
		
		public virtual int? PbSubjectEducationId { get; set; }
		
        [ForeignKey("PbSubjectEducationId")]
		public PbSubjectEducation PbSubjectEducationFk { get; set; }
		
		public virtual int PbTypeEbookId { get; set; }
		
        [ForeignKey("PbTypeEbookId")]
		public PbTypeEbook PbTypeEbookFk { get; set; }
		
		public virtual int PbTypeFileId { get; set; }
		
        [ForeignKey("PbTypeFileId")]
		public PbTypeFile PbTypeFileFk { get; set; }
		
    }
}