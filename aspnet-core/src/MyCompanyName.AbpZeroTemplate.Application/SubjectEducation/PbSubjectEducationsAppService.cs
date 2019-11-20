

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.SubjectEducation.Exporting;
using MyCompanyName.AbpZeroTemplate.SubjectEducation.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.SubjectEducation
{
	[AbpAuthorize(AppPermissions.Pages_Administration_PbSubjectEducations)]
    public class PbSubjectEducationsAppService : AbpZeroTemplateAppServiceBase, IPbSubjectEducationsAppService
    {
		 private readonly IRepository<PbSubjectEducation> _pbSubjectEducationRepository;
		 private readonly IPbSubjectEducationsExcelExporter _pbSubjectEducationsExcelExporter;
		 

		  public PbSubjectEducationsAppService(IRepository<PbSubjectEducation> pbSubjectEducationRepository, IPbSubjectEducationsExcelExporter pbSubjectEducationsExcelExporter ) 
		  {
			_pbSubjectEducationRepository = pbSubjectEducationRepository;
			_pbSubjectEducationsExcelExporter = pbSubjectEducationsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPbSubjectEducationForViewDto>> GetAll(GetAllPbSubjectEducationsInput input)
         {
			
			var filteredPbSubjectEducations = _pbSubjectEducationRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.SubjectName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.SubjectNameFilter),  e => e.SubjectName.ToLower() == input.SubjectNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var pagedAndFilteredPbSubjectEducations = filteredPbSubjectEducations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbSubjectEducations = from o in pagedAndFilteredPbSubjectEducations
                         select new GetPbSubjectEducationForViewDto() {
							PbSubjectEducation = new PbSubjectEducationDto
							{
                                SubjectName = o.SubjectName,
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPbSubjectEducations.CountAsync();

            return new PagedResultDto<GetPbSubjectEducationForViewDto>(
                totalCount,
                await pbSubjectEducations.ToListAsync()
            );
         }
		 
		 public async Task<GetPbSubjectEducationForViewDto> GetPbSubjectEducationForView(int id)
         {
            var pbSubjectEducation = await _pbSubjectEducationRepository.GetAsync(id);

            var output = new GetPbSubjectEducationForViewDto { PbSubjectEducation = ObjectMapper.Map<PbSubjectEducationDto>(pbSubjectEducation) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_PbSubjectEducations_Edit)]
		 public async Task<GetPbSubjectEducationForEditOutput> GetPbSubjectEducationForEdit(EntityDto input)
         {
            var pbSubjectEducation = await _pbSubjectEducationRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbSubjectEducationForEditOutput {PbSubjectEducation = ObjectMapper.Map<CreateOrEditPbSubjectEducationDto>(pbSubjectEducation)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbSubjectEducationDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbSubjectEducations_Create)]
		 protected virtual async Task Create(CreateOrEditPbSubjectEducationDto input)
         {
            var pbSubjectEducation = ObjectMapper.Map<PbSubjectEducation>(input);

			

            await _pbSubjectEducationRepository.InsertAsync(pbSubjectEducation);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbSubjectEducations_Edit)]
		 protected virtual async Task Update(CreateOrEditPbSubjectEducationDto input)
         {
            var pbSubjectEducation = await _pbSubjectEducationRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbSubjectEducation);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbSubjectEducations_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbSubjectEducationRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbSubjectEducationsToExcel(GetAllPbSubjectEducationsForExcelInput input)
         {
			
			var filteredPbSubjectEducations = _pbSubjectEducationRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.SubjectName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.SubjectNameFilter),  e => e.SubjectName.ToLower() == input.SubjectNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var query = (from o in filteredPbSubjectEducations
                         select new GetPbSubjectEducationForViewDto() { 
							PbSubjectEducation = new PbSubjectEducationDto
							{
                                SubjectName = o.SubjectName,
                                Description = o.Description,
                                Id = o.Id
							}
						 });


            var pbSubjectEducationListDtos = await query.ToListAsync();

            return _pbSubjectEducationsExcelExporter.ExportToFile(pbSubjectEducationListDtos);
         }


    }
}