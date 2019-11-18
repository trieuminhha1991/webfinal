

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.Class.Exporting;
using MyCompanyName.AbpZeroTemplate.Class.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.Class
{
	[AbpAuthorize(AppPermissions.Pages_Administration_PbClasses)]
    public class PbClassesAppService : AbpZeroTemplateAppServiceBase, IPbClassesAppService
    {
		 private readonly IRepository<PbClass> _pbClassRepository;
		 private readonly IPbClassesExcelExporter _pbClassesExcelExporter;
		 

		  public PbClassesAppService(IRepository<PbClass> pbClassRepository, IPbClassesExcelExporter pbClassesExcelExporter ) 
		  {
			_pbClassRepository = pbClassRepository;
			_pbClassesExcelExporter = pbClassesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPbClassForViewDto>> GetAll(GetAllPbClassesInput input)
         {
			
			var filteredPbClasses = _pbClassRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ClassGroup.Contains(input.Filter) || e.ClassName.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClassGroupFilter),  e => e.ClassGroup.ToLower() == input.ClassGroupFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClassNameFilter),  e => e.ClassName.ToLower() == input.ClassNameFilter.ToLower().Trim());

			var pagedAndFilteredPbClasses = filteredPbClasses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbClasses = from o in pagedAndFilteredPbClasses
                         select new GetPbClassForViewDto() {
							PbClass = new PbClassDto
							{
                                ClassGroup = o.ClassGroup,
                                ClassName = o.ClassName,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPbClasses.CountAsync();

            return new PagedResultDto<GetPbClassForViewDto>(
                totalCount,
                await pbClasses.ToListAsync()
            );
         }
		 
		 public async Task<GetPbClassForViewDto> GetPbClassForView(int id)
         {
            var pbClass = await _pbClassRepository.GetAsync(id);

            var output = new GetPbClassForViewDto { PbClass = ObjectMapper.Map<PbClassDto>(pbClass) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_PbClasses_Edit)]
		 public async Task<GetPbClassForEditOutput> GetPbClassForEdit(EntityDto input)
         {
            var pbClass = await _pbClassRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbClassForEditOutput {PbClass = ObjectMapper.Map<CreateOrEditPbClassDto>(pbClass)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbClassDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbClasses_Create)]
		 protected virtual async Task Create(CreateOrEditPbClassDto input)
         {
            var pbClass = ObjectMapper.Map<PbClass>(input);

			

            await _pbClassRepository.InsertAsync(pbClass);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbClasses_Edit)]
		 protected virtual async Task Update(CreateOrEditPbClassDto input)
         {
            var pbClass = await _pbClassRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbClass);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbClasses_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbClassRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbClassesToExcel(GetAllPbClassesForExcelInput input)
         {
			
			var filteredPbClasses = _pbClassRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ClassGroup.Contains(input.Filter) || e.ClassName.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClassGroupFilter),  e => e.ClassGroup.ToLower() == input.ClassGroupFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClassNameFilter),  e => e.ClassName.ToLower() == input.ClassNameFilter.ToLower().Trim());

			var query = (from o in filteredPbClasses
                         select new GetPbClassForViewDto() { 
							PbClass = new PbClassDto
							{
                                ClassGroup = o.ClassGroup,
                                ClassName = o.ClassName,
                                Id = o.Id
							}
						 });


            var pbClassListDtos = await query.ToListAsync();

            return _pbClassesExcelExporter.ExportToFile(pbClassListDtos);
         }


    }
}