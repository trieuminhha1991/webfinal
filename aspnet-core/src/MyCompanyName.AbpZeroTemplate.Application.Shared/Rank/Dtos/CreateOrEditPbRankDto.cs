
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Rank.Dtos
{
    public class CreateOrEditPbRankDto : EntityDto<int?>
    {

		[Required]
		public string RankName { get; set; }
		
		
		public string Description { get; set; }
		
		

    }
}