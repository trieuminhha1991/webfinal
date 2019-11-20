
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.DownloadEbook.Dtos
{
    public class PbDownloadEbookDto : EntityDto
    {
		public long Number { get; set; }

		public DateTime Month { get; set; }


		 public int? PbEbookId { get; set; }

		 
    }
}