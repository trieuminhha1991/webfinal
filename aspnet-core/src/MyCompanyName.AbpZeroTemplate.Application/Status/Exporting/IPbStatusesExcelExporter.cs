using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.Status.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Status.Exporting
{
    public interface IPbStatusesExcelExporter
    {
        FileDto ExportToFile(List<GetPbStatusForViewDto> pbStatuses);
    }
}