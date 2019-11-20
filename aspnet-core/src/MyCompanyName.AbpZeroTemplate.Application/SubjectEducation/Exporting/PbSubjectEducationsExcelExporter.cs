using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.SubjectEducation.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.SubjectEducation.Exporting
{
    public class PbSubjectEducationsExcelExporter : EpPlusExcelExporterBase, IPbSubjectEducationsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbSubjectEducationsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbSubjectEducationForViewDto> pbSubjectEducations)
        {
            return CreateExcelPackage(
                "PbSubjectEducations.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbSubjectEducations"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("SubjectName"),
                        L("Description")
                        );

                    AddObjects(
                        sheet, 2, pbSubjectEducations,
                        _ => _.PbSubjectEducation.SubjectName,
                        _ => _.PbSubjectEducation.Description
                        );

					

                });
        }
    }
}
