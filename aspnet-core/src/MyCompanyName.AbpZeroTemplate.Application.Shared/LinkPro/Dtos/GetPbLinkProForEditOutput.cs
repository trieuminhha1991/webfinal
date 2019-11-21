using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.LinkPro.Dtos
{
    public class GetPbLinkProForEditOutput
    {
		public CreateOrEditPbLinkProDto PbLinkPro { get; set; }

		public string PbEbookEbookName { get; set;}


    }
}