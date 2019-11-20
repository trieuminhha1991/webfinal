using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Status.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.Status.Exporting
{
    public class PbStatusesExcelExporter : EpPlusExcelExporterBase, IPbStatusesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbStatusesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbStatusForViewDto> pbStatuses)
        {
            return CreateExcelPackage(
                "PbStatuses.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbStatuses"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("StatusName"),
                        L("Description")
                        );

                    AddObjects(
                        sheet, 2, pbStatuses,
                        _ => _.PbStatus.StatusName,
                        _ => _.PbStatus.Description
                        );

					

                });
        }
    }
}
