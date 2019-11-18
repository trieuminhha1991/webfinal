using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Class.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Class
{
    public interface IPbClassesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbClassForViewDto>> GetAll(GetAllPbClassesInput input);

        Task<GetPbClassForViewDto> GetPbClassForView(int id);

		Task<GetPbClassForEditOutput> GetPbClassForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbClassDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbClassesToExcel(GetAllPbClassesForExcelInput input);

		
    }
}