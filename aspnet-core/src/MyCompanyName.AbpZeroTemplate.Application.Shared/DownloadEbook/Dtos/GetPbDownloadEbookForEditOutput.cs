using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.DownloadEbook.Dtos
{
    public class GetPbDownloadEbookForEditOutput
    {
		public CreateOrEditPbDownloadEbookDto PbDownloadEbook { get; set; }

		public string PbEbookEbookName { get; set;}


    }
}