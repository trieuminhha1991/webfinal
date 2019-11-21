using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.LinkPro.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.LinkPro
{
    public interface IPbLinkProsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPbLinkProForViewDto>> GetAll(GetAllPbLinkProsInput input);

        Task<GetPbLinkProForViewDto> GetPbLinkProForView(int id);

		Task<GetPbLinkProForEditOutput> GetPbLinkProForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPbLinkProDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetPbLinkProsToExcel(GetAllPbLinkProsForExcelInput input);

		
		Task<PagedResultDto<PbLinkProPbEbookLookupTableDto>> GetAllPbEbookForLookupTable(GetAllForLookupTableInput input);
		
    }
}