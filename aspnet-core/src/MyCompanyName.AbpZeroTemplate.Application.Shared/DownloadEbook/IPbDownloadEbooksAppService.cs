using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.DownloadEbook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.DownloadEbook
{
    public interface IPbDownloadEbooksAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbDownloadEbookForViewDto>> GetAll(GetAllPbDownloadEbooksInput input);

        Task<GetPbDownloadEbookForViewDto> GetPbDownloadEbookForView(int id);

		Task<GetPbDownloadEbookForEditOutput> GetPbDownloadEbookForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbDownloadEbookDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbDownloadEbooksToExcel(GetAllPbDownloadEbooksForExcelInput input);

		
		Task<PagedResultDto<PbDownloadEbookPbEbookLookupTableDto>> GetAllPbEbookForLookupTable(GetAllForLookupTableInput input);
		
    }
}