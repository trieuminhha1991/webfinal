using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.Rank.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Rank.Exporting
{
    public interface IPbRanksExcelExporter
    {
        FileDto ExportToFile(List<GetPbRankForViewDto> pbRanks);
    }
}