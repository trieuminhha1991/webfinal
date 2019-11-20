
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Place.Dtos
{
    public class PbPlaceDto : EntityDto
    {
		public string PlaceGroup { get; set; }

		public string PlaceName { get; set; }

		public string Description { get; set; }



    }
}