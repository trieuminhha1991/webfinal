using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Oppinion.Dtos
{
    public class GetPbOppinionForEditOutput
    {
		public CreateOrEditPbOppinionDto PbOppinion { get; set; }

		public string UserName { get; set;}

		public string PbEbookEbookName { get; set;}


    }
}