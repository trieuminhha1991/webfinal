using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Place.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Place
{
    public interface IPbPlacesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbPlaceForViewDto>> GetAll(GetAllPbPlacesInput input);

        Task<GetPbPlaceForViewDto> GetPbPlaceForView(int id);

		Task<GetPbPlaceForEditOutput> GetPbPlaceForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbPlaceDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbPlacesToExcel(GetAllPbPlacesForExcelInput input);

		
    }
}