using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Ebook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.Ebook.Exporting
{
    public class PbEbooksExcelExporter : EpPlusExcelExporterBase, IPbEbooksExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbEbooksExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbEbookForViewDto> pbEbooks)
        {
            return CreateExcelPackage(
                "PbEbooks.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbEbooks"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("EbookName"),
                        L("Link"),
                        L("EbookDateStart"),
                        L("Pro"),
                        L("EbookPrice"),
                        L("EbookView"),
                        L("EbookLike"),
                        L("EbookDislike"),
                        L("Discription"),
                        L("EbookCover"),
                        L("BookPage"),
                        (L("User")) + L("Name"),
                        (L("PbClass")) + L("ClassName"),
                        (L("PbPlace")) + L("PlaceName"),
                        (L("PbRank")) + L("RankName"),
                        (L("PbStatus")) + L("StatusName"),
                        (L("PbSubject")) + L("SectionName"),
                        (L("PbSubjectEducation")) + L("SubjectName"),
                        (L("PbTypeEbook")) + L("TypeName"),
                        (L("PbTypeFile")) + L("TypeFileName")
                        );

                    AddObjects(
                        sheet, 2, pbEbooks,
                        _ => _.PbEbook.EbookName,
                        _ => _.PbEbook.Link,
                        _ => _timeZoneConverter.Convert(_.PbEbook.EbookDateStart, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.PbEbook.Pro,
                        _ => _.PbEbook.EbookPrice,
                        _ => _.PbEbook.EbookView,
                        _ => _.PbEbook.EbookLike,
                        _ => _.PbEbook.EbookDislike,
                        _ => _.PbEbook.Discription,
                        _ => _.PbEbook.EbookCover,
                        _ => _.PbEbook.BookPage,
                        _ => _.UserName,
                        _ => _.PbClassClassName,
                        _ => _.PbPlacePlaceName,
                        _ => _.PbRankRankName,
                        _ => _.PbStatusStatusName,
                        _ => _.PbSubjectSectionName,
                        _ => _.PbSubjectEducationSubjectName,
                        _ => _.PbTypeEbookTypeName,
                        _ => _.PbTypeFileTypeFileName
                        );

					var ebookDateStartColumn = sheet.Column(3);
                    ebookDateStartColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					ebookDateStartColumn.AutoFit();
					

                });
        }
    }
}
