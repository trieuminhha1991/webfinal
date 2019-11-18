using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.Class.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Class.Exporting
{
    public interface IPbClassesExcelExporter
    {
        FileDto ExportToFile(List<GetPbClassForViewDto> pbClasses);
    }
}