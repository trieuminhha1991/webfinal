using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.DownloadEbook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.DownloadEbook.Exporting
{
    public class PbDownloadEbooksExcelExporter : EpPlusExcelExporterBase, IPbDownloadEbooksExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbDownloadEbooksExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbDownloadEbookForViewDto> pbDownloadEbooks)
        {
            return CreateExcelPackage(
                "PbDownloadEbooks.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbDownloadEbooks"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Number"),
                        L("Month"),
                        (L("PbEbook")) + L("EbookName")
                        );

                    AddObjects(
                        sheet, 2, pbDownloadEbooks,
                        _ => _.PbDownloadEbook.Number,
                        _ => _timeZoneConverter.Convert(_.PbDownloadEbook.Month, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.PbEbookEbookName
                        );

					var monthColumn = sheet.Column(2);
                    monthColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					monthColumn.AutoFit();
					

                });
        }
    }
}
