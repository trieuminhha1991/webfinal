
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Place.Dtos
{
    public class CreateOrEditPbPlaceDto : EntityDto<int?>
    {

		[Required]
		public string PlaceGroup { get; set; }
		
		
		public string PlaceName { get; set; }
		
		
		public string Description { get; set; }
		
		

    }
}