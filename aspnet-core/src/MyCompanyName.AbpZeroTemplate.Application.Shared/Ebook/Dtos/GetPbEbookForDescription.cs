using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Ebook.Dtos
{
    public class GetPbEbookForDescription : EntityDto
    {
        public string EbookName { get; set; }
        public string Link { get; set; }
        public DateTime? EbookDateStart { get; set; }
        public bool Pro { get; set; }
        public decimal? EbookPrice { get; set; }

        public long EbookView { get; set; }

        public long EbookLike { get; set; }

        public long EbookDislike { get; set; }
        public string EbookCover { get; set; }

        public long? BookPage { get; set; }
        public string UserName { get; set; }

        public string PbClassClassName { get; set; }

        public string PbRankRankName { get; set; }

        public string PbTypeEbookTypeName { get; set; }

        public string PbTypeFileTypeFileName { get; set; }


    }
    public class GetPbEbookSame : EntityDto
    {
        public string EbookName { get; set; }
        public string Link { get; set; }
        public string PbRankRankName { get; set; }
        public string EbookCover { get; set; }
        public long? BookPage { get; set; }
    }
}
}