using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.Oppinion.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Oppinion.Exporting
{
    public interface IPbOppinionsExcelExporter
    {
        FileDto ExportToFile(List<GetPbOppinionForViewDto> pbOppinions);
    }
}