using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.PriceUser.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.PriceUser
{
    public interface IPbPriceUsersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbPriceUserForViewDto>> GetAll(GetAllPbPriceUsersInput input);

        Task<GetPbPriceUserForViewDto> GetPbPriceUserForView(int id);

		Task<GetPbPriceUserForEditOutput> GetPbPriceUserForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbPriceUserDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbPriceUsersToExcel(GetAllPbPriceUsersForExcelInput input);

		
		Task<PagedResultDto<PbPriceUserUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}