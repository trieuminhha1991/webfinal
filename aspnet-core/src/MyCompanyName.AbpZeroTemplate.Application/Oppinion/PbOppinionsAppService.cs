using MyCompanyName.AbpZeroTemplate.Authorization.Users;
using MyCompanyName.AbpZeroTemplate.Ebook;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.Oppinion.Exporting;
using MyCompanyName.AbpZeroTemplate.Oppinion.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.Oppinion
{
	[AbpAuthorize(AppPermissions.Pages_PbOppinions)]
    public class PbOppinionsAppService : AbpZeroTemplateAppServiceBase, IPbOppinionsAppService
    {
		 private readonly IRepository<PbOppinion> _pbOppinionRepository;
		 private readonly IPbOppinionsExcelExporter _pbOppinionsExcelExporter;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 private readonly IRepository<PbEbook,int> _lookup_pbEbookRepository;
		 

		  public PbOppinionsAppService(IRepository<PbOppinion> pbOppinionRepository, IPbOppinionsExcelExporter pbOppinionsExcelExporter , IRepository<User, long> lookup_userRepository, IRepository<PbEbook, int> lookup_pbEbookRepository) 
		  {
			_pbOppinionRepository = pbOppinionRepository;
			_pbOppinionsExcelExporter = pbOppinionsExcelExporter;
			_lookup_userRepository = lookup_userRepository;
		_lookup_pbEbookRepository = lookup_pbEbookRepository;
		
		  }

		 public async Task<PagedResultDto<GetPbOppinionForViewDto>> GetAll(GetAllPbOppinionsInput input)
         {
			
			var filteredPbOppinions = _pbOppinionRepository.GetAll()
						.Include( e => e.UserFk)
						.Include( e => e.PbEbookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Content.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContentFilter),  e => e.Content.ToLower() == input.ContentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserFk != null && e.UserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbEbookEbookNameFilter), e => e.PbEbookFk != null && e.PbEbookFk.EbookName.ToLower() == input.PbEbookEbookNameFilter.ToLower().Trim());

			var pagedAndFilteredPbOppinions = filteredPbOppinions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbOppinions = from o in pagedAndFilteredPbOppinions
                         join o1 in _lookup_userRepository.GetAll() on o.UserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_pbEbookRepository.GetAll() on o.PbEbookId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetPbOppinionForViewDto() {
							PbOppinion = new PbOppinionDto
							{
                                Content = o.Content,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString(),
                         	PbEbookEbookName = s2 == null ? "" : s2.EbookName.ToString()
						};

            var totalCount = await filteredPbOppinions.CountAsync();

            return new PagedResultDto<GetPbOppinionForViewDto>(
                totalCount,
                await pbOppinions.ToListAsync()
            );
         }
		 
		 public async Task<GetPbOppinionForViewDto> GetPbOppinionForView(int id)
         {
            var pbOppinion = await _pbOppinionRepository.GetAsync(id);

            var output = new GetPbOppinionForViewDto { PbOppinion = ObjectMapper.Map<PbOppinionDto>(pbOppinion) };

		    if (output.PbOppinion.UserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.PbOppinion.UserId);
                output.UserName = _lookupUser.Name.ToString();
            }

		    if (output.PbOppinion.PbEbookId != null)
            {
                var _lookupPbEbook = await _lookup_pbEbookRepository.FirstOrDefaultAsync((int)output.PbOppinion.PbEbookId);
                output.PbEbookEbookName = _lookupPbEbook.EbookName.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PbOppinions_Edit)]
		 public async Task<GetPbOppinionForEditOutput> GetPbOppinionForEdit(EntityDto input)
         {
            var pbOppinion = await _pbOppinionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbOppinionForEditOutput {PbOppinion = ObjectMapper.Map<CreateOrEditPbOppinionDto>(pbOppinion)};

		    if (output.PbOppinion.UserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.PbOppinion.UserId);
                output.UserName = _lookupUser.Name.ToString();
            }

		    if (output.PbOppinion.PbEbookId != null)
            {
                var _lookupPbEbook = await _lookup_pbEbookRepository.FirstOrDefaultAsync((int)output.PbOppinion.PbEbookId);
                output.PbEbookEbookName = _lookupPbEbook.EbookName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbOppinionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PbOppinions_Create)]
		 protected virtual async Task Create(CreateOrEditPbOppinionDto input)
         {
            var pbOppinion = ObjectMapper.Map<PbOppinion>(input);

			

            await _pbOppinionRepository.InsertAsync(pbOppinion);
         }

		 [AbpAuthorize(AppPermissions.Pages_PbOppinions_Edit)]
		 protected virtual async Task Update(CreateOrEditPbOppinionDto input)
         {
            var pbOppinion = await _pbOppinionRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbOppinion);
         }

		 [AbpAuthorize(AppPermissions.Pages_PbOppinions_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbOppinionRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbOppinionsToExcel(GetAllPbOppinionsForExcelInput input)
         {
			
			var filteredPbOppinions = _pbOppinionRepository.GetAll()
						.Include( e => e.UserFk)
						.Include( e => e.PbEbookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Content.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContentFilter),  e => e.Content.ToLower() == input.ContentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserFk != null && e.UserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PbEbookEbookNameFilter), e => e.PbEbookFk != null && e.PbEbookFk.EbookName.ToLower() == input.PbEbookEbookNameFilter.ToLower().Trim());

			var query = (from o in filteredPbOppinions
                         join o1 in _lookup_userRepository.GetAll() on o.UserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_pbEbookRepository.GetAll() on o.PbEbookId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetPbOppinionForViewDto() { 
							PbOppinion = new PbOppinionDto
							{
                                Content = o.Content,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString(),
                         	PbEbookEbookName = s2 == null ? "" : s2.EbookName.ToString()
						 });


            var pbOppinionListDtos = await query.ToListAsync();

            return _pbOppinionsExcelExporter.ExportToFile(pbOppinionListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_PbOppinions)]
         public async Task<PagedResultDto<PbOppinionUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbOppinionUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new PbOppinionUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<PbOppinionUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_PbOppinions)]
         public async Task<PagedResultDto<PbOppinionPbEbookLookupTableDto>> GetAllPbEbookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_pbEbookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.EbookName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var pbEbookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbOppinionPbEbookLookupTableDto>();
			foreach(var pbEbook in pbEbookList){
				lookupTableDtoList.Add(new PbOppinionPbEbookLookupTableDto
				{
					Id = pbEbook.Id,
					DisplayName = pbEbook.EbookName?.ToString()
				});
			}

            return new PagedResultDto<PbOppinionPbEbookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}