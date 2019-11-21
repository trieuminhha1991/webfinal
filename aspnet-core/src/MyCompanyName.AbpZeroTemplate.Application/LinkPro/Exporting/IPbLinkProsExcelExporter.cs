using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.LinkPro.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.LinkPro.Exporting
{
    public interface IPbLinkProsExcelExporter
    {
        FileDto ExportToFile(List<GetPbLinkProForViewDto> pbLinkPros);
    }
}