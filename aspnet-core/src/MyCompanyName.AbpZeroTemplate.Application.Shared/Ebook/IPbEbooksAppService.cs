using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Ebook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Ebook
{
    public interface IPbEbooksAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbEbookForViewDto>> GetAll(GetAllPbEbooksInput input);

        Task<GetPbEbookForViewDto> GetPbEbookForView(int id);

		Task<GetPbEbookForEditOutput> GetPbEbookForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbEbookDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbEbooksToExcel(GetAllPbEbooksForExcelInput input);

		
		Task<PagedResultDto<PbEbookUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<PbEbookPbClassLookupTableDto>> GetAllPbClassForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<PbEbookPbPlaceLookupTableDto>> GetAllPbPlaceForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<PbEbookPbRankLookupTableDto>> GetAllPbRankForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<PbEbookPbStatusLookupTableDto>> GetAllPbStatusForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<PbEbookPbSubjectLookupTableDto>> GetAllPbSubjectForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<PbEbookPbSubjectEducationLookupTableDto>> GetAllPbSubjectEducationForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<PbEbookPbTypeEbookLookupTableDto>> GetAllPbTypeEbookForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<PbEbookPbTypeFileLookupTableDto>> GetAllPbTypeFileForLookupTable(GetAllForLookupTableInput input);
		
    }
}