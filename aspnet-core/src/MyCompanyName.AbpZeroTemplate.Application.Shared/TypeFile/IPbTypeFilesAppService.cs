using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TypeFile.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.TypeFile
{
    public interface IPbTypeFilesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbTypeFileForViewDto>> GetAll(GetAllPbTypeFilesInput input);

        Task<GetPbTypeFileForViewDto> GetPbTypeFileForView(int id);

		Task<GetPbTypeFileForEditOutput> GetPbTypeFileForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbTypeFileDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbTypeFilesToExcel(GetAllPbTypeFilesForExcelInput input);

		
    }
}