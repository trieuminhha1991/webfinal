using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.Place.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Place.Exporting
{
    public interface IPbPlacesExcelExporter
    {
        FileDto ExportToFile(List<GetPbPlaceForViewDto> pbPlaces);
    }
}