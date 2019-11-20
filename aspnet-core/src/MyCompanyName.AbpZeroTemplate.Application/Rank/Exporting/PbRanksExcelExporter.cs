using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.Rank.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.Rank.Exporting
{
    public class PbRanksExcelExporter : EpPlusExcelExporterBase, IPbRanksExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbRanksExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbRankForViewDto> pbRanks)
        {
            return CreateExcelPackage(
                "PbRanks.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbRanks"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("RankName"),
                        L("Description")
                        );

                    AddObjects(
                        sheet, 2, pbRanks,
                        _ => _.PbRank.RankName,
                        _ => _.PbRank.Description
                        );

					

                });
        }
    }
}
