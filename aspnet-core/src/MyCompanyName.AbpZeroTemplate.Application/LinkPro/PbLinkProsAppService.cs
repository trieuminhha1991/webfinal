using MyCompanyName.AbpZeroTemplate.Ebook;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.LinkPro.Exporting;
using MyCompanyName.AbpZeroTemplate.LinkPro.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.LinkPro
{
	[AbpAuthorize(AppPermissions.Pages_Administration_PbLinkPros)]
    public class PbLinkProsAppService : AbpZeroTemplateAppServiceBase, IPbLinkProsAppService
    {
		 private readonly IRepository<PbLinkPro> _pbLinkProRepository;
		 private readonly IPbLinkProsExcelExporter _pbLinkProsExcelExporter;
		 private readonly IRepository<PbEbook,int> _lookup_pbEbookRepository;
		 

		  public PbLinkProsAppService(IRepository<PbLinkPro> pbLinkProRepository, IPbLinkProsExcelExporter pbLinkProsExcelExporter , IRepository<PbEbook, int> lookup_pbEbookRepository) 
		  {
			_pbLinkProRepository = pbLinkProRepository;
			_pbLinkProsExcelExporter = pbLinkProsExcelExporter;
			_lookup_pbEbookRepository = lookup_pbEbookRepository;
		
		  }

		 public async Task<PagedResultDto<GetPbLinkProForViewDto>> GetAll(GetAllPbLinkProsInput input)
         {
			
			var filteredPbLinkPros = _pbLinkProRepository.GetAll()
						.Include( e => e.PbEbookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.LinkName.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.LinkNameFilter),  e => e.LinkName.ToLower() == input.LinkNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbEbookEbookNameFilter), e => e.PbEbookFk != null && e.PbEbookFk.EbookName.ToLower() == input.PbEbookEbookNameFilter.ToLower().Trim());

			var pagedAndFilteredPbLinkPros = filteredPbLinkPros
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbLinkPros = from o in pagedAndFilteredPbLinkPros
                         join o1 in _lookup_pbEbookRepository.GetAll() on o.PbEbookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetPbLinkProForViewDto() {
							PbLinkPro = new PbLinkProDto
							{
                                LinkName = o.LinkName,
                                Id = o.Id
							},
                         	PbEbookEbookName = s1 == null ? "" : s1.EbookName.ToString()
						};

            var totalCount = await filteredPbLinkPros.CountAsync();

            return new PagedResultDto<GetPbLinkProForViewDto>(
                totalCount,
                await pbLinkPros.ToListAsync()
            );
         }
		 
		 public async Task<GetPbLinkProForViewDto> GetPbLinkProForView(int id)
         {
            var pbLinkPro = await _pbLinkProRepository.GetAsync(id);

            var output = new GetPbLinkProForViewDto { PbLinkPro = ObjectMapper.Map<PbLinkProDto>(pbLinkPro) };

		    if (output.PbLinkPro.PbEbookId != null)
            {
                var _lookupPbEbook = await _lookup_pbEbookRepository.FirstOrDefaultAsync((int)output.PbLinkPro.PbEbookId);
                output.PbEbookEbookName = _lookupPbEbook.EbookName.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_PbLinkPros_Edit)]
		 public async Task<GetPbLinkProForEditOutput> GetPbLinkProForEdit(EntityDto input)
         {
            var pbLinkPro = await _pbLinkProRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbLinkProForEditOutput {PbLinkPro = ObjectMapper.Map<CreateOrEditPbLinkProDto>(pbLinkPro)};

		    if (output.PbLinkPro.PbEbookId != null)
            {
                var _lookupPbEbook = await _lookup_pbEbookRepository.FirstOrDefaultAsync((int)output.PbLinkPro.PbEbookId);
                output.PbEbookEbookName = _lookupPbEbook.EbookName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbLinkProDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbLinkPros_Create)]
		 protected virtual async Task Create(CreateOrEditPbLinkProDto input)
         {
            var pbLinkPro = ObjectMapper.Map<PbLinkPro>(input);

			

            await _pbLinkProRepository.InsertAsync(pbLinkPro);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbLinkPros_Edit)]
		 protected virtual async Task Update(CreateOrEditPbLinkProDto input)
         {
            var pbLinkPro = await _pbLinkProRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbLinkPro);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbLinkPros_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbLinkProRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbLinkProsToExcel(GetAllPbLinkProsForExcelInput input)
         {
			
			var filteredPbLinkPros = _pbLinkProRepository.GetAll()
						.Include( e => e.PbEbookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.LinkName.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.LinkNameFilter),  e => e.LinkName.ToLower() == input.LinkNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbEbookEbookNameFilter), e => e.PbEbookFk != null && e.PbEbookFk.EbookName.ToLower() == input.PbEbookEbookNameFilter.ToLower().Trim());

			var query = (from o in filteredPbLinkPros
                         join o1 in _lookup_pbEbookRepository.GetAll() on o.PbEbookId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetPbLinkProForViewDto() { 
							PbLinkPro = new PbLinkProDto
							{
                                LinkName = o.LinkName,
                                Id = o.Id
							},
                         	PbEbookEbookName = s1 == null ? "" : s1.EbookName.ToString()
						 });


            var pbLinkProListDtos = await query.ToListAsync();

            return _pbLinkProsExcelExporter.ExportToFile(pbLinkProListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_Administration_PbLinkPros)]
         public async Task<PagedResultDto<PbLinkProPbEbookLookupTableDto>> GetAllPbEbookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_pbEbookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.EbookName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var pbEbookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbLinkProPbEbookLookupTableDto>();
			foreach(var pbEbook in pbEbookList){
				lookupTableDtoList.Add(new PbLinkProPbEbookLookupTableDto
				{
					Id = pbEbook.Id,
					DisplayName = pbEbook.EbookName?.ToString()
				});
			}

            return new PagedResultDto<PbLinkProPbEbookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}