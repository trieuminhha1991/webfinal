

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.TypeEbook.Exporting;
using MyCompanyName.AbpZeroTemplate.TypeEbook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.TypeEbook
{
	[AbpAuthorize(AppPermissions.Pages_Administration_PbTypeEbooks)]
    public class PbTypeEbooksAppService : AbpZeroTemplateAppServiceBase, IPbTypeEbooksAppService
    {
		 private readonly IRepository<PbTypeEbook> _pbTypeEbookRepository;
		 private readonly IPbTypeEbooksExcelExporter _pbTypeEbooksExcelExporter;
		 

		  public PbTypeEbooksAppService(IRepository<PbTypeEbook> pbTypeEbookRepository, IPbTypeEbooksExcelExporter pbTypeEbooksExcelExporter ) 
		  {
			_pbTypeEbookRepository = pbTypeEbookRepository;
			_pbTypeEbooksExcelExporter = pbTypeEbooksExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPbTypeEbookForViewDto>> GetAll(GetAllPbTypeEbooksInput input)
         {
			
			var filteredPbTypeEbooks = _pbTypeEbookRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TypeName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.TypeNameFilter),  e => e.TypeName.ToLower() == input.TypeNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var pagedAndFilteredPbTypeEbooks = filteredPbTypeEbooks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbTypeEbooks = from o in pagedAndFilteredPbTypeEbooks
                         select new GetPbTypeEbookForViewDto() {
							PbTypeEbook = new PbTypeEbookDto
							{
                                TypeName = o.TypeName,
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPbTypeEbooks.CountAsync();

            return new PagedResultDto<GetPbTypeEbookForViewDto>(
                totalCount,
                await pbTypeEbooks.ToListAsync()
            );
         }
		 
		 public async Task<GetPbTypeEbookForViewDto> GetPbTypeEbookForView(int id)
         {
            var pbTypeEbook = await _pbTypeEbookRepository.GetAsync(id);

            var output = new GetPbTypeEbookForViewDto { PbTypeEbook = ObjectMapper.Map<PbTypeEbookDto>(pbTypeEbook) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_PbTypeEbooks_Edit)]
		 public async Task<GetPbTypeEbookForEditOutput> GetPbTypeEbookForEdit(EntityDto input)
         {
            var pbTypeEbook = await _pbTypeEbookRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbTypeEbookForEditOutput {PbTypeEbook = ObjectMapper.Map<CreateOrEditPbTypeEbookDto>(pbTypeEbook)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbTypeEbookDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbTypeEbooks_Create)]
		 protected virtual async Task Create(CreateOrEditPbTypeEbookDto input)
         {
            var pbTypeEbook = ObjectMapper.Map<PbTypeEbook>(input);

			

            await _pbTypeEbookRepository.InsertAsync(pbTypeEbook);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbTypeEbooks_Edit)]
		 protected virtual async Task Update(CreateOrEditPbTypeEbookDto input)
         {
            var pbTypeEbook = await _pbTypeEbookRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbTypeEbook);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbTypeEbooks_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbTypeEbookRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbTypeEbooksToExcel(GetAllPbTypeEbooksForExcelInput input)
         {
			
			var filteredPbTypeEbooks = _pbTypeEbookRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TypeName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.TypeNameFilter),  e => e.TypeName.ToLower() == input.TypeNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var query = (from o in filteredPbTypeEbooks
                         select new GetPbTypeEbookForViewDto() { 
							PbTypeEbook = new PbTypeEbookDto
							{
                                TypeName = o.TypeName,
                                Description = o.Description,
                                Id = o.Id
							}
						 });


            var pbTypeEbookListDtos = await query.ToListAsync();

            return _pbTypeEbooksExcelExporter.ExportToFile(pbTypeEbookListDtos);
         }


    }
}