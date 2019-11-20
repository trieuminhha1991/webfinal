using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TypeEbook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TypeEbook.Exporting
{
    public class PbTypeEbooksExcelExporter : EpPlusExcelExporterBase, IPbTypeEbooksExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbTypeEbooksExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbTypeEbookForViewDto> pbTypeEbooks)
        {
            return CreateExcelPackage(
                "PbTypeEbooks.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbTypeEbooks"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("TypeName"),
                        L("Description")
                        );

                    AddObjects(
                        sheet, 2, pbTypeEbooks,
                        _ => _.PbTypeEbook.TypeName,
                        _ => _.PbTypeEbook.Description
                        );

					

                });
        }
    }
}
