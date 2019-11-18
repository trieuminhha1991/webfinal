

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.Subject.Exporting;
using MyCompanyName.AbpZeroTemplate.Subject.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.Subject
{
	[AbpAuthorize(AppPermissions.Pages_PbSubjects)]
    public class PbSubjectsAppService : AbpZeroTemplateAppServiceBase, IPbSubjectsAppService
    {
		 private readonly IRepository<PbSubject> _pbSubjectRepository;
		 private readonly IPbSubjectsExcelExporter _pbSubjectsExcelExporter;
		 

		  public PbSubjectsAppService(IRepository<PbSubject> pbSubjectRepository, IPbSubjectsExcelExporter pbSubjectsExcelExporter ) 
		  {
			_pbSubjectRepository = pbSubjectRepository;
			_pbSubjectsExcelExporter = pbSubjectsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPbSubjectForViewDto>> GetAll(GetAllPbSubjectsInput input)
         {
			
			var filteredPbSubjects = _pbSubjectRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ClassName.Contains(input.Filter) || e.ObjectName.Contains(input.Filter) || e.ChapterName.Contains(input.Filter) || e.SectionName.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClassNameFilter),  e => e.ClassName.ToLower() == input.ClassNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObjectNameFilter),  e => e.ObjectName.ToLower() == input.ObjectNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ChapterNameFilter),  e => e.ChapterName.ToLower() == input.ChapterNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.SectionNameFilter),  e => e.SectionName.ToLower() == input.SectionNameFilter.ToLower().Trim());

			var pagedAndFilteredPbSubjects = filteredPbSubjects
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbSubjects = from o in pagedAndFilteredPbSubjects
                         select new GetPbSubjectForViewDto() {
							PbSubject = new PbSubjectDto
							{
                                ClassName = o.ClassName,
                                ObjectName = o.ObjectName,
                                ChapterName = o.ChapterName,
                                SectionName = o.SectionName,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPbSubjects.CountAsync();

            return new PagedResultDto<GetPbSubjectForViewDto>(
                totalCount,
                await pbSubjects.ToListAsync()
            );
         }
		 
		 public async Task<GetPbSubjectForViewDto> GetPbSubjectForView(int id)
         {
            var pbSubject = await _pbSubjectRepository.GetAsync(id);

            var output = new GetPbSubjectForViewDto { PbSubject = ObjectMapper.Map<PbSubjectDto>(pbSubject) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PbSubjects_Edit)]
		 public async Task<GetPbSubjectForEditOutput> GetPbSubjectForEdit(EntityDto input)
         {
            var pbSubject = await _pbSubjectRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbSubjectForEditOutput {PbSubject = ObjectMapper.Map<CreateOrEditPbSubjectDto>(pbSubject)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbSubjectDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PbSubjects_Create)]
		 protected virtual async Task Create(CreateOrEditPbSubjectDto input)
         {
            var pbSubject = ObjectMapper.Map<PbSubject>(input);

			

            await _pbSubjectRepository.InsertAsync(pbSubject);
         }

		 [AbpAuthorize(AppPermissions.Pages_PbSubjects_Edit)]
		 protected virtual async Task Update(CreateOrEditPbSubjectDto input)
         {
            var pbSubject = await _pbSubjectRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbSubject);
         }

		 [AbpAuthorize(AppPermissions.Pages_PbSubjects_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbSubjectRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbSubjectsToExcel(GetAllPbSubjectsForExcelInput input)
         {
			
			var filteredPbSubjects = _pbSubjectRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ClassName.Contains(input.Filter) || e.ObjectName.Contains(input.Filter) || e.ChapterName.Contains(input.Filter) || e.SectionName.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClassNameFilter),  e => e.ClassName.ToLower() == input.ClassNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObjectNameFilter),  e => e.ObjectName.ToLower() == input.ObjectNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ChapterNameFilter),  e => e.ChapterName.ToLower() == input.ChapterNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.SectionNameFilter),  e => e.SectionName.ToLower() == input.SectionNameFilter.ToLower().Trim());

			var query = (from o in filteredPbSubjects
                         select new GetPbSubjectForViewDto() { 
							PbSubject = new PbSubjectDto
							{
                                ClassName = o.ClassName,
                                ObjectName = o.ObjectName,
                                ChapterName = o.ChapterName,
                                SectionName = o.SectionName,
                                Id = o.Id
							}
						 });


            var pbSubjectListDtos = await query.ToListAsync();

            return _pbSubjectsExcelExporter.ExportToFile(pbSubjectListDtos);
         }


    }
}