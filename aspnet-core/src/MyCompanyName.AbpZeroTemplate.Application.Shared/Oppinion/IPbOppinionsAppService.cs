using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Oppinion.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Oppinion
{
    public interface IPbOppinionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbOppinionForViewDto>> GetAll(GetAllPbOppinionsInput input);

        Task<GetPbOppinionForViewDto> GetPbOppinionForView(int id);

		Task<GetPbOppinionForEditOutput> GetPbOppinionForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbOppinionDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbOppinionsToExcel(GetAllPbOppinionsForExcelInput input);

		
		Task<PagedResultDto<PbOppinionUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<PbOppinionPbEbookLookupTableDto>> GetAllPbEbookForLookupTable(GetAllForLookupTableInput input);
		
    }
}