using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Place.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.Place.Exporting
{
    public class PbPlacesExcelExporter : EpPlusExcelExporterBase, IPbPlacesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbPlacesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbPlaceForViewDto> pbPlaces)
        {
            return CreateExcelPackage(
                "PbPlaces.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbPlaces"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("PlaceGroup"),
                        L("PlaceName"),
                        L("Description")
                        );

                    AddObjects(
                        sheet, 2, pbPlaces,
                        _ => _.PbPlace.PlaceGroup,
                        _ => _.PbPlace.PlaceName,
                        _ => _.PbPlace.Description
                        );

					

                });
        }
    }
}
