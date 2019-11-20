using MyCompanyName.AbpZeroTemplate.Ebook;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.DownloadEbook.Exporting;
using MyCompanyName.AbpZeroTemplate.DownloadEbook.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.DownloadEbook
{
	[AbpAuthorize(AppPermissions.Pages_PbDownloadEbooks)]
    public class PbDownloadEbooksAppService : AbpZeroTemplateAppServiceBase, IPbDownloadEbooksAppService
    {
		 private readonly IRepository<PbDownloadEbook> _pbDownloadEbookRepository;
		 private readonly IPbDownloadEbooksExcelExporter _pbDownloadEbooksExcelExporter;
		 private readonly IRepository<PbEbook,int> _lookup_pbEbookRepository;
		 

		  public PbDownloadEbooksAppService(IRepository<PbDownloadEbook> pbDownloadEbookRepository, IPbDownloadEbooksExcelExporter pbDownloadEbooksExcelExporter , IRepository<PbEbook, int> lookup_pbEbookRepository) 
		  {
			_pbDownloadEbookRepository = pbDownloadEbookRepository;
			_pbDownloadEbooksExcelExporter = pbDownloadEbooksExcelExporter;
			_lookup_pbEbookRepository = lookup_pbEbookRepository;
		
		  }

		 public async Task<PagedResultDto<GetPbDownloadEbookForViewDto>> GetAll(GetAllPbDownloadEbooksInput input)
         {
			
			var filteredPbDownloadEbooks = _pbDownloadEbookRepository.GetAll()
						.Include( e => e.PbEbookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinNumberFilter != null, e => e.Number >= input.MinNumberFilter)
						.WhereIf(input.MaxNumberFilter != null, e => e.Number <= input.MaxNumberFilter)
						.WhereIf(input.MinMonthFilter != null, e => e.Month >= input.MinMonthFilter)
						.WhereIf(input.MaxMonthFilter != null, e => e.Month <= input.MaxMonthFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbEbookEbookNameFilter), e => e.PbEbookFk != null && e.PbEbookFk.EbookName.ToLower() == input.PbEbookEbookNameFilter.ToLower().Trim());

			var pagedAndFilteredPbDownloadEbooks = filteredPbDownloadEbooks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbDownloadEbooks = from o in pagedAndFilteredPbDownloadEbooks
                         join o1 in _lookup_pbEbookRepository.GetAll() on o.PbEbookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetPbDownloadEbookForViewDto() {
							PbDownloadEbook = new PbDownloadEbookDto
							{
                                Number = o.Number,
                                Month = o.Month,
                                Id = o.Id
							},
                         	PbEbookEbookName = s1 == null ? "" : s1.EbookName.ToString()
						};

            var totalCount = await filteredPbDownloadEbooks.CountAsync();

            return new PagedResultDto<GetPbDownloadEbookForViewDto>(
                totalCount,
                await pbDownloadEbooks.ToListAsync()
            );
         }
		 
		 public async Task<GetPbDownloadEbookForViewDto> GetPbDownloadEbookForView(int id)
         {
            var pbDownloadEbook = await _pbDownloadEbookRepository.GetAsync(id);

            var output = new GetPbDownloadEbookForViewDto { PbDownloadEbook = ObjectMapper.Map<PbDownloadEbookDto>(pbDownloadEbook) };

		    if (output.PbDownloadEbook.PbEbookId != null)
            {
                var _lookupPbEbook = await _lookup_pbEbookRepository.FirstOrDefaultAsync((int)output.PbDownloadEbook.PbEbookId);
                output.PbEbookEbookName = _lookupPbEbook.EbookName.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PbDownloadEbooks_Edit)]
		 public async Task<GetPbDownloadEbookForEditOutput> GetPbDownloadEbookForEdit(EntityDto input)
         {
            var pbDownloadEbook = await _pbDownloadEbookRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbDownloadEbookForEditOutput {PbDownloadEbook = ObjectMapper.Map<CreateOrEditPbDownloadEbookDto>(pbDownloadEbook)};

		    if (output.PbDownloadEbook.PbEbookId != null)
            {
                var _lookupPbEbook = await _lookup_pbEbookRepository.FirstOrDefaultAsync((int)output.PbDownloadEbook.PbEbookId);
                output.PbEbookEbookName = _lookupPbEbook.EbookName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbDownloadEbookDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PbDownloadEbooks_Create)]
		 protected virtual async Task Create(CreateOrEditPbDownloadEbookDto input)
         {
            var pbDownloadEbook = ObjectMapper.Map<PbDownloadEbook>(input);

			

            await _pbDownloadEbookRepository.InsertAsync(pbDownloadEbook);
         }

		 [AbpAuthorize(AppPermissions.Pages_PbDownloadEbooks_Edit)]
		 protected virtual async Task Update(CreateOrEditPbDownloadEbookDto input)
         {
            var pbDownloadEbook = await _pbDownloadEbookRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbDownloadEbook);
         }

		 [AbpAuthorize(AppPermissions.Pages_PbDownloadEbooks_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbDownloadEbookRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbDownloadEbooksToExcel(GetAllPbDownloadEbooksForExcelInput input)
         {
			
			var filteredPbDownloadEbooks = _pbDownloadEbookRepository.GetAll()
						.Include( e => e.PbEbookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinNumberFilter != null, e => e.Number >= input.MinNumberFilter)
						.WhereIf(input.MaxNumberFilter != null, e => e.Number <= input.MaxNumberFilter)
						.WhereIf(input.MinMonthFilter != null, e => e.Month >= input.MinMonthFilter)
						.WhereIf(input.MaxMonthFilter != null, e => e.Month <= input.MaxMonthFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbEbookEbookNameFilter), e => e.PbEbookFk != null && e.PbEbookFk.EbookName.ToLower() == input.PbEbookEbookNameFilter.ToLower().Trim());

			var query = (from o in filteredPbDownloadEbooks
                         join o1 in _lookup_pbEbookRepository.GetAll() on o.PbEbookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetPbDownloadEbookForViewDto() { 
							PbDownloadEbook = new PbDownloadEbookDto
							{
                                Number = o.Number,
                                Month = o.Month,
                                Id = o.Id
							},
                         	PbEbookEbookName = s1 == null ? "" : s1.EbookName.ToString()
						 });


            var pbDownloadEbookListDtos = await query.ToListAsync();

            return _pbDownloadEbooksExcelExporter.ExportToFile(pbDownloadEbookListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_PbDownloadEbooks)]
         public async Task<PagedResultDto<PbDownloadEbookPbEbookLookupTableDto>> GetAllPbEbookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_pbEbookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.EbookName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var pbEbookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbDownloadEbookPbEbookLookupTableDto>();
			foreach(var pbEbook in pbEbookList){
				lookupTableDtoList.Add(new PbDownloadEbookPbEbookLookupTableDto
				{
					Id = pbEbook.Id,
					DisplayName = pbEbook.EbookName?.ToString()
				});
			}

            return new PagedResultDto<PbDownloadEbookPbEbookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}