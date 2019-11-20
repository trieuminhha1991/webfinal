

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.Status.Exporting;
using MyCompanyName.AbpZeroTemplate.Status.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.Status
{
	[AbpAuthorize(AppPermissions.Pages_Administration_PbStatuses)]
    public class PbStatusesAppService : AbpZeroTemplateAppServiceBase, IPbStatusesAppService
    {
		 private readonly IRepository<PbStatus> _pbStatusRepository;
		 private readonly IPbStatusesExcelExporter _pbStatusesExcelExporter;
		 

		  public PbStatusesAppService(IRepository<PbStatus> pbStatusRepository, IPbStatusesExcelExporter pbStatusesExcelExporter ) 
		  {
			_pbStatusRepository = pbStatusRepository;
			_pbStatusesExcelExporter = pbStatusesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPbStatusForViewDto>> GetAll(GetAllPbStatusesInput input)
         {
			
			var filteredPbStatuses = _pbStatusRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.StatusName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.StatusNameFilter),  e => e.StatusName.ToLower() == input.StatusNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var pagedAndFilteredPbStatuses = filteredPbStatuses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbStatuses = from o in pagedAndFilteredPbStatuses
                         select new GetPbStatusForViewDto() {
							PbStatus = new PbStatusDto
							{
                                StatusName = o.StatusName,
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPbStatuses.CountAsync();

            return new PagedResultDto<GetPbStatusForViewDto>(
                totalCount,
                await pbStatuses.ToListAsync()
            );
         }
		 
		 public async Task<GetPbStatusForViewDto> GetPbStatusForView(int id)
         {
            var pbStatus = await _pbStatusRepository.GetAsync(id);

            var output = new GetPbStatusForViewDto { PbStatus = ObjectMapper.Map<PbStatusDto>(pbStatus) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_PbStatuses_Edit)]
		 public async Task<GetPbStatusForEditOutput> GetPbStatusForEdit(EntityDto input)
         {
            var pbStatus = await _pbStatusRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbStatusForEditOutput {PbStatus = ObjectMapper.Map<CreateOrEditPbStatusDto>(pbStatus)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbStatusDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbStatuses_Create)]
		 protected virtual async Task Create(CreateOrEditPbStatusDto input)
         {
            var pbStatus = ObjectMapper.Map<PbStatus>(input);

			

            await _pbStatusRepository.InsertAsync(pbStatus);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbStatuses_Edit)]
		 protected virtual async Task Update(CreateOrEditPbStatusDto input)
         {
            var pbStatus = await _pbStatusRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbStatus);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbStatuses_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbStatusRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbStatusesToExcel(GetAllPbStatusesForExcelInput input)
         {
			
			var filteredPbStatuses = _pbStatusRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.StatusName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.StatusNameFilter),  e => e.StatusName.ToLower() == input.StatusNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var query = (from o in filteredPbStatuses
                         select new GetPbStatusForViewDto() { 
							PbStatus = new PbStatusDto
							{
                                StatusName = o.StatusName,
                                Description = o.Description,
                                Id = o.Id
							}
						 });


            var pbStatusListDtos = await query.ToListAsync();

            return _pbStatusesExcelExporter.ExportToFile(pbStatusListDtos);
         }


    }
}