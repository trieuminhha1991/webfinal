using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.TypeFile.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.TypeFile.Exporting
{
    public class PbTypeFilesExcelExporter : EpPlusExcelExporterBase, IPbTypeFilesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbTypeFilesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbTypeFileForViewDto> pbTypeFiles)
        {
            return CreateExcelPackage(
                "PbTypeFiles.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbTypeFiles"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("TypeFileName"),
                        L("Description")
                        );

                    AddObjects(
                        sheet, 2, pbTypeFiles,
                        _ => _.PbTypeFile.TypeFileName,
                        _ => _.PbTypeFile.Description
                        );

					

                });
        }
    }
}
