using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Oppinion.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.Oppinion.Exporting
{
    public class PbOppinionsExcelExporter : EpPlusExcelExporterBase, IPbOppinionsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbOppinionsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbOppinionForViewDto> pbOppinions)
        {
            return CreateExcelPackage(
                "PbOppinions.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbOppinions"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Content"),
                        (L("User")) + L("Name"),
                        (L("PbEbook")) + L("EbookName")
                        );

                    AddObjects(
                        sheet, 2, pbOppinions,
                        _ => _.PbOppinion.Content,
                        _ => _.UserName,
                        _ => _.PbEbookEbookName
                        );

					

                });
        }
    }
}
