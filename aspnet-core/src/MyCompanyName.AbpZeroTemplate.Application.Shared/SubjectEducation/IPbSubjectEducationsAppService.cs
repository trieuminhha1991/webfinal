using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.SubjectEducation.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.SubjectEducation
{
    public interface IPbSubjectEducationsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbSubjectEducationForViewDto>> GetAll(GetAllPbSubjectEducationsInput input);

        Task<GetPbSubjectEducationForViewDto> GetPbSubjectEducationForView(int id);

		Task<GetPbSubjectEducationForEditOutput> GetPbSubjectEducationForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbSubjectEducationDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbSubjectEducationsToExcel(GetAllPbSubjectEducationsForExcelInput input);

		
    }
}