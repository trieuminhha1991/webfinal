using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.TypeEbook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TypeEbook.Exporting
{
    public interface IPbTypeEbooksExcelExporter
    {
        FileDto ExportToFile(List<GetPbTypeEbookForViewDto> pbTypeEbooks);
    }
}