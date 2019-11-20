using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.Ebook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Ebook.Exporting
{
    public interface IPbEbooksExcelExporter
    {
        FileDto ExportToFile(List<GetPbEbookForViewDto> pbEbooks);
    }
}