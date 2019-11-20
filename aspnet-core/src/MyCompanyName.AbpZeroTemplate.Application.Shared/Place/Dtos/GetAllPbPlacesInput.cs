using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Place.Dtos
{
    public class GetAllPbPlacesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string PlaceGroupFilter { get; set; }

		public string PlaceNameFilter { get; set; }

		public string DescriptionFilter { get; set; }



    }
}