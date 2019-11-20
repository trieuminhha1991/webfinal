using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.PriceUser.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.PriceUser.Exporting
{
    public interface IPbPriceUsersExcelExporter
    {
        FileDto ExportToFile(List<GetPbPriceUserForViewDto> pbPriceUsers);
    }
}