using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Place
{
	[Table("PbPlaces")]
    public class PbPlace : Entity 
    {

		[Required]
		public virtual string PlaceGroup { get; set; }
		
		public virtual string PlaceName { get; set; }
		
		public virtual string Description { get; set; }
		

    }
}