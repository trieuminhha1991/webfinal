
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.DownloadEbook.Dtos
{
    public class CreateOrEditPbDownloadEbookDto : EntityDto<int?>
    {

		public long Number { get; set; }
		
		
		public DateTime Month { get; set; }
		
		
		 public int? PbEbookId { get; set; }
		 
		 
    }
}