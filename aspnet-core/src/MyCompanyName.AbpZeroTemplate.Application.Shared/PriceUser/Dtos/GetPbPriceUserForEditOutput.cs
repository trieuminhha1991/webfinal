using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.PriceUser.Dtos
{
    public class GetPbPriceUserForEditOutput
    {
		public CreateOrEditPbPriceUserDto PbPriceUser { get; set; }

		public string UserName { get; set;}


    }
}