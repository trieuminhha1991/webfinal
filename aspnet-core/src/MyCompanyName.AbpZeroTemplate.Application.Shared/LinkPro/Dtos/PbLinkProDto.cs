
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.LinkPro.Dtos
{
    public class PbLinkProDto : EntityDto
    {
		public string LinkName { get; set; }


		 public int? PbEbookId { get; set; }

		 
    }
}