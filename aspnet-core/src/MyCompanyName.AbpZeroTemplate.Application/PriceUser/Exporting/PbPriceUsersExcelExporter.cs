using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using MyCompanyName.AbpZeroTemplate.DataExporting.Excel.EpPlus;
using MyCompanyName.AbpZeroTemplate.PriceUser.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using MyCompanyName.AbpZeroTemplate.Storage;

namespace MyCompanyName.AbpZeroTemplate.PriceUser.Exporting
{
    public class PbPriceUsersExcelExporter : EpPlusExcelExporterBase, IPbPriceUsersExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PbPriceUsersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPbPriceUserForViewDto> pbPriceUsers)
        {
            return CreateExcelPackage(
                "PbPriceUsers.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PbPriceUsers"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Price"),
                        L("Month"),
                        (L("User")) + L("Name")
                        );

                    AddObjects(
                        sheet, 2, pbPriceUsers,
                        _ => _.PbPriceUser.Price,
                        _ => _timeZoneConverter.Convert(_.PbPriceUser.Month, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.UserName
                        );

					var monthColumn = sheet.Column(2);
                    monthColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					monthColumn.AutoFit();
					

                });
        }
    }
}
