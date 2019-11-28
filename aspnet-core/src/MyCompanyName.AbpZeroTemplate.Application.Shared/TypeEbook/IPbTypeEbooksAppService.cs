using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.TypeEbook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using System.Collections.Generic;

namespace MyCompanyName.AbpZeroTemplate.TypeEbook
{
    public interface IPbTypeEbooksAppService : IApplicationService 
    {
        Task<List<string>> GetAllEbook();
        Task<PagedResultDto<GetPbTypeEbookForViewDto>> GetAll(GetAllPbTypeEbooksInput input);

        Task<GetPbTypeEbookForViewDto> GetPbTypeEbookForView(int id);

		Task<GetPbTypeEbookForEditOutput> GetPbTypeEbookForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbTypeEbookDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbTypeEbooksToExcel(GetAllPbTypeEbooksForExcelInput input);

		
    }
}