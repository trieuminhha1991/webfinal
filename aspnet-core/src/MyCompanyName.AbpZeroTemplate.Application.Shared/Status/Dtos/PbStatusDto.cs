
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Status.Dtos
{
    public class PbStatusDto : EntityDto
    {
		public string StatusName { get; set; }

		public string Description { get; set; }



    }
}