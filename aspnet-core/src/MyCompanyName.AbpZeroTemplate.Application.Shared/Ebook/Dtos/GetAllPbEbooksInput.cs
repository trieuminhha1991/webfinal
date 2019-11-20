using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Ebook.Dtos
{
    public class GetAllPbEbooksInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string EbookNameFilter { get; set; }

		public string LinkFilter { get; set; }

		public DateTime? MaxEbookDateStartFilter { get; set; }
		public DateTime? MinEbookDateStartFilter { get; set; }

		public int ProFilter { get; set; }

		public string LinkProFilter { get; set; }

		public decimal? MaxEbookPriceFilter { get; set; }
		public decimal? MinEbookPriceFilter { get; set; }

		public long? MaxEbookViewFilter { get; set; }
		public long? MinEbookViewFilter { get; set; }

		public long? MaxEbookLikeFilter { get; set; }
		public long? MinEbookLikeFilter { get; set; }

		public long? MaxEbookDislikeFilter { get; set; }
		public long? MinEbookDislikeFilter { get; set; }

		public string DiscriptionFilter { get; set; }

		public string EbookCoverFilter { get; set; }


		 public string UserNameFilter { get; set; }

		 		 public string PbClassClassNameFilter { get; set; }

		 		 public string PbPlacePlaceNameFilter { get; set; }

		 		 public string PbRankRankNameFilter { get; set; }

		 		 public string PbStatusStatusNameFilter { get; set; }

		 		 public string PbSubjectSectionNameFilter { get; set; }

		 		 public string PbSubjectEducationSubjectNameFilter { get; set; }

		 		 public string PbTypeEbookTypeNameFilter { get; set; }

		 		 public string PbTypeFileTypeFileNameFilter { get; set; }

		 
    }
}