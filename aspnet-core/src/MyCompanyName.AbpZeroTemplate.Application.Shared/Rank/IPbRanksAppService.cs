using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Rank.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Rank
{
    public interface IPbRanksAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbRankForViewDto>> GetAll(GetAllPbRanksInput input);

        Task<GetPbRankForViewDto> GetPbRankForView(int id);

		Task<GetPbRankForEditOutput> GetPbRankForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbRankDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbRanksToExcel(GetAllPbRanksForExcelInput input);

		
    }
}