using MyCompanyName.AbpZeroTemplate.Authorization.Users;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.PriceUser.Exporting;
using MyCompanyName.AbpZeroTemplate.PriceUser.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.PriceUser
{
	[AbpAuthorize(AppPermissions.Pages_PbPriceUsers)]
    public class PbPriceUsersAppService : AbpZeroTemplateAppServiceBase, IPbPriceUsersAppService
    {
		 private readonly IRepository<PbPriceUser> _pbPriceUserRepository;
		 private readonly IPbPriceUsersExcelExporter _pbPriceUsersExcelExporter;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 

		  public PbPriceUsersAppService(IRepository<PbPriceUser> pbPriceUserRepository, IPbPriceUsersExcelExporter pbPriceUsersExcelExporter , IRepository<User, long> lookup_userRepository) 
		  {
			_pbPriceUserRepository = pbPriceUserRepository;
			_pbPriceUsersExcelExporter = pbPriceUsersExcelExporter;
			_lookup_userRepository = lookup_userRepository;
		
		  }

		 public async Task<PagedResultDto<GetPbPriceUserForViewDto>> GetAll(GetAllPbPriceUsersInput input)
         {
			
			var filteredPbPriceUsers = _pbPriceUserRepository.GetAll()
						.Include( e => e.UserFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinPriceFilter != null, e => e.Price >= input.MinPriceFilter)
						.WhereIf(input.MaxPriceFilter != null, e => e.Price <= input.MaxPriceFilter)
						.WhereIf(input.MinMonthFilter != null, e => e.Month >= input.MinMonthFilter)
						.WhereIf(input.MaxMonthFilter != null, e => e.Month <= input.MaxMonthFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserFk != null && e.UserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

			var pagedAndFilteredPbPriceUsers = filteredPbPriceUsers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbPriceUsers = from o in pagedAndFilteredPbPriceUsers
                         join o1 in _lookup_userRepository.GetAll() on o.UserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetPbPriceUserForViewDto() {
							PbPriceUser = new PbPriceUserDto
							{
                                Price = o.Price,
                                Month = o.Month,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString()
						};

            var totalCount = await filteredPbPriceUsers.CountAsync();

            return new PagedResultDto<GetPbPriceUserForViewDto>(
                totalCount,
                await pbPriceUsers.ToListAsync()
            );
         }
		 
		 public async Task<GetPbPriceUserForViewDto> GetPbPriceUserForView(int id)
         {
            var pbPriceUser = await _pbPriceUserRepository.GetAsync(id);

            var output = new GetPbPriceUserForViewDto { PbPriceUser = ObjectMapper.Map<PbPriceUserDto>(pbPriceUser) };

		    if (output.PbPriceUser.UserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.PbPriceUser.UserId);
                output.UserName = _lookupUser.Name.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PbPriceUsers_Edit)]
		 public async Task<GetPbPriceUserForEditOutput> GetPbPriceUserForEdit(EntityDto input)
         {
            var pbPriceUser = await _pbPriceUserRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbPriceUserForEditOutput {PbPriceUser = ObjectMapper.Map<CreateOrEditPbPriceUserDto>(pbPriceUser)};

		    if (output.PbPriceUser.UserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.PbPriceUser.UserId);
                output.UserName = _lookupUser.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbPriceUserDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PbPriceUsers_Create)]
		 protected virtual async Task Create(CreateOrEditPbPriceUserDto input)
         {
            var pbPriceUser = ObjectMapper.Map<PbPriceUser>(input);

			

            await _pbPriceUserRepository.InsertAsync(pbPriceUser);
         }

		 [AbpAuthorize(AppPermissions.Pages_PbPriceUsers_Edit)]
		 protected virtual async Task Update(CreateOrEditPbPriceUserDto input)
         {
            var pbPriceUser = await _pbPriceUserRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbPriceUser);
         }

		 [AbpAuthorize(AppPermissions.Pages_PbPriceUsers_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbPriceUserRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbPriceUsersToExcel(GetAllPbPriceUsersForExcelInput input)
         {
			
			var filteredPbPriceUsers = _pbPriceUserRepository.GetAll()
						.Include( e => e.UserFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinPriceFilter != null, e => e.Price >= input.MinPriceFilter)
						.WhereIf(input.MaxPriceFilter != null, e => e.Price <= input.MaxPriceFilter)
						.WhereIf(input.MinMonthFilter != null, e => e.Month >= input.MinMonthFilter)
						.WhereIf(input.MaxMonthFilter != null, e => e.Month <= input.MaxMonthFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserFk != null && e.UserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

			var query = (from o in filteredPbPriceUsers
                         join o1 in _lookup_userRepository.GetAll() on o.UserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetPbPriceUserForViewDto() { 
							PbPriceUser = new PbPriceUserDto
							{
                                Price = o.Price,
                                Month = o.Month,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString()
						 });


            var pbPriceUserListDtos = await query.ToListAsync();

            return _pbPriceUsersExcelExporter.ExportToFile(pbPriceUserListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_PbPriceUsers)]
         public async Task<PagedResultDto<PbPriceUserUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PbPriceUserUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new PbPriceUserUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<PbPriceUserUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}