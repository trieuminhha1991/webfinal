using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Class.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.Class.Exporting
{
    public class PbClassesExcelExporter : EpPlusExcelExporterBase, IPbClassesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbClassesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbClassForViewDto> pbClasses)
        {
            return CreateExcelPackage(
                "PbClasses.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbClasses"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ClassGroup"),
                        L("ClassName")
                        );

                    AddObjects(
                        sheet, 2, pbClasses,
                        _ => _.PbClass.ClassGroup,
                        _ => _.PbClass.ClassName
                        );

					

                });
        }
    }
}
