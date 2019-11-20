using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.TypeFile.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TypeFile.Exporting
{
    public interface IPbTypeFilesExcelExporter
    {
        FileDto ExportToFile(List<GetPbTypeFileForViewDto> pbTypeFiles);
    }
}