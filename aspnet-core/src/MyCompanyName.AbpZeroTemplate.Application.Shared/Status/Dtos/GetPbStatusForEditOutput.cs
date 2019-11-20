using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Status.Dtos
{
    public class GetPbStatusForEditOutput
    {
		public CreateOrEditPbStatusDto PbStatus { get; set; }


    }
}