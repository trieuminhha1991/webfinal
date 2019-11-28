

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.TypeFile.Exporting;
using MyCompanyName.AbpZeroTemplate.TypeFile.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.TypeFile
{
	[AbpAuthorize(AppPermissions.Pages_Administration_PbTypeFiles)]
    public class PbTypeFilesAppService : AbpZeroTemplateAppServiceBase, IPbTypeFilesAppService
    {
		 private readonly IRepository<PbTypeFile> _pbTypeFileRepository;
		 private readonly IPbTypeFilesExcelExporter _pbTypeFilesExcelExporter;
		 

		  public PbTypeFilesAppService(IRepository<PbTypeFile> pbTypeFileRepository, IPbTypeFilesExcelExporter pbTypeFilesExcelExporter ) 
		  {
			_pbTypeFileRepository = pbTypeFileRepository;
			_pbTypeFilesExcelExporter = pbTypeFilesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPbTypeFileForViewDto>> GetAll(GetAllPbTypeFilesInput input)
         {
			
			var filteredPbTypeFiles = _pbTypeFileRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TypeFileName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.TypeFileNameFilter),  e => e.TypeFileName.ToLower() == input.TypeFileNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var pagedAndFilteredPbTypeFiles = filteredPbTypeFiles
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbTypeFiles = from o in pagedAndFilteredPbTypeFiles
                         select new GetPbTypeFileForViewDto() {
							PbTypeFile = new PbTypeFileDto
							{
                                TypeFileName = o.TypeFileName,
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPbTypeFiles.CountAsync();

            return new PagedResultDto<GetPbTypeFileForViewDto>(
                totalCount,
                await pbTypeFiles.ToListAsync()
            );
         }
        public async Task<List<string>> GetAllTypeFile()
        {
            var filteredPbTypeEbooks = _pbTypeFileRepository.GetAll();
            var pbTypeEbooks = (from o in filteredPbTypeEbooks
                                select o.TypeFileName).ToList();
            return pbTypeEbooks;
        }
        public async Task<GetPbTypeFileForViewDto> GetPbTypeFileForView(int id)
         {
            var pbTypeFile = await _pbTypeFileRepository.GetAsync(id);

            var output = new GetPbTypeFileForViewDto { PbTypeFile = ObjectMapper.Map<PbTypeFileDto>(pbTypeFile) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_PbTypeFiles_Edit)]
		 public async Task<GetPbTypeFileForEditOutput> GetPbTypeFileForEdit(EntityDto input)
         {
            var pbTypeFile = await _pbTypeFileRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbTypeFileForEditOutput {PbTypeFile = ObjectMapper.Map<CreateOrEditPbTypeFileDto>(pbTypeFile)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbTypeFileDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbTypeFiles_Create)]
		 protected virtual async Task Create(CreateOrEditPbTypeFileDto input)
         {
            var pbTypeFile = ObjectMapper.Map<PbTypeFile>(input);

			

            await _pbTypeFileRepository.InsertAsync(pbTypeFile);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbTypeFiles_Edit)]
		 protected virtual async Task Update(CreateOrEditPbTypeFileDto input)
         {
            var pbTypeFile = await _pbTypeFileRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbTypeFile);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbTypeFiles_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbTypeFileRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbTypeFilesToExcel(GetAllPbTypeFilesForExcelInput input)
         {
			
			var filteredPbTypeFiles = _pbTypeFileRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.TypeFileName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.TypeFileNameFilter),  e => e.TypeFileName.ToLower() == input.TypeFileNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var query = (from o in filteredPbTypeFiles
                         select new GetPbTypeFileForViewDto() { 
							PbTypeFile = new PbTypeFileDto
							{
                                TypeFileName = o.TypeFileName,
                                Description = o.Description,
                                Id = o.Id
							}
						 });


            var pbTypeFileListDtos = await query.ToListAsync();

            return _pbTypeFilesExcelExporter.ExportToFile(pbTypeFileListDtos);
         }


    }
}