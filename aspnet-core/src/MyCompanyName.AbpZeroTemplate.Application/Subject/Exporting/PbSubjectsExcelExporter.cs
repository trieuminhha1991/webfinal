using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Subject.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.Subject.Exporting
{
    public class PbSubjectsExcelExporter : EpPlusExcelExporterBase, IPbSubjectsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbSubjectsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbSubjectForViewDto> pbSubjects)
        {
            return CreateExcelPackage(
                "PbSubjects.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbSubjects"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ClassName"),
                        L("ObjectName"),
                        L("ChapterName"),
                        L("SectionName")
                        );

                    AddObjects(
                        sheet, 2, pbSubjects,
                        _ => _.PbSubject.ClassName,
                        _ => _.PbSubject.ObjectName,
                        _ => _.PbSubject.ChapterName,
                        _ => _.PbSubject.SectionName
                        );

					

                });
        }
    }
}
