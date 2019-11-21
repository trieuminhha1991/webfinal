using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.LinkPro.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.LinkPro.Exporting
{
    public class PbLinkProsExcelExporter : EpPlusExcelExporterBase, IPbLinkProsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbLinkProsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbLinkProForViewDto> pbLinkPros)
        {
            return CreateExcelPackage(
                "PbLinkPros.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbLinkPros"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("LinkName"),
                        (L("PbEbook")) + L("EbookName")
                        );

                    AddObjects(
                        sheet, 2, pbLinkPros,
                        _ => _.PbLinkPro.LinkName,
                        _ => _.PbEbookEbookName
                        );

					

                });
        }
    }
}
