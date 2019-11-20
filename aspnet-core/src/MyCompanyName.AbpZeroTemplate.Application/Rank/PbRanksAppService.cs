

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.Rank.Exporting;
using MyCompanyName.AbpZeroTemplate.Rank.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.Rank
{
	[AbpAuthorize(AppPermissions.Pages_Administration_PbRanks)]
    public class PbRanksAppService : AbpZeroTemplateAppServiceBase, IPbRanksAppService
    {
		 private readonly IRepository<PbRank> _pbRankRepository;
		 private readonly IPbRanksExcelExporter _pbRanksExcelExporter;
		 

		  public PbRanksAppService(IRepository<PbRank> pbRankRepository, IPbRanksExcelExporter pbRanksExcelExporter ) 
		  {
			_pbRankRepository = pbRankRepository;
			_pbRanksExcelExporter = pbRanksExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPbRankForViewDto>> GetAll(GetAllPbRanksInput input)
         {
			
			var filteredPbRanks = _pbRankRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.RankName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RankNameFilter),  e => e.RankName.ToLower() == input.RankNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var pagedAndFilteredPbRanks = filteredPbRanks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbRanks = from o in pagedAndFilteredPbRanks
                         select new GetPbRankForViewDto() {
							PbRank = new PbRankDto
							{
                                RankName = o.RankName,
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPbRanks.CountAsync();

            return new PagedResultDto<GetPbRankForViewDto>(
                totalCount,
                await pbRanks.ToListAsync()
            );
         }
		 
		 public async Task<GetPbRankForViewDto> GetPbRankForView(int id)
         {
            var pbRank = await _pbRankRepository.GetAsync(id);

            var output = new GetPbRankForViewDto { PbRank = ObjectMapper.Map<PbRankDto>(pbRank) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_PbRanks_Edit)]
		 public async Task<GetPbRankForEditOutput> GetPbRankForEdit(EntityDto input)
         {
            var pbRank = await _pbRankRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbRankForEditOutput {PbRank = ObjectMapper.Map<CreateOrEditPbRankDto>(pbRank)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbRankDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbRanks_Create)]
		 protected virtual async Task Create(CreateOrEditPbRankDto input)
         {
            var pbRank = ObjectMapper.Map<PbRank>(input);

			

            await _pbRankRepository.InsertAsync(pbRank);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbRanks_Edit)]
		 protected virtual async Task Update(CreateOrEditPbRankDto input)
         {
            var pbRank = await _pbRankRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbRank);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbRanks_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbRankRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbRanksToExcel(GetAllPbRanksForExcelInput input)
         {
			
			var filteredPbRanks = _pbRankRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.RankName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RankNameFilter),  e => e.RankName.ToLower() == input.RankNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var query = (from o in filteredPbRanks
                         select new GetPbRankForViewDto() { 
							PbRank = new PbRankDto
							{
                                RankName = o.RankName,
                                Description = o.Description,
                                Id = o.Id
							}
						 });


            var pbRankListDtos = await query.ToListAsync();

            return _pbRanksExcelExporter.ExportToFile(pbRankListDtos);
         }


    }
}