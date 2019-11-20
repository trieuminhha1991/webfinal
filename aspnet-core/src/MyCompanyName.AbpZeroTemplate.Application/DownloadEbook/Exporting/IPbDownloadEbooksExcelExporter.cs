using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.DownloadEbook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.DownloadEbook.Exporting
{
    public interface IPbDownloadEbooksExcelExporter
    {
        FileDto ExportToFile(List<GetPbDownloadEbookForViewDto> pbDownloadEbooks);
    }
}