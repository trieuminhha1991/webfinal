using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Ebook.Dtos
{
    public class GetAll2PbEbooksInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string EbookNameFilter { get; set; }

        public int ProFilter { get; set; }

        public long? MaxBookPageFilter { get; set; }
        public long? MinBookPageFilter { get; set; }


        public string UserNameFilter { get; set; }

        public string PbClassClassNameFilter { get; set; }

        public string PbRankRankNameFilter { get; set; }

        public string PbTypeEbookTypeNameFilter { get; set; }

        public string PbTypeFileTypeFileNameFilter { get; set; }


    }
}