using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Subject.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Subject
{
    public interface IPbSubjectsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbSubjectForViewDto>> GetAll(GetAllPbSubjectsInput input);

        Task<GetPbSubjectForViewDto> GetPbSubjectForView(int id);

		Task<GetPbSubjectForEditOutput> GetPbSubjectForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbSubjectDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbSubjectsToExcel(GetAllPbSubjectsForExcelInput input);

		
    }
}