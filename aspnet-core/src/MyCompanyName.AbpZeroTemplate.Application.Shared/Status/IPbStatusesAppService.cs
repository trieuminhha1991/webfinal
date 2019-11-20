using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Status.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Status
{
    public interface IPbStatusesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbStatusForViewDto>> GetAll(GetAllPbStatusesInput input);

        Task<GetPbStatusForViewDto> GetPbStatusForView(int id);

		Task<GetPbStatusForEditOutput> GetPbStatusForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbStatusDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbStatusesToExcel(GetAllPbStatusesForExcelInput input);

		
    }
}