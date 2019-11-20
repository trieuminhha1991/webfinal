
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Oppinion.Dtos
{
    public class PbOppinionDto : EntityDto
    {
		public string Content { get; set; }


		 public long? UserId { get; set; }

		 		 public int? PbEbookId { get; set; }

		 
    }
}