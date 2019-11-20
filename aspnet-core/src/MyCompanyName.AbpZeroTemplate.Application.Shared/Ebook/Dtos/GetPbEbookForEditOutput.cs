using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Ebook.Dtos
{
    public class GetPbEbookForEditOutput
    {
		public CreateOrEditPbEbookDto PbEbook { get; set; }

		public string UserName { get; set;}

		public string PbClassClassName { get; set;}

		public string PbPlacePlaceName { get; set;}

		public string PbRankRankName { get; set;}

		public string PbStatusStatusName { get; set;}

		public string PbSubjectSectionName { get; set;}

		public string PbSubjectEducationSubjectName { get; set;}

		public string PbTypeEbookTypeName { get; set;}

		public string PbTypeFileTypeFileName { get; set;}


    }
}